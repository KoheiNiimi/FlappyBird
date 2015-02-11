using UnityEngine;
using System.Collections;

public class SceneToContinueController : MonoBehaviour
{

		private AudioSource swooshingSound;
		private SpriteRenderer fadeBlack;
		private float fadeTime = 0.5f;
		private float currentRemainTime;
	
		// Use this for initialization
		void Start ()
		{
				AudioSource[] audioSources = GetComponents<AudioSource> ();
				swooshingSound = audioSources [0];
				fadeBlack = GameObject.Find ("fadeblack").GetComponent<SpriteRenderer> ();
				currentRemainTime = fadeTime;
	
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnMouseUp ()
		{
				swooshingSound.Play ();
				StartCoroutine ("mainReset");
		}
	
		IEnumerator mainReset ()
		{
				while (currentRemainTime > 0) {
						currentRemainTime -= Time.deltaTime;
						float Alpha = fadeBlack.color.a + (255f / currentRemainTime);
			
						fadeBlack.color = new Color (255, 255, 255, Alpha);
				}
				yield return new WaitForSeconds (0.5f);
				Application.LoadLevel ("main");
		
		}
}
