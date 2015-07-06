using UnityEngine;
using System.Collections;

public class Shit : Weapon {	
	
	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{
		
		if( characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("ShitAttack"))
		{
			numberOfWeapon--;
			GameObject throwingBomb = Instantiate(bombPrefabs,equipTransform.position + new Vector3(0, 1.0f, 0), equipTransform.rotation) as GameObject;
			throwingBomb.GetComponent<ThrowingShit> ().characterTransform = characterTransform;
			ThrowingShit script = throwingBomb.GetComponent<ThrowingShit> ();
			
			//equipTransform.rotation = characterTransform.rotation;
			script.Init (characterTransform.forward);
			
			
			
		}
		
	}
}
