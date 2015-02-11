using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
		private AudioSource swooshingSound;

		private SpriteRenderer fadeBlack;
	

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
				FadeManager.Instance.LoadLevel ("main", 0.7f);
				yield return new WaitForSeconds (0f);

		}
}
