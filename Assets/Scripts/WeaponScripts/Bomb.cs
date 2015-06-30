using UnityEngine;
using System.Collections;

public class Bomb : Weapon {	
	
	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{

		if( characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("BombAttack"))
		{
			numberOfWeapon--;
			GameObject throwingBomb = Instantiate(bombPrefabs,equipTransform.position + new Vector3(0, 0.5f, 0), equipTransform.rotation) as GameObject;
			throwingBomb.GetComponent<ThrowingBomb> ().characterTransform = characterTransform;
			ThrowingBomb script = throwingBomb.GetComponent<ThrowingBomb> ();

			equipTransform.rotation = characterTransform.rotation;
			script.Init (equipTransform.forward);



		}

	}
}
