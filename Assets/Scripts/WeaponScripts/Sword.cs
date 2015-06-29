using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	// Use this for initialization

	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{

			characterTransform.GetComponent<PlayerControler>().SetAnimationAttack();
			
			characterTransform.GetComponent<PlayerControler>().FinishAttack();
			
			

		
	}
}
