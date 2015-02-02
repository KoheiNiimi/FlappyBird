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
						Vector3 vec = transform.localPosition;
						vec.x -= 1f * Time.deltaTime;
						transform.localPosition = vec;
				} 
		}

		public void stopMover ()
		{
				playFlg = false;
		}
}
