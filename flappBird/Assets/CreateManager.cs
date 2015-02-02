using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateManager : MonoBehaviour
{
		public GameObject pipes;
		public GameObject ground;

		private GameObject defaultGround;			
	
		private float waitCreatePipeTime = 0.8f;
		private float waitCreateGroundTime = 3.0f;
	
		public List<MoveBoad> boadList;
		public List<MoveBoad> backgroundList;
		public MoveBoad bg;

	private GameObject[] gameObjectList;


		// Use this for initialization
		void Start ()
		{
				defaultGround = GameObject.Find ("BG_GROUND");
				if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
						StartCoroutine ("CreateGround");
				} else {
						StartCoroutine ("CreateGround");
						StartCoroutine ("CreatePipes");
				}
		}
	
	
	
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		private IEnumerator CreateGround ()
		{

				while (true) {

						GameObject go = Instantiate (ground, new Vector3 (3.36f, 3.86f, -1f), Quaternion.identity)  as GameObject;
						MoveBoad mb = go.GetComponent<MoveBoad> ();
						backgroundList.Add (mb);
						yield return new WaitForSeconds (waitCreateGroundTime);
				
				}
				
				
		}

		private IEnumerator CreatePipes ()
		{
				while (true) {
				
						yield return new WaitForSeconds (waitCreatePipeTime);
						GameObject go = Instantiate (pipes, new Vector3 (2.5f, Random.Range (1.3f, 3f), 0), Quaternion.identity) as GameObject;
						MoveBoad mb = go.GetComponent<MoveBoad> ();
						boadList.Add (mb);
						yield return new WaitForSeconds (waitCreatePipeTime);
				
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
		public void disablePipesTrigger() {
		gameObjectList = GameObject.FindGameObjectsWithTag("pipe");
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
}
