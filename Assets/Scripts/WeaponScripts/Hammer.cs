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
		
		characterTransform.GetComponent<PlayerControler>().SetAnimationAttack();
		
		characterTransform.GetComponent<PlayerControler>().FinishAttack();
		
		
		
		
	}
	void OnTriggerEnter (Collider other) {

		
		if (other.gameObject.tag == "Bot" || other.gameObject.tag == "Player") {

			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward  *30;				

		}
	}
}
