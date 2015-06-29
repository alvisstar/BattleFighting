using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	// Use this for initialization

	public Transform characterTransform;
	public Transform equipTransform;
	void Start () {
		numberOfWeapon = -1;

	}

	public override void OnAttack()
	{

			characterTransform.GetComponent<PlayerControler>().SetAnimationAttack();
			
			characterTransform.GetComponent<PlayerControler>().FinishAttack();
			
			

		
	}
}
