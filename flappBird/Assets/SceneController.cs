using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
		private AudioSource swooshingSound;

		private SpriteRenderer fadeBlack;

		private TweenAlphaSprite tweenAlpha;

		private bool touchFlg = true;
	

		// Use this for initialization
		void Start ()
		{
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				swooshingSound = audioSources [0];
				tweenAlpha = GameObject.Find ("fadeBlack").GetComponent<TweenAlphaSprite> ();
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
						transform.position = new Vector3 (0.01f, 1.17f, 0f);
						StartCoroutine ("moveTitleReady");
						swooshingSound.Play ();
						touchFlg = false;
				}
		}

		IEnumerator moveTitleReady ()
		{
				StartAnim ();
				yield return new WaitForSeconds (0.5f);
				Application.LoadLevel ("main");
				yield return new WaitForSeconds (0f);

		}

		public void StartAnim ()
		{
				tweenAlpha.Play ();
		}
}
