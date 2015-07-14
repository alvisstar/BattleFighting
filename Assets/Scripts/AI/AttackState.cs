using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackState : FSMState
{
	float timeToAttack;
	public AttackState (AICharacterManager controller1)
	{ 
		controller = controller1;
		stateID = FSMStateID.Attacking;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position

	}
	public override void ReInit ()
	{
		hpDecrease = 0;
		timeToAttack = 2;
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
		List<GameObject> listItem = controller.GetListNearItem (npc);
		int index = checkBestItem (listItem, npc);
		if (npc.GetComponent<PlayerControler> ().targetObject == null) {
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.NoTarget);
		} else
		if (hpDecrease >= 5 && npc.GetComponent<PlayerControler> ().targetObject.GetComponent<PlayerControler> ().CurrentStateID == FSMStateID.Attacking) {
			//npc.GetComponent<PlayerControler>().targetObject = null;
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.LowHp);
		} else
		

			if (index != -1) {
			npc.GetComponent<PlayerControler> ().focusItem = true;
			npc.GetComponent<PlayerControler> ().itemToTake = listItem [index];
			if (npc.GetComponent<PlayerControler> ().targetObject != null) {
				Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
				npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove (npc.GetComponent<PlayerControler> ());
			}
			npc.GetComponent<PlayerControler> ().targetObject = listItem [index];
			npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Add (npc.GetComponent<PlayerControler> ());
			controller.target = listItem [index].transform;
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawItem);

			
		} else if (dist > maxRange) {
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawPlayer);
		} else if (dist < minRange) {
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawPlayer);
		} else if (timeToAttack <= 0) {
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.NoTarget);
		}


	}

	int checkBestItem (List<GameObject> item, Transform npc)
	{
		int index = 0;
		int min = 1000;
		if (item.Count == 0)
			return -1;
		for (int i =0; i< item.Count; i++) {
			if (item [i].GetComponent<RandomItem> ().piority < min) {
				index = i;
				min = item [i].GetComponent<RandomItem> ().piority;
			}
			
		}
		if (npc.GetComponent<Equipment> ()._weapon != null) {
			if (item [index].GetComponent<RandomItem> ().piority >= npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().piority) {
				index = -1;
			}
		} 
		return index;
		
		
	}

	public override void Act (Transform player, Transform npc)
	{
		Vector3 relativePos = player.position - npc.position;
		
		npc.GetComponent<PlayerControler> ().RotateByDirection (relativePos);
		//if(npc.GetComponent<BotControler>().recoveryTime <= 0)
		npc.GetComponent<PlayerControler> ().AttackTarget ();
	}
}
