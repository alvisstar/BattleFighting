using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LongBowScript : Weapon {

	// Use this for initialization

	public GameObject arrowPrefabs;
	public Transform characterTransform;
	void Start () {
		numberOfWeapon = 10;
		delayTime = 0.5f;
	}

	public override void OnAttack()
	{
		GameObject arrow = Instantiate(arrowPrefabs, characterTransform.position, characterTransform.rotation) as GameObject;
		arrow.transform.position += new Vector3 (0, 2, 0);
		arrow.transform.Rotate(0,180,0);
		Arrow script = arrow.GetComponent<Arrow> ();
		script.Init (characterTransform.forward);
	}
}
