using UnityEngine;
using System.Collections;

public class Bomb : Weapon {	
	
	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	
	public override void OnAttack()
	{
		GameObject throwingBomb = Instantiate(bombPrefabs, characterTransform.position + new Vector3(0, 2.0f, 0), characterTransform.rotation) as GameObject;
		throwingBomb.transform.Rotate(0,180,0);
		ThrowingBomb script = throwingBomb.GetComponent<ThrowingBomb> ();
		script.Init (characterTransform.forward);
	}
}
