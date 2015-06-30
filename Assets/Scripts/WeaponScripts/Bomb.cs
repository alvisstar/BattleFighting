using UnityEngine;
using System.Collections;

public class Bomb : Weapon {	
	
	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	public override void OnAttack()
	{
		if(characterTransform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1)//&& GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BombAttack"))
		{
			characterTransform.GetComponent<PlayerControler>().SetAnimationAttack();
			GameObject throwingBomb = Instantiate(bombPrefabs,equipTransform.position + new Vector3(0, 0.5f, 0), equipTransform.rotation) as GameObject;
			//throwingBomb.transform.Rotate(0,180,0);
			throwingBomb.GetComponent<ThrowingBomb> ().characterTransform = characterTransform;
			ThrowingBomb script = throwingBomb.GetComponent<ThrowingBomb> ();
			
			
			equipTransform.rotation = characterTransform.rotation;
			script.Init (equipTransform.forward);
			characterTransform.GetComponent<PlayerControler>().FinishAttack();


		}

	}
}
