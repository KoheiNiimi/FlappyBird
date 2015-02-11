using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedalController : MonoBehaviour
{

		public List<Sprite> medalList;
		private Sprite medal;

		private SpriteRenderer defaultMedalObj;

		private CreateManager createManager;
	
		// Use this for initialization
		void Start ()
		{
				defaultMedalObj = GameObject.Find ("medal").GetComponent<SpriteRenderer> ();
				createManager = GameObject.Find ("CreateManager").GetComponent<CreateManager> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public IEnumerator startApperMedal (int score)
		{

				yield return new WaitForSeconds (1f);
				appearMedal (score);

		}

		private void appearMedal (int score)
		{
				if (score < 10) {
						return;
				}

				if (score >= 10 && score < 20) {
						medal = medalList [0];
				} else if (score >= 20 && score < 30) {
						medal = medalList [1];
				} else if (score >= 30 && score < 40) {
						medal = medalList [2];
				} else if (score >= 40) {
						medal = medalList [3];
				}

				defaultMedalObj.sprite = medal;
				defaultMedalObj.enabled = true;

				createManager.StartCreateKirakira ();
		}
}
