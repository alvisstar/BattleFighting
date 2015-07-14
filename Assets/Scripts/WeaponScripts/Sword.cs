using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	// Use this for initialization

	public Transform characterTransform;
	public Transform equipTransform;
	public GameObject prefabHit;
	public Xft.XWeaponTrail trail;
	void Start () {
		numberOfWeapon = -1;
		delayTime = 0;
		trail.Deactivate ();
		maxRangeAttack = 4;
		minRangeAttack = 1;
		piority = 1000;
		//trail.Init ();

	}

	public override void OnAttack()
	{
		

	}

	void OnCollisionEnter(Collision other) {
		if(characterTransform!=null)
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.gameObject.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack") 
			   && characterTransform.gameObject.GetComponent<PlayerControler>().onTrigger) {
			Vector3 pos = other.contacts[0].point;
			
			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)  *0.15f  *60;			
			other.gameObject.GetComponent<PlayerControler>().BeHitted();
			GameObject explosionIceBall = Instantiate(prefabHit,pos,Quaternion.identity) as GameObject;
	}
	}
}
