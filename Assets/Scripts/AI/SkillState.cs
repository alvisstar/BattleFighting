using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillState : FSMState
{
	float timeToAttack;
	bool usedSkill;
	public SkillState (AICharacterManager controller1)
	{ 
		controller = controller1;
		stateID = FSMStateID.Skill;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
		
	}
	public override void ReInit ()
	{
		hpDecrease = 0;
		timeToAttack = 2;
		usedSkill = false;
	}
	public override void Reason (Transform player, Transform npc)
	{
		//Check the distance with the player tank
		timeToAttack -= Time.deltaTime;
		float dist = Vector3.Distance (npc.position, player.position);
		float maxRange = 3.5f;
		float minRange = 1f;
		if (npc.GetComponent<Equipment> ()._weapon != null) {
			maxRange = npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().maxRangeAttack;
			minRange = npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().minRangeAttack;
		}
		if (usedSkill && npc.GetComponent<PlayerControler> ().getAllowControl()) {
			npc.GetComponent<PlayerControler> ().PerformTransition(Transition.SawPlayer);

		}
		
		
	}
	

	
	public override void Act (Transform player, Transform npc)
	{
		Vector3 relativePos = player.position - npc.position;
		
		npc.GetComponent<PlayerControler> ().RotateByDirection (relativePos);
		npc.GetComponent<PlayerControler> ()._playerSkill.activeSkill1 ();
		usedSkill = true;

	}
}
