﻿using UnityEngine;
using System.Collections;

//using System.Collections.Generic;

public class SamuraiSkill : Skill
{

	// skill 1
	private float currentTimeSkill1 = 0;
	private bool isBeingActiveSkill1 = false;
	private Vector3 startPos;
	public float rangeOfSkill1 = 10;
	public float timeOfSkill1 = 0.5f;

	// skill 2
	private float currentTimeSkill2 = 0;
	private bool isBeingActiveSkill2 = false;
	public float rangeOfSkill2 = 5;
	public float timeOfSkill2 = 7.0f;
	public GameObject prefabSkill2;
	private GameObject skill2Object;
	private float currentTimeAddForce = 0.0f;
//	private List<int> hashOfEffectedBot;

	// Use this for initialization
	void Start ()
	{
		_player = GetComponent<PlayerControler> ();	
		_gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		// skill 1
		if (isBeingActiveSkill1) {
			currentTimeSkill1 += Time.deltaTime;

			if (Vector3.Distance (startPos, GetComponent<Rigidbody> ().position) >= rangeOfSkill1
				|| currentTimeSkill1 >= timeOfSkill1) {
				isBeingActiveSkill1 = false;

				_player.DisableTrail ();
				_player.setAllowControl (true);
				_player._animator.SetBool ("IsSkill1", false);
				_player._animator.SetBool ("IsSkill", false);

				GetComponent<Rigidbody> ().velocity = Vector3.zero;
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
				isBeingActiveSkill2 = false;
				Destroy (skill2Object);
			}

			// processing power of skill 2
			if (currentTimeAddForce > 0.25f) {
				currentTimeAddForce = 0;
				foreach (BotControler bot in _gameManager.botScripts) {
					if (Vector3.Distance (bot.GetComponent<Rigidbody> ().position, GetComponent<Rigidbody> ().position) <= rangeOfSkill2) {
						bot.BeHitted ();
						bot.GetComponent<Rigidbody> ().AddForce (-transform.forward * 10, ForceMode.Impulse);
//					Debug.Log(bot.GetHashCode());
					}
				}
			}
		} else {
			currentTimeSkill2 = 0;
			currentTimeAddForce = 0;
		}
	}

	public override void activeSkill1 ()
	{
		if (isBeingActiveSkill1)
			return;

		base.activeSkill1 ();

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
		
		isBeingActiveSkill2 = true;
		skill2Object = Instantiate (prefabSkill2);
	}
}
