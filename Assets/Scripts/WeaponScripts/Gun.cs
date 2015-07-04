using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Gun : Weapon {

	// Use this for initialization
	public GameObject bulletPrefabs;
	public Transform characterTransform;
	public Transform equipTransform;
	void Start () {
		numberOfWeapon = 30;
		delayTime = 0.2f;

	}
	
	public override void OnAttack()
	{
		numberOfWeapon--;
		GameObject bullet = Instantiate(bulletPrefabs, equipTransform.position, equipTransform.rotation) as GameObject;
		//bullet.transform.position += new Vector3 (0, 2, 0);
		bullet.transform.Rotate(0,180,0);
		Bullet script = bullet.GetComponent<Bullet> ();
		script.Init (characterTransform.forward);

	}
}
