using UnityEngine;
using System.Collections;

public class CreateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject pipes;
	public GameObject ground;
	
	private float waitCreatePipeTime = 1.5f;
	private float waitCreateGroundTime = 3f;

	void Awake(){
		// InvokeRepeating("関数名",初回呼出までの遅延秒数,次回呼出までの遅延秒数)
		if (Application.loadedLevelName == "TitleTop" || Application.loadedLevelName == "ReadyTop") {
			InvokeRepeating("CreateGround", 0, waitCreateGroundTime);
		} else {
			InvokeRepeating("CreatePipes", waitCreatePipeTime, waitCreatePipeTime);
			InvokeRepeating("CreateGround", 0, waitCreateGroundTime);		
		}

	}
	/// <summary>
	/// オブジェクトの生成
	/// </summary>
	void CreatePipes(){
		// インスタンス生成
		Instantiate(pipes, new Vector3(2.5f, Random.Range (1.3f, 3f), 0), Quaternion.identity);
	}

	void CreateGround(){
		Instantiate (ground, new Vector3 (3.36f, 3.86f, -1f), Quaternion.identity);
	}
}
