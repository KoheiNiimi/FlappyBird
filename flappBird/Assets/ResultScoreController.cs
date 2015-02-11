using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultScoreController : MonoBehaviour
{

		public List<Sprite> _resultNumberImage;

		public List<SpriteRenderer> _resultList;

		private int addResultDigitCount;

		void Awake ()
		{
				_resultList = new List<SpriteRenderer> ();
		}



		// Use this for initialization
		void Start ()
		{
				AddResultDigit (0);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}


		public void countUpScore (int score)
		{
				int updateDigitIndex = 0;
				while (score > 0) {
						int digit = score % 10;
			
						if (_resultList.Count > updateDigitIndex) {
								SpriteRenderer sr = _resultList [updateDigitIndex];
								sr.sprite = _resultNumberImage [digit];
						} else {
								AddResultDigit (digit);
						}
						updateDigitIndex++;
						score = score / 10;
				}
		}


		public void AddResultDigit (int a)
		{
		
				addResultDigitCount += 1;
				GameObject go = new GameObject ();
		
				go.transform.parent = this.transform;
		
				if (addResultDigitCount == 1) {
						go.transform.localPosition = new Vector3 (0f, 0f, -1f);
			
				} else if (addResultDigitCount == 2) {
						go.transform.localPosition = new Vector3 (-0.14f, 0f, -1f);
				} else {
			
				}
		
				SpriteRenderer sr = go.AddComponent<SpriteRenderer> ();
		
				sr.sprite = _resultNumberImage [a];
				_resultList.Add (sr);	
		
		}

		IEnumerator countUp (int score)
		{

				for (int i = 1; i <= score; i++) {
						yield return new WaitForSeconds (0.1f);
						countUpScore (i);
				}


		}
}
