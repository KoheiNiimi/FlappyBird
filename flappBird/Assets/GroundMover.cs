using UnityEngine;
using System.Collections;

public class GroundMover : MonoBehaviour {

	Rigidbody2D player;

	void Start () {
		GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
		
		if (playerGo == null) {
			Debug.LogError("Couldn't find an object tab 'Player'!");
			return;
		}
		
		player = playerGo.rigidbody2D;

	}

	void fixedUpdate() {
		float vel = player.velocity.x * 0.75f;

		transform.position = transform.position + Vector3.right * vel * Time.deltaTime;
	}


}
