using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BirdMovement : MonoBehaviour
{
	
		private float flapSpeed = 3f;

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
				gameOverRenderer = GameObject.Find ("GameOver").GetComponent<SpriteRenderer> ();
				gameOverRenderer.enabled = false;
				startButton = GameObject.Find ("buttonStart");
				gameOverToStartButton = startButton.GetComponent<SpriteRenderer> ();
				startButton.collider.enabled = false;
				gameOverToStartButton.enabled = false;
				createObject = GameObject.Find ("CreateManager").GetComponent<CreateManager> ();
				result = GameObject.Find ("Result");
				transform.rotation = Quaternion.Euler (0, 0, 30);
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				scoreGetSound = audioSources [0];
				wingSound = audioSources [1];
				hitSound = audioSources [2];
				dieSound = audioSources [3];
				swooshingSound = audioSources [4];
		}

		void Update ()
		{

				rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
				if (!gameover) {
						if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
								didFlap = true;
								wingSound.Play ();
						}
				}

				if (moveResultFlg) {
						if (result.transform.position.y >= 2.25f) {
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

	void Awake()
	{
		Application.targetFrameRate = 60;
		Physics.gravity = new Vector3(0, -20.0f, 0);
		rigidbody2D.mass = 5000.0f;
		}
		
		IEnumerator moveResult ()
		{
				yield return new WaitForSeconds (1f);
				startMoveResult = true;

		}

		void FixedUpdate ()
		{

				if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
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
						Jump();
										}
								
									
										didFlap = false;
								}
					

								if (rigidbody2D.velocity.y > 0) {
										transform.rotation = Quaternion.Euler (0, 0, 30);
								} else {
					if(transform.eulerAngles.z >= 270 && transform.eulerAngles.z <= 360) {
//						float angle = Mathf.Lerp (0, -90, -rigidbody2D.velocity.y / 2);
//						transform.rotation = Quaternion.Euler (0, 0, angle);
						transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 5);
					} else if(transform.eulerAngles.z >= 0){
						transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - 2);
									}
								}


						} else {

								if (rigidbody2D.velocity.y < -2.842f) { 
										float angle = Mathf.Lerp (0, -90, -rigidbody2D.velocity.y / 2);
										transform.rotation = Quaternion.Euler (0, 0, angle);
								} else {
										if (playDieSoundFlg) {
												dieSound.Play ();
												playDieSoundFlg = false;
										}
										float angle = Mathf.Lerp (0, -90, 2.842f / 2);
										transform.rotation = Quaternion.Euler (0, 0, angle);
								}
						}    
				}


		}

	 void Jump()
	{
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.velocity = Vector3.up * flapSpeed;
		animator.SetTrigger ("DoFlap");
//		rigidbody.AddForce (0, 10.0f, 0, ForceMode.VelocityChange);
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

		}


		IEnumerator GameOver ()
		{
				// ゲームオーバーのフラグをたてる
				gameover = true;
				// マウス連打してたらスコアが表示されずに画面が遷移するのでその対策
				yield return new WaitForSeconds (1f);
				// マウスクリックしたらゲームの最初に戻る
				while (!Input.GetMouseButtonDown(0)) {
						yield return 0;
				}
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

}
