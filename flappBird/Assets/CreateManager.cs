using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateManager : MonoBehaviour
{
		public GameObject pipes;
		public GameObject ground;
		public GameObject kirakira;

		private GameObject defaultGround;			
	
		private float waitCreatePipeTime = 0.8f;
		private float waitCreateGroundTime = 2.2f;
	
		public List<MoveBoad> boadList;
		public List<MoveBoad> backgroundList;
		public MoveBoad bg;

		private GameObject[] gameObjectList;

		private bool firstWaitFlg = true;


		// Use this for initialization
		void Start ()
		{
				defaultGround = GameObject.Find ("BG_GROUND");
				StartCoroutine ("CreateGround");

		}
	
	
	
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		private IEnumerator CreateGround ()
		{

				while (true) {
						if (firstWaitFlg) {
								yield return new WaitForSeconds (waitCreateGroundTime);
								firstWaitFlg = false;
						}
						GameObject go = Instantiate (ground, new Vector3 (3.36f, 3.86f, -1f), Quaternion.identity)  as GameObject;
						MoveBoad mb = go.GetComponent<MoveBoad> ();
						backgroundList.Add (mb);
						yield return new WaitForSeconds (2.87f);
				
				}
				
				
		}

		private IEnumerator CreatePipes ()
		{

				yield return new WaitForSeconds (1.5f);
				while (true) {
				
						yield return new WaitForSeconds (waitCreatePipeTime);
//						GameObject go = Instantiate (pipes, new Vector3 (2.5f, Random.Range (1.3f, 3f), 0), Quaternion.identity) as GameObject;
						GameObject go = Instantiate (pipes, new Vector3 (2.5f, 2f, 0), Quaternion.identity) as GameObject;
						MoveBoad mb = go.GetComponent<MoveBoad> ();
						boadList.Add (mb);
						yield return new WaitForSeconds (waitCreatePipeTime);
				
				}
				
		}

		private IEnumerator createKirakira ()
		{
				while (true) {
						GameObject go = Instantiate (kirakira, new Vector3 (Random.Range (-0.17f, 0.29f), Random.Range (2.16f, 2.58f), -3.6f), Quaternion.identity) as GameObject;
						yield return new WaitForSeconds (0.5f);
						Destroy (go);
				}
		}
	
		public void stopCreate ()
		{
				StopAllCoroutines ();
		}

		public void stopPipes ()
		{
				foreach (MoveBoad mb in boadList) {
						mb.stopMover ();
				}
		}
		public void disablePipesTrigger ()
		{
				gameObjectList = GameObject.FindGameObjectsWithTag ("pipe");
				foreach (GameObject go in gameObjectList) {
						go.collider2D.isTrigger = true;
				}

		}
		public void stopGrounds ()
		{
				if (defaultGround != null) {
						MoveBoad mb = defaultGround.GetComponent<MoveBoad> ();
						mb.stopMover ();
				}
				foreach (MoveBoad mb in backgroundList) {
						mb.stopMover ();
				}
		}

		public void startCreatePipes ()
		{
				StartCoroutine ("CreatePipes");
		}

		public void StartCreateKirakira ()
		{
				StartCoroutine ("createKirakira");
		}
	
}
