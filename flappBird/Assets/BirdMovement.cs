﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BirdMovement : MonoBehaviour
{

		Vector3 velocity = Vector3.zero;
		float flapSpeed = 2f;
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
		SpriteRenderer gameOverToStartButton;

		CreateManager createObject;

		GameObject result;

		float resultUpParam = 0.05f;

		bool moveResultFlg = false;
	



		// Use this for initialization
		void Start ()
		{
				animator = transform.GetComponentInChildren<Animator> ();
				defaultPlayerPositionX = transform.position.x;
				defaultPlayerPositionY = transform.position.y;
				defaultPlayerPositionZ = transform.position.z;
				score0bject = GameObject.Find ("Score");
				scoreCon = score0bject.GetComponent<ScoreController> ();
				gameOverRenderer = GameObject.Find ("GameOver").GetComponent<SpriteRenderer> ();
				gameOverRenderer.enabled = false;
				gameOverToStartButton = GameObject.Find ("buttonStart").GetComponent<SpriteRenderer> ();
				gameOverToStartButton.enabled = false;
				createObject = GameObject.Find ("CreateManager").GetComponent<CreateManager> ();
				result = GameObject.Find ("Result");
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
						didFlap = true;
				}

				if (moveResultFlg) {
					if (result.transform.position.y >= 2.25f) {
						moveResultFlg = false;
					} else {
						result.transform.position = new Vector3 (result.transform.position.x, result.transform.position.y + resultUpParam, result.transform.position.z);
					}	
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
						if(transform.position.y < 5.37f) {
							rigidbody2D.velocity = Vector3.up * flapSpeed;
						}
								
							animator.SetTrigger ("DoFlap");
							didFlap = false;
					}
					

						if (rigidbody2D.velocity.y > 0) {
								transform.rotation = Quaternion.Euler (0, 0, 0);
						} else {
			float angle = Mathf.Lerp(0, -90, -rigidbody2D.velocity.y);
			transform.rotation = Quaternion.Euler(0, 0, angle);
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
				createObject.stopPipes ();
				createObject.stopGrounds ();
				createObject.stopCreate ();
				StartCoroutine ("appearStartButton");
		moveResultFlg = true;
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

	
//		void moveResult ()
//		{
//				bool moveFlg = true;
//				while (moveFlg) {
//						if (result.transform.position.y == 2.25f) {
//								moveFlg = false;
//						}
//			result.transform.position = new Vector3 (result.transform.position.x, result.transform.position.y + resultUpParam, result.transform.position.z);
//				}
//
//
//		}



}
