using UnityEngine;
using System.Collections;

public class Hammer : Weapon {
	
	// Use this for initialization
	
	public Transform characterTransform;
	public Transform equipTransform;
	void Start () {
		numberOfWeapon = -1;
		rangeAttack = 4;
		piority = 400;
	}
	
	public override void OnAttack()
	{
		

		
		
		
	}
	void OnCollisionEnter(Collision other) {
		if(characterTransform!=null)
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.gameObject.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("HammerAttack")) {
			Vector3 pos = other.contacts[0].point;
			
			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)  *0.15f  *100;			
			other.gameObject.GetComponent<PlayerControler>().BeHitted();
			//GameObject explosionIceBall = Instantiate(prefabHit,pos,Quaternion.identity) as GameObject;
		}
	}

}
