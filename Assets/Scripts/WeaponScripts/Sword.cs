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

		trail.Deactivate ();
		//trail.Init ();

	}

	public override void OnAttack()
	{
		

	}
	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.gameObject.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack")) {
			//Vector3 pos = other.contacts[0].point;
			
			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)*0.15f  *80;			
			other.gameObject.GetComponent<BotControler>().BeHitted();
			GameObject explosionIceBall = Instantiate(prefabHit,other.transform.position,Quaternion.identity) as GameObject;
		}
	}
	void OnCollisionEnter(Collision other) {
		if(characterTransform!=null)
		if ((other.gameObject.tag == "Bot" || other.gameObject.tag == "Player" )&& characterTransform.gameObject.GetComponent<PlayerControler>()._animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack")) {
			Vector3 pos = other.contacts[0].point;
			
			other.gameObject.GetComponent<Rigidbody> ().velocity = other.gameObject.transform.forward*(-1)  *0.15f  *60;			
			other.gameObject.GetComponent<BotControler>().BeHitted();
			GameObject explosionIceBall = Instantiate(prefabHit,pos,Quaternion.identity) as GameObject;
	}
	}
}
