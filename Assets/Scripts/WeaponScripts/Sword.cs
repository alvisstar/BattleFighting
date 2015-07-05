using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	// Use this for initialization

	public Transform characterTransform;
	public Transform equipTransform;
	public Xft.XWeaponTrail trail;
	void Start () {
		numberOfWeapon = -1;

		trail.Deactivate ();
		//trail.Init ();

	}

	public override void OnAttack()
	{
		

	}
	void OnTriggerEnter (Collider other) {
		
		
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.gameObject.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack")) {

			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)  *10;			
			other.gameObject.GetComponent<BotControler>().BeHitted();


			
		}
	}
}
