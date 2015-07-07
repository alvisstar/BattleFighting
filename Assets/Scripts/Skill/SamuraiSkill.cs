using UnityEngine;
using System.Collections;

public class SamuraiSkill : Skill {

	// Use this for initialization
	void Start ()
	{
		_player = GetComponent<PlayerControler> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Rigidbody> ().velocity ==Vector3.zero ) {
			_player.DisableTrail();
			_player.setAllowControl(true);
			_player._animator.SetBool ("IsSkill1",false);
			_player._animator.SetBool ("IsSkill",false);

		}
	}

	public override void activeSkill1 ()
	{
		base.activeSkill1 ();
		GetComponent<Rigidbody>().AddForce(transform.forward*400,ForceMode.Impulse);
		_player.ActiveTrail ();
	}

	public override void activeSkill2 ()
	{
		base.activeSkill2 ();
	}
}
