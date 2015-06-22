using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mine : MonoBehaviour {

	// Use this for initialization
	public GameObject minePrefabs;
	public Transform characterTransform;
	List<LandMine> _landMines;
	void Start () {
		
		_landMines = new List<LandMine> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void Attack()
	{
		GameObject landMine = Instantiate(minePrefabs, characterTransform.position, characterTransform.rotation) as GameObject;
		landMine.transform.Rotate(0,180,0);
		LandMine script = landMine.GetComponent<LandMine> ();
		script.Init (characterTransform.forward);
		_landMines.Add (script);
	}
}
