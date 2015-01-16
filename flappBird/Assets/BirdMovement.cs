using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	float flapSpeed = 100f;
	float forwardSpeed = 1f;

	float defaultPlayerPositionX;
	float defaultPlayerPositionY;
	float defaultPlayerPositionZ;

	bool didFlap = false;

	private bool gameover = false;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator> ();
		defaultPlayerPositionX = transform.position.x;
		defaultPlayerPositionY = transform.position.y;
		defaultPlayerPositionZ = transform.position.z;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			didFlap = true;
		}
	}

	void FixedUpdate () {

		if (Application.loadedLevelName == "TitleTop") {
			if (transform.position.y < defaultPlayerPositionY + 0.2f) {
				transform.position = new Vector3(transform.position.x,  transform.position.y + 0.01f, transform.position.z);
			}else if(transform.position.y > defaultPlayerPositionY - 0.2f){
				transform.position = new Vector3(transform.position.x,  transform.position.y - 0.01f , transform.position.z);
			}

		} else {

			if (didFlap) {
				rigidbody2D.AddForce (Vector2.up * flapSpeed);

				animator.SetTrigger("DoFlap");

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
	void OnTriggerEnter2D(Collider2D collider) {
		score += 1;
		Destroy(collider); // OnTriggerEnter は1回しか呼ばれない、という認識なんですが何度も呼ばれてウボァーってなったので Destroy 呼んでます。
	}

	void OnGUI () {
		if (Application.loadedLevelName != "TitleTop") {
						if (gameover) {
								GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "gameover\nscore:" + score.ToString (), guiStyle);
						} else {
								GUI.Label (new Rect (0, 0, 200, 50), "score:" + score.ToString (), guiStyle);
						}
				}
	}

	// 何かにぶつかったら呼ばれる
	void OnCollisionEnter2D(Collision2D collision) {
		StartCoroutine(GameOver());
	}

	IEnumerator GameOver() {
		// ゲームオーバーのフラグをたてる
		gameover = true;
		// マウス連打してたらスコアが表示されずに画面が遷移するのでその対策
		yield return new WaitForSeconds(1f);
		// マウスクリックしたらゲームの最初に戻る
		while (!Input.GetMouseButtonDown(0)) { yield return 0; }
		Application.LoadLevel("Main");
	}



}
