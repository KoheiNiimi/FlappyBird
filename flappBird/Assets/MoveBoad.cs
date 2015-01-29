using UnityEngine;
using System.Collections;

public class MoveBoad : MonoBehaviour
{

		public float speed = 1.2f;

		bool playFlg = true;
	
		void Start ()
		{
	
		}

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
