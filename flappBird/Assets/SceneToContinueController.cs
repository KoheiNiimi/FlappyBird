using UnityEngine;
using System.Collections;

public class SceneToContinueController : MonoBehaviour
{

		private AudioSource swooshingSound;

		// Use this for initialization
		void Start ()
		{
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				swooshingSound = audioSources [0];
	
		}
	
		// Update is called once per frame
		void Update ()
		{
//		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
//			Application.LoadLevel("ReadyTop"); 
//		}
		}

		void OnMouseUp ()
		{
				swooshingSound.Play ();
				StartCoroutine ("moveTitleReady");
		}
	
		IEnumerator moveTitleReady ()
		{
				yield return new WaitForSeconds (0.5f);
				Application.LoadLevel ("ReadyTop");
		
		}
}
