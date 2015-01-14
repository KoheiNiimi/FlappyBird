using UnityEngine;
using System.Collections;

public class BoadManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject boadObject;
	
	private float waitTime = 2.0f;

	void Awake(){
		// InvokeRepeating("関数名",初回呼出までの遅延秒数,次回呼出までの遅延秒数)
		InvokeRepeating("Create", waitTime, waitTime);
	}
	/// <summary>
	/// オブジェクトの生成
	/// </summary>
	void Create(){
		// インスタンス生成
		Instantiate(boadObject, new Vector3(2.5f, 1.7f, 0), Quaternion.identity);
	}
}
