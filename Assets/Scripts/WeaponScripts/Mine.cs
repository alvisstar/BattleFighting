﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mine : Weapon {

	// Use this for initialization
	public GameObject minePrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	void Start () {
		numberOfWeapon = 5;
		maxRangeAttack = 20;
		minRangeAttack = 4;
		piority = 800;
	}
	public override void OnAttack()
	{
		if( characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("MineAttack"))
		{
			numberOfWeapon--;
			GameObject landMine = Instantiate(minePrefabs, new Vector3(equipTransform.position.x,0.5f,equipTransform.position.z), equipTransform.rotation) as GameObject;
			landMine.transform.Rotate(0,0,20	);
			LandMine script = landMine.GetComponent<LandMine> ();
			//equipTransform.rotation = characterTransform.rotation;
			script.Init (characterTransform.forward);

		}
	}
}
