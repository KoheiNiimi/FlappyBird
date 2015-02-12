using UnityEngine;
using System.Collections;

public class MoveBoad : MonoBehaviour
{
	
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
//						vec.x -= 1f * Time.deltaTime;
						vec.x -= 1.3f * Time.deltaTime;
						transform.localPosition = vec;
				} 
		}

		public void stopMover ()
		{
				playFlg = false;
		}
}
