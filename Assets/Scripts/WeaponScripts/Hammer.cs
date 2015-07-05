using UnityEngine;
using System.Collections;

public class Hammer : Weapon {
	
	// Use this for initialization
	
	public Transform characterTransform;
	public Transform equipTransform;
	void Start () {
		numberOfWeapon = -1;
		
	}
	
	public override void OnAttack()
	{
		

		
		
		
	}
	void OnTriggerEnter (Collider other) {

		
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("HammerAttack")) {
			
			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)  *25;			
			other.gameObject.GetComponent<BotControler>().BeHitted();
			
			
			
		}
	}

}
