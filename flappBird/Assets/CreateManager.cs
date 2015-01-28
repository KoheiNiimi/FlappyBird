using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateManager : MonoBehaviour
{
		public GameObject pipes;
		public GameObject ground;
	
		private float waitCreatePipeTime = 1.5f;
		private float waitCreateGroundTime = 3.0f;
	
		public List<MoveBoad> boadList;
		public MoveBoad bg;
	
		bool playFlg = true;

		// Use this for initialization
		void Start ()
		{
				while (true) {
						if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
								StartCoroutine ("CreateGround");
						} else {
				
						}
				}
		}
	
	
	
	
		// Update is called once per frame
		void Update ()
		{
//				if (playFlg) {
//						if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
//								StartCoroutine ("CreateGround");
//				
//						} else {
//				
//						}
//				}
	
		}

		private IEnumerator CreateGround ()
		{
				Instantiate (ground, new Vector3 (3.36f, 3.86f, -1f), Quaternion.identity);
				yield return new WaitForSeconds (waitCreateGroundTime);
		}

//		void Awake ()
//		{
//
//				if (playFlg) {
//						// InvokeRepeating("関数名",初回呼出までの遅延秒数,次回呼出までの遅延秒数)
//						if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
//								InvokeRepeating ("CreateGround", 0, waitCreateGroundTime);
//						} else {
//								InvokeRepeating ("CreatePipes", waitCreatePipeTime, waitCreatePipeTime);
//								InvokeRepeating ("CreateGround", 0, waitCreateGroundTime);		
//						}
//				}
//
//		}
		/// <summary>
		/// オブジェクトの生成
		/// </summary>
//		void CreatePipes ()
//		{
//				// インスタンス生成
//				GameObject go = Instantiate (pipes, new Vector3 (2.5f, Random.Range (1.3f, 3f), 0), Quaternion.identity) as GameObject;
//				MoveBoad mb = go.GetComponent<MoveBoad> ();
//				boadList.Add (mb);
//		}

//		void CreateGround ()
//		{
//				Instantiate (ground, new Vector3 (3.36f, 3.86f, -1f), Quaternion.identity);
//		}

		public void stopCreate ()
		{
				playFlg = false;
		}

		public void stopPipes ()
		{
				foreach (MoveBoad mb in boadList) {
						mb.stopMover ();
				}
		}
}
