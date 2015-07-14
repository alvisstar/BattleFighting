﻿using UnityEngine;
using System.Collections;

//using System.Collections.Generic;

public class SamuraiSkill : Skill
{

	// skill 1

//	private List<int> hashOfEffectedBot;

	// Use this for initialization
	void Start ()
	{
		_player = GetComponent<PlayerControler> ();	
		_gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager> ();
		delayTimeSkill1 = 10f;
		delayTimeSkill2 = 20f;
		timeOfSkill2 = 5;
		isUsedSkill1 = false;
		isUsedSkill2 = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isUsedSkill1) {
			delayTimeSkill1 -= Time.deltaTime;
			if(delayTimeSkill1 <=0)
			{
				delayTimeSkill1 =10;
				isUsedSkill1 = false;
			}
		}
		if (isUsedSkill2) {
			delayTimeSkill2 -= Time.deltaTime;
			if(delayTimeSkill1 <=0)
			{
				delayTimeSkill1 =20;
				isUsedSkill2 = false;
			}
		}

		// skill 1
		if (isBeingActiveSkill1) {

			currentTimeSkill1 += Time.deltaTime;

			if (Vector3.Distance (startPos, GetComponent<Rigidbody> ().position) >= rangeOfSkill1
				|| currentTimeSkill1 >= timeOfSkill1) {
				isBeingActiveSkill1 = false;

				deActiveSkill1();
			}
		} else {
			currentTimeSkill1 = 0;
		}

		// skill 2
		if (isBeingActiveSkill2) {
			currentTimeSkill2 += Time.deltaTime;
			currentTimeAddForce += Time.deltaTime;
			skill2Object.transform.position = GetComponent<Rigidbody> ().position;

			// destroy skill 2 when time out
			if (currentTimeSkill2 >= timeOfSkill2) {
				deActiveSkill2();
			}

			// processing power of skill 2
			if (currentTimeAddForce > 0.25f) {
				currentTimeAddForce = 0;
				foreach (PlayerControler bot in _gameManager.botScripts) {
					if (Vector3.Distance (bot.GetComponent<Rigidbody> ().position, GetComponent<Rigidbody> ().position) <= rangeOfSkill2) {
						bot.BeHitted ();
						bot.GetComponent<Rigidbody> ().AddForce (-bot.transform.forward * 10, ForceMode.Impulse);
//					Debug.Log(bot.GetHashCode());
					}
				}
			}
		} else {
			currentTimeSkill2 = 0;
			currentTimeAddForce = 0;
		}
	}

	public override void deActiveSkill1 ()
	{

		base.deActiveSkill1 ();
		_player.DisableTrail ();
		_player.setAllowControl (true);
		_player._animator.SetBool ("IsSkill1", false);
		_player._animator.SetBool ("IsSkill", false);
		
		GetComponent<Rigidbody> ().velocity = Vector3.zero;

	}
	
	public override void deActiveSkill2 ()
	{
			
		base.deActiveSkill2 ();
		isBeingActiveSkill2 = false;

		Destroy (skill2Object);
	}

	public override void activeSkill1 ()
	{
		if (isBeingActiveSkill1)
			return;

		base.activeSkill1 ();
		isUsedSkill1 = true;
		startPos = GetComponent<Rigidbody> ().position;
		isBeingActiveSkill1 = true;
		GetComponent<Rigidbody> ().AddForce (transform.forward * 400, ForceMode.Impulse);
		_player.ActiveTrail ();
	}

	public override void activeSkill2 ()
	{
		if (isBeingActiveSkill2)
			return;

		base.activeSkill2 ();
		isUsedSkill2 = true;
		_player.setAllowControl (false);
		_player._animator.SetTrigger (Animator.StringToHash("Skill2"));
		_player._animator.SetBool ("IsSkill",true);
		isBeingActiveSkill2 = true;
		skill2Object = Instantiate (prefabSkill2);
	}
	public override bool readyToSkill1() {
		if (delayTimeSkill1 == 10)
			return true;
		return false;

		
	}
	public override bool readyToSkill2() {
		
		if (delayTimeSkill2 == 20)
			return true;
		return false;
	}
	public override void finishAnimation ()
	{

		_player._animator.SetBool ("IsSkill", false);
		_player.setAllowControl (true);
	}
}
