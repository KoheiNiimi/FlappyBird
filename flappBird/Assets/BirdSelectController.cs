using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdSelectController : MonoBehaviour
{

		public List<RuntimeAnimatorController> birdList;

		public List<RuntimeAnimatorController> birdFlappyingList;

		private Animator animator;

		private string BIRD_COLOR_KEY = "bird_color";

		// Use this for initialization
		void Start ()
		{
				int random = Random.Range (0, 3);
				animator = GetComponentInChildren<Animator> ();
				animator.runtimeAnimatorController = birdList [random];
				PlayerPrefs.SetInt (BIRD_COLOR_KEY, random);
				
		}

		public void changeBirdAnimation ()
		{
				int index = PlayerPrefs.GetInt (BIRD_COLOR_KEY);
				PlayerPrefs.DeleteKey (BIRD_COLOR_KEY);
				animator = GetComponentInChildren<Animator> ();
				animator.runtimeAnimatorController = birdFlappyingList [index];

		}

	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
