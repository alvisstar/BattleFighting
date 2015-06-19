using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	// Use this for initialization
	private GameObject myChar = null;
	public GameObject camera = null;
	public List<GameObject> characterPrefabs = null;
	// Use this for initialization

	void Start () {
		myChar = Instantiate(characterPrefabs[0], new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject;
		myChar.GetComponent<PlayerControler>().Init (new Vector3 (-1, 0, 0), true);
		camera.GetComponent<CameraControler> ().targetObject = myChar;
		// 6
		GameObject opponentChar = (Instantiate(characterPrefabs[1], new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject);
		BotControler opponentScript = opponentChar.GetComponent<BotControler>();
		opponentScript.Init(new Vector3 (-3, 0, 0), false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
	void Update () {
		
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
