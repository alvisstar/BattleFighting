using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	// Use this for initialization
	private GameObject myChar = null;
	public GameObject camera = null;
	public List<GameObject> characterPrefabs = null;
	public TouchController	ctrl;
	public GUISkin	guiSkin;
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
		if (this.ctrl){	
			// Get stick and zone references by IDs...			
			TouchStick 
				walkStick = this.ctrl.GetStick(0);
			
			if (walkStick.Pressed()){
				float playerSpeed = myChar.GetComponent<PlayerControler>().speed;
				myChar.GetComponent<PlayerControler>().Move (walkStick.GetVec3d(true, 0));
				myChar.GetComponent<Rigidbody> ().velocity = walkStick.GetVec3d(true, 0) * playerSpeed * 100;				
			}			
			// Stop when stick is released...
			
			else {
				
			}
			// Shoot when right stick is pressed...

		}
	}
	void Update () {
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
	void OnGUI()
	{
		// Manually draw the controller...		
		if (this.ctrl != null)
			this.ctrl.DrawControllerGUI();
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
