using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BirdMovement : MonoBehaviour
{
	
		private float flapSpeed = 3.0f;

		float defaultPlayerPositionX;
		float defaultPlayerPositionY;
		float defaultPlayerPositionZ;

		bool didFlap = false;

		bool upBirdFlg = true;
		bool downBirdFlg = false;

		private bool gameover = false;

		Animator animator;

		public List<Sprite> _numberImage;

		GameObject score0bject;
		GameObject scoreResultBbject;
		ScoreController scoreCon;
		ResultScoreController resultScoreCon;

		SpriteRenderer gameOverRenderer;
		SpriteRenderer gameOverToStartButton;
		SpriteRenderer getReady;
		SpriteRenderer explanation;
		SpriteRenderer fadeWhite;

		GameObject startButton;

		CreateManager createObject;

		GameObject result;

		float resultUpParam = 0.05f;

		bool moveResultFlg = false;

		bool startMoveResult = false;

		private AudioSource scoreGetSound;
		private AudioSource wingSound;
		private AudioSource hitSound;
		private AudioSource dieSound;
		private AudioSource swooshingSound;

		private bool playHitSoundFlg = true;
		private bool playDieSoundFlg = true;
		private bool playGameOverSwooshingSoundFlg = true;
		private bool mainGamePlayFlg = false;

		private float fadeTime = 0.5f;
		private float currentRemainTime;

		private bool fadeFlg = false;

		private int touchCount = 0;

		private BirdSelectController birdSelectController;


		// Use this for initialization
		void Start ()
		{

				animator = transform.GetComponentInChildren<Animator> ();
				defaultPlayerPositionX = transform.position.x;
				defaultPlayerPositionY = transform.position.y;
				defaultPlayerPositionZ = transform.position.z;
				score0bject = GameObject.Find ("Score");
				scoreCon = score0bject.GetComponent<ScoreController> ();
				scoreResultBbject = GameObject.Find ("resultScore");
				resultScoreCon = scoreResultBbject.GetComponent<ResultScoreController> ();
				birdSelectController = gameObject.GetComponent<BirdSelectController> ();
				gameOverRenderer = GameObject.Find ("GameOver").GetComponent<SpriteRenderer> ();
				gameOverRenderer.enabled = false;
				getReady = GameObject.Find ("getready").GetComponent<SpriteRenderer> ();
				explanation = GameObject.Find ("explanation").GetComponent<SpriteRenderer> ();
				startButton = GameObject.Find ("buttonStart");
				gameOverToStartButton = startButton.GetComponent<SpriteRenderer> ();
				startButton.collider.enabled = false;
				gameOverToStartButton.enabled = false;
				createObject = GameObject.Find ("CreateManager").GetComponent<CreateManager> ();
				result = GameObject.Find ("Result");
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				scoreGetSound = audioSources [0];
				wingSound = audioSources [1];
				hitSound = audioSources [2];
				dieSound = audioSources [3];
				swooshingSound = audioSources [4];
				fadeWhite = GameObject.Find ("fadewhite").GetComponent<SpriteRenderer> ();
				if (mainGamePlayFlg) {
			
				} else {
			
				}
				currentRemainTime = fadeTime;
		}

		void Update ()
		{

				rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
				if (Application.loadedLevelName != "TitleTop") {
						if (!gameover) {
								if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
										touchCount += 1;
										if (touchCount == 1) {
												createObject.startCreatePipes ();
												birdSelectController.changeBirdAnimation ();
										}
										mainGamePlayFlg = true;
										fadeFlg = true;
										rigidbody2D.isKinematic = false;
										didFlap = true;
										wingSound.Play ();
								}
						}
			
						if (moveResultFlg) {
								if (result.transform.position.y >= 2.4f) {
										moveResultFlg = false;
										swooshingSound.Play ();
										resultScoreCon.StartCoroutine ("countUp", score);
								} else {
										if (startMoveResult) {
												Vector3 vec = result.transform.position;
												vec.y += 8f * Time.deltaTime;
												result.transform.position = vec;
										} else {
												StartCoroutine ("moveResult");
										}
					
								}	
						}
				}

				if (fadeFlg) {
						currentRemainTime -= Time.deltaTime;
						float alpha = currentRemainTime / fadeTime;
						getReady.color = new Color (255, 255, 255, alpha);
						explanation.color = new Color (255, 255, 255, alpha);

				}
		}
	
		IEnumerator fadeReady ()
		{
				while (getReady.color.a != 0) {
						float alpha = getReady.color.a;
						float afterAlpah = alpha - 1f;
						getReady.color = new Color (255, 255, 255, afterAlpah);
				}
			
				yield return new WaitForSeconds (1f);
		}
	
		void Awake ()
		{
				Application.targetFrameRate = 60;
				Physics.gravity = new Vector3 (0, -20.0f, 0);
				rigidbody2D.mass = 5000.0f;
		}
		
		IEnumerator moveResult ()
		{
				yield return new WaitForSeconds (1f);
				startMoveResult = true;

		}

		void FixedUpdate ()
		{

				if (Application.loadedLevelName == "TitleTop" || !mainGamePlayFlg) {
						animator.SetTrigger ("DoFlap");
						if (upBirdFlg) {
								transform.position = new Vector3 (transform.position.x, transform.position.y + 0.004f, transform.position.z);
						}
						if (downBirdFlg) {
								transform.position = new Vector3 (transform.position.x, transform.position.y - 0.004f, transform.position.z);
						}
						if (transform.position.y > defaultPlayerPositionY + 0.04f) {
								downBirdFlg = true;
								upBirdFlg = false;
						}
						if (transform.position.y < defaultPlayerPositionY - 0.04f) {
								upBirdFlg = true;
								downBirdFlg = false;
						}

				} else {

						if (!gameover) {
								if (didFlap) {
										animator.SetTrigger ("DoFlap");
						
										if (transform.position.y < 5.37f) {
												Jump ();
										}
						
						
										didFlap = false;
								}
					
					
								if (rigidbody2D.velocity.y > 0) {
										transform.rotation = Quaternion.Euler (0, 0, 30);
								} else {
										if (transform.eulerAngles.z > 270 && transform.eulerAngles.z <= 360) {
												transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 5);
										} else if (transform.eulerAngles.z > 265 && transform.eulerAngles.z < 275) {
												transform.rotation = Quaternion.Euler (0, 0, 270);
										} else if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z < 270) {
												transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 2);
										}
								}
					
					
						} else {
					
								if (transform.eulerAngles.z >= 270 && transform.eulerAngles.z <= 360) {
										transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 5);
								} else if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 45) {
										transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 2);
								}
					
								//								if (rigidbody2D.velocity.y < -2.842f) { 
								//										float angle = Mathf.Lerp (0, -90, -rigidbody2D.velocity.y / 2);
								//										transform.rotation = Quaternion.Euler (0, 0, angle);
								//								} else {
								if (playDieSoundFlg) {
										dieSound.Play ();
										playDieSoundFlg = false;
								}
								//										float angle = Mathf.Lerp (0, -90, 2.842f / 2);
								//										transform.rotation = Quaternion.Euler (0, 0, angle);
								//								}
						}

				}


		}

		void Jump ()
		{
				rigidbody2D.velocity = Vector3.zero;
				rigidbody2D.velocity = Vector3.up * flapSpeed;
				animator.SetTrigger ("DoFlap");
				didFlap = false;

		}
	
		private int score = 0;

		public GUIStyle guiStyle;


		void OnTriggerEnter2D (Collider2D collider)
		{
				if (!gameover) {
						score += 1;
						scoreCon.UpdateScore (score);
						scoreGetSound.Play ();
				}
		}

		// 何かにぶつかったら呼ばれる
		void OnCollisionEnter2D (Collision2D collision)
		{
				StartCoroutine (GameOver ());
				if (playHitSoundFlg) {
						hitSound.Play ();
						playHitSoundFlg = false;

				}
				StartCoroutine ("appearGameOverButton"); 
				createObject.stopPipes ();
				createObject.stopGrounds ();
				createObject.stopCreate ();
				StartCoroutine ("appearStartButton");         
				moveResultFlg = true;
				scoreCon.StartCoroutine ("viewDisableScore");
				createObject.disablePipesTrigger ();
				animator.ResetTrigger ("DoFlap");

		}


		IEnumerator GameOver ()
		{
				gameover = true;
				fadeWhite.enabled = true;
				yield return new WaitForSeconds (0.0005f);
				fadeWhite.enabled = false;

		}
		
		IEnumerator appearStartButton ()
		{
				yield return new WaitForSeconds (1.5f);

				startButton.collider.enabled = true;
				
				gameOverToStartButton.enabled = true;

		}

		IEnumerator appearGameOverButton ()
		{
				yield return new WaitForSeconds (0.5f);
				if (playGameOverSwooshingSoundFlg) {
						swooshingSound.Play ();
						playGameOverSwooshingSoundFlg = false;
				}
				
				gameOverRenderer.enabled = true;
		}

		public void Restart ()
		{
				mainGamePlayFlg = false;
		}

}
