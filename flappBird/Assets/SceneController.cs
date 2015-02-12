using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
		private AudioSource swooshingSound;

		private SpriteRenderer fadeBlack;

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
				transform.position = new Vector3 (0.01f, 1.15f, 0f);
		}

		void OnMouseUp ()
		{	
				if (touchFlg) {
						swooshingSound.Play ();
						StartCoroutine ("moveTitleReady");
						touchFlg = false;
				}
		}

		IEnumerator moveTitleReady ()
		{
				transform.position = new Vector3 (0.01f, 1.17f, 0f);
				FadeManager.Instance.LoadLevel ("main", 0.7f);
				yield return new WaitForSeconds (0f);

		}
}
