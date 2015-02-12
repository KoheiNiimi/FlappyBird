using UnityEngine;
using System.Collections;

public class SceneToContinueController : MonoBehaviour
{

		private AudioSource swooshingSound;
		private bool touchFlg = true;
	
		// Use this for initialization
		void Start ()
		{
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				swooshingSound = audioSources [0];
	
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnMouseDown ()
		{
				transform.position = new Vector3 (0.01f, 1.15f, -2f);
		}
	
		void OnMouseUp ()
		{
				if (touchFlg) {
						swooshingSound.Play ();
						StartCoroutine ("mainReset");
						touchFlg = false;
				}
			
		}
	
		IEnumerator mainReset ()
		{

				transform.position = new Vector3 (0.01f, 1.17f, -2f);
				FadeManager.Instance.LoadLevel ("main", 0.7f);
				yield return new WaitForSeconds (0f);
		
		}
}
