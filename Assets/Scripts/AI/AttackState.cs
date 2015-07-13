using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackState : FSMState
{
	public AttackState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Attacking;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
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
		List<GameObject> listItem = controller.GetListNearItem (npc);

		if (npc.GetComponent<PlayerControler> ().targetObject == null) {
			npc.GetComponent<PlayerControler>().PerformTransition(Transition.NoTarget);
		}
		else
		if(hpDecrease>=5 && npc.GetComponent<PlayerControler>().targetObject.GetComponent<PlayerControler>().CurrentStateID == FSMStateID.Attacking)
		{
			//npc.GetComponent<PlayerControler>().targetObject = null;
			npc.GetComponent<PlayerControler>().PerformTransition(Transition.LowHp);
		}
		else
		if (listItem.Count > 0) {
			npc.GetComponent<PlayerControler>().focusItem = true;
			npc.GetComponent<PlayerControler>().itemToTake = listItem[0];
			if(npc.GetComponent<PlayerControler> ().targetObject!=null)
			{
				Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
				npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove(npc.GetComponent<PlayerControler> ());
			}
			npc.GetComponent<PlayerControler>().targetObject = listItem[0];
			npc.GetComponent<PlayerControler>().targetObject.GetComponent<Flock> ().botScripts.Add(npc.GetComponent<PlayerControler> ());
			controller.target =listItem[0].transform;
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
