﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	// Use this for initialization
	private GameObject myChar = null;
	public GameObject camera = null;
	public List<GameObject> characterPrefabs = null;
	int maxWave;
	public List<BotControler> botScripts ;
	// Use this for initialization

	void Start () {
		myChar = Instantiate(characterPrefabs[0], new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject;
		myChar.GetComponent<PlayerControler>().Init (new Vector3 (-1, 0, 0), true);
		camera.GetComponent<CameraControler> ().targetObject = myChar;
		// 6
		Init ();
	}

	void Init()
	{
		maxWave = 5;
	}
	void SpawnWave()
	{

		//int n = Random (5, 6);
		for (int i =0; i<5; i++) {
			GameObject opponentChar = (Instantiate(characterPrefabs[1], new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject);
			BotControler opponentScript = opponentChar.GetComponent<BotControler>();
			float x = Random.Range(-10.0f,10.0f);
			float z =Random.Range(-10.0f,10.0f);
			opponentScript.Init(new Vector3 (x, 0, z), false);
			botScripts.Add(opponentScript);
		}
		maxWave--;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		
	}
	void Update () {
		if (maxWave >= 0 && botScripts.Count==0) {
		
			SpawnWave();
		}
		for (int i =0; i< botScripts.Count; i++) {
			if(botScripts[i].isDie ==true && myChar.GetComponent<PlayerControler>().CheckIsAnimation("TripleKich"))
			{
				Destroy(botScripts[i].gameObject);
				botScripts.RemoveAt(i);

			}
		}
	}
	


	public void HandleFinish(int id)
	{
		Debug.Log(id + " win, congratulation");
		RestartGame ();
	}
	
	public void RestartGame(){
		Application.LoadLevel ("level1");
	}
}
