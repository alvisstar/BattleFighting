using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{
	public AttackState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Attacking;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		
		//find next Waypoint position

	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Check the distance with the player tank
		float dist = Vector3.Distance(npc.position, player.position);
		float range = 3.5f;
		if(npc.GetComponent<Equipment> ()._weapon !=null)
		{
			range=npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon>().rangeAttack;
		}
		float distItem =-1;
		if(GameObject.Find("Item(Clone)")!=null)
			distItem = Vector3.Distance(npc.position,GameObject.Find("Item(Clone)").transform.position);
		if (distItem !=-1 && distItem < 10) {
			npc.GetComponent<PlayerControler>().focusItem = true;
			npc.GetComponent<PlayerControler>().itemToTake = GameObject.Find("Item(Clone)");
			npc.GetComponent<PlayerControler>().targetObject = GameObject.Find("Item(Clone)");
			controller.target = GameObject.Find("Item(Clone)").transform;
			npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawItem);
		}
		else
		if (dist > range)
		{
			//Rotate to the target point

			npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawPlayer);
		}

	}
	
	public override void Act(Transform player, Transform npc)
	{
		Vector3 relativePos = player.position - npc.position ;
		
		npc.GetComponent<PlayerControler>().RotateByDirection (relativePos);
		//if(npc.GetComponent<BotControler>().recoveryTime <= 0)
			npc.GetComponent<PlayerControler> ().AttackTarget ();
	}
}
