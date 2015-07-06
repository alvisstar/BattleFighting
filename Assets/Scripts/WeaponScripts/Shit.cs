using UnityEngine;
using System.Collections;

public class Shit : Weapon {	
	
	// Use this for initialization
	public GameObject shitPrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{
		
		if( characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("ShitAttack"))
		{
			numberOfWeapon--;
			GameObject throwingShit = Instantiate(shitPrefabs,equipTransform.position + new Vector3(0, 0, 0), equipTransform.rotation) as GameObject;
			throwingShit.GetComponent<ThrowingShit> ().characterTransform = characterTransform;
			ThrowingShit script = throwingShit.GetComponent<ThrowingShit> ();
			
			//equipTransform.rotation = characterTransform.rotation;
			script.Init (characterTransform.forward);
			
			
			
		}
		
	}
}
