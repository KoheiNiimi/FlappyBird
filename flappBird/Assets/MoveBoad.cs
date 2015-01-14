using UnityEngine;
using System.Collections;

public class MoveBoad : MonoBehaviour {

	public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void FixedUpdate () {
		
		rigidbody2D.velocity = -Vector2.right * speed;
		
	}
}
