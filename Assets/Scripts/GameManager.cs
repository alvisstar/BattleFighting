using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	// Use this for initialization
	private GameObject myChar = null;
	public GameObject camera = null;
	public List<GameObject> characterPrefabs = null;
	int maxWave;
	public List<BotControler> botScripts ;
	public GameObject aiBotManager ;
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
		for (int i =0; i<0; i++) {
			GameObject opponentChar = (Instantiate(characterPrefabs[1], new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject);
			BotControler opponentScript = opponentChar.GetComponent<BotControler>();
			float x = Random.Range(-100.0f,100.0f);
			float z =Random.Range(-100.0f,100.0f);
			opponentScript.Init(new Vector3 (x, 0, z), false);
			opponentScript.targetObject = myChar;
			botScripts.Add(opponentScript);
		}
		maxWave--;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		
	}
	void Update () {
		aiBotManager.GetComponent<AIBotManager> ().UpdateUnit (myChar, botScripts);
		if (maxWave >= 0 && botScripts.Count==0) {
		
			SpawnWave();
		}
		for (int i =0; i< botScripts.Count; i++) {
			if(botScripts[i].isDie ==true)
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
