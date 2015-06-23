using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mine : Weapon {

	// Use this for initialization
	public GameObject minePrefabs;
	public Transform characterTransform;
	
	public override void OnAttack()
	{
		GameObject landMine = Instantiate(minePrefabs, characterTransform.position, characterTransform.rotation) as GameObject;
		landMine.transform.Rotate(0,180,0);
		LandMine script = landMine.GetComponent<LandMine> ();
		script.Init (characterTransform.forward);
	}
}
