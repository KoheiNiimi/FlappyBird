using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreController : MonoBehaviour
{

		public List<Sprite> _numberImage;

		public List<SpriteRenderer> _list;

		void Awake ()
		{
				_list = new List<SpriteRenderer> ();
		}

		// Use this for initialization
		void Start ()
		{
	
				AddDigit (0);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void UpdsateScore (int score)
		{
				int updateDigitIndex = 0;
				while (score > 0) {
						int digit = score % 10;

						if (_list.Count > updateDigitIndex) {
								SpriteRenderer sr = _list [updateDigitIndex];
								sr.sprite = _numberImage [digit];
						} else {
								AddDigit (digit);
						}
						updateDigitIndex++;
						score = score / 10;
				}
		}

		public void AddDigit (int a)
		{
				GameObject go = new GameObject ();

				go.transform.parent = this.transform;

				go.transform.localPosition = new Vector3 (-0.24f, 0.3f, -2f);

				SpriteRenderer sr = go.AddComponent<SpriteRenderer> ();

				sr.sprite = _numberImage [a];
				_list.Add (sr);	
		}

		IEnumerator viewDisableScore ()
		{
				yield return new WaitForSeconds (1.5f);
				foreach (SpriteRenderer sp in _list) {
						sp.enabled = false;
				}
		}
}
