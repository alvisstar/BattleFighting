using UnityEngine;
using System.Collections;

public class SamuraiSkill : Skill {

	private float currentTime = 0;
	private bool isBeingActiveSkill1 = false;
	private Vector3 startPos;
	public float rangeOfSkill1 = 10;
	public float timeOfSkill1 = 0.5f;

	// Use this for initialization
	void Start ()
	{
		_player = GetComponent<PlayerControler> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (isBeingActiveSkill1) {
			currentTime += Time.deltaTime;

			if (Vector3.Distance (startPos, GetComponent<Rigidbody> ().position) >= rangeOfSkill1
			    || currentTime >= timeOfSkill1) {
				isBeingActiveSkill1 = false;

				_player.DisableTrail ();
				_player.setAllowControl (true);
				_player._animator.SetBool ("IsSkill1", false);
				_player._animator.SetBool ("IsSkill", false);

				GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		} else {
			currentTime = 0;
		}
	}

	public override void activeSkill1 ()
	{
		base.activeSkill1 ();
		startPos = GetComponent<Rigidbody> ().position;
		isBeingActiveSkill1 = true;
		GetComponent<Rigidbody>().AddForce(transform.forward*400,ForceMode.Impulse);
		_player.ActiveTrail ();
	}

	public override void activeSkill2 ()
	{
		base.activeSkill2 ();
	}
}
