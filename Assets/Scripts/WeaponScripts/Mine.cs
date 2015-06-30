using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mine : Weapon {

	// Use this for initialization
	public GameObject minePrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{
		if( characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("MineAttack"))
		{
			numberOfWeapon--;
			GameObject landMine = Instantiate(minePrefabs, equipTransform.position, equipTransform.rotation) as GameObject;
		//landMine.transform.Rotate(0,180,0);
			LandMine script = landMine.GetComponent<LandMine> ();
			equipTransform.rotation = characterTransform.rotation;
			script.Init (equipTransform.forward);

		}
	}
}
