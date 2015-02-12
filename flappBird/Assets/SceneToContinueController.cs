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
				FadeManager.Instance.LoadLevel ("main", 0.7f);
				yield return new WaitForSeconds (0f);
		
		}
}
