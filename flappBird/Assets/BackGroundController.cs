using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackGroundController : MonoBehaviour
{

		public List<Sprite> backGroundList;

		private SpriteRenderer[] spriteRenderes;
	
		// Use this for initialization
		void Start ()
		{
				
				int random = Random.Range (0, 2);
				spriteRenderes = GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer bg in spriteRenderes) {
						bg.sprite = backGroundList [random];
				}
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
}
