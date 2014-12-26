using UnityEngine;
using System.Collections;

public class BgLooper : MonoBehaviour {

	int numBgPanels = 6;
	
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("Triggered:" + collider.name);

		float widthOfBgObject = ((BoxCollider2D)collider).size.x;

		Vector3 pos = collider.transform.position;

		pos.x += widthOfBgObject * numBgPanels - widthOfBgObject/2f;

		collider.transform.position = pos;
	}
}
