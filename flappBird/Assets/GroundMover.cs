using UnityEngine;
using System.Collections;

public class GroundMover : MonoBehaviour {

	float speed = 0f;

	void fixedUpdate() {
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
	}


}
