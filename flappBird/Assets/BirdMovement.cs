using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BirdMovement : MonoBehaviour
{
	
		private float flapSpeed = 2f;

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

		CreateManager createObject;

		GameObject result;

		float resultUpParam = 0.05f;

		bool moveResultFlg = false;

		bool startMoveResult = false;
	

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
				gameOverToStartButton = GameObject.Find ("buttonStart").GetComponent<SpriteRenderer> ();
				gameOverToStartButton.enabled = false;
				createObject = GameObject.Find ("CreateManager").GetComponent<CreateManager> ();
				result = GameObject.Find ("Result");
				animator.SetTrigger ("DoFlap");
				rigidbody2D.velocity = Vector3.up * flapSpeed;
				transform.rotation = Quaternion.Euler (0, 0, 30);
		}

		void Update ()
		{

				rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
				if (!gameover) {
						if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
								didFlap = true;
						}
				}

				if (moveResultFlg) {
						if (result.transform.position.y >= 2.25f) {
								moveResultFlg = false;
								
				resultScoreCon.StartCoroutine("countUp",score);
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
										if (transform.position.y < 5.37f) {
												rigidbody2D.velocity = Vector3.up * flapSpeed;
										}
								
										animator.SetTrigger ("DoFlap");
										didFlap = false;
								}
					

								if (rigidbody2D.velocity.y > 0) {
										transform.rotation = Quaternion.Euler (0, 0, 30);
								} else {
										float angle = Mathf.Lerp (0, -90, -rigidbody2D.velocity.y / 2);
										transform.rotation = Quaternion.Euler (0, 0, angle);
								}

						} else {

								if (rigidbody2D.velocity.y < -2.842f) { 
										float angle = Mathf.Lerp (0, -90, -rigidbody2D.velocity.y / 2);
										transform.rotation = Quaternion.Euler (0, 0, angle);
								} else {
										float angle = Mathf.Lerp (0, -90, 2.842f / 2);
										transform.rotation = Quaternion.Euler (0, 0, angle);
								}
						}    
				}


		}
	
		private int score = 0;

		public GUIStyle guiStyle;


		void OnTriggerEnter2D (Collider2D collider)
		{
		if (!gameover) {
						score += 1;
			audioSource.Play();
						scoreCon.UpdateScore (score);
						

				}
		}

		// 何かにぶつかったら呼ばれる
		void OnCollisionEnter2D (Collision2D collision)
		{
				StartCoroutine (GameOver ());
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
				gameOverToStartButton.enabled = true;

		}

		IEnumerator appearGameOverButton ()
		{
				yield return new WaitForSeconds (0.5f);
				gameOverRenderer.enabled = true;
		}

}
