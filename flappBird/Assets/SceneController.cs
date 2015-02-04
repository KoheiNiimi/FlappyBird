using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
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
