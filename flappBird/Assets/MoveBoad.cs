using UnityEngine;
using System.Collections;

public class MoveBoad : MonoBehaviour
{

		public float speed = 1.2f;

		bool playFlg = true;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		
		void FixedUpdate ()
		{

				if (playFlg) {
						rigidbody2D.velocity = -Vector2.right * speed;
				} else {
						rigidbody2D.velocity = Vector2.zero;
				}
		}

		public void stopMover ()
		{
				playFlg = false;
		}
}
