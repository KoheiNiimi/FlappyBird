using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	float flapSpeed = 100f;
	float forwardSpeed = 1f;


	bool didFlap = false;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator> ();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			didFlap = true;
		}
	}

	void FixedUpdate () {
		rigidbody2D.AddForce (Vector2.right * forwardSpeed);

		if (didFlap) {
			rigidbody2D.AddForce (Vector2.up * flapSpeed);

			animator.SetTrigger("DoFlap");

			didFlap = false;
		}

		if (rigidbody2D.velocity.y > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		} else {
			float angle = Mathf.Lerp(0, -90, -rigidbody2D.velocity.y);
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}


	}

	void OnCollisionenter2D(Collision2D collision) {

	}
}
