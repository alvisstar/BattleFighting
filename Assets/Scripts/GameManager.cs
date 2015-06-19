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
