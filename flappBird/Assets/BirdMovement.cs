using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BirdMovement : MonoBehaviour
{

		Vector3 velocity = Vector3.zero;
		float flapSpeed = 100f;
		float forwardSpeed = 1f;

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
		ScoreController scoreCon;

	SpriteRenderer gameOverRenderer;

	GameObject createObject;
	MoveBoad pipes;
	MoveBoad bgGround;
	


		// Use this for initialization
		void Start ()
		{
				animator = transform.GetComponentInChildren<Animator> ();
				defaultPlayerPositionX = transform.position.x;
				defaultPlayerPositionY = transform.position.y;
				defaultPlayerPositionZ = transform.position.z;
				score0bject = GameObject.Find ("Score");
				scoreCon = score0bject.GetComponent<ScoreController> ();
		gameOverRenderer = GameObject.Find ("GameOver").GetComponent<SpriteRenderer>();
		gameOverRenderer.enabled = false;
		createObject = GameObject.Find ("CreateManager");
// 		pipes = GameObject.Find ("Pipes").GetComponent<MoveBoad>();
//		bgGround = GameObject.Find ("BG_GROUND").GetComponent<MoveBoad>();
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
						didFlap = true;
				}
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

						if (didFlap) {
								rigidbody2D.AddForce (Vector2.up * flapSpeed);

								animator.SetTrigger ("DoFlap");

								didFlap = false;
						}

						if (rigidbody2D.velocity.y > 0) {
								transform.rotation = Quaternion.Euler (0, 0, 0);
						} else {
//			float angle = Mathf.Lerp(0, -90, -rigidbody2D.velocity.y);
//			transform.rotation = Quaternion.Euler(0, 0, angle);
						}

				}


		}

		// スコア用
		private int score = 0;
		// スコアの表示の設定を行います。
		public GUIStyle guiStyle;
		// トリガーに入った時に score を加算します。
		void OnTriggerEnter2D (Collider2D collider)
		{
				score += 1;
				scoreCon.UpdsateScore (score);
		}

		// 何かにぶつかったら呼ばれる
		void OnCollisionEnter2D (Collision2D collision)
		{
				StartCoroutine (GameOver ());
 		gameOverRenderer.enabled = true;
		createObject.SendMessage ("stopCreate");
//		pipes.stopMover ();
//		bgGround.stopMover ();

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
				Application.LoadLevel ("Main");
		}



}
