using UnityEngine;
using System.Collections;

public class EscapeState : FSMState
{
	
	public EscapeState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Running;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
		
	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Set the target position as the player position
		destPos = player.position;
		
		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(npc.position, destPos);
		float range = 3.5f;
		if(npc.GetComponent<Equipment> ()._weapon !=null)
		{
			range=npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon>().rangeAttack;
		}
		

			if (dist > 20)
		{
			//npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove(npc.GetComponent<PlayerControler> ());
			npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawPlayer);
			
		}
		
		
	}
	
	public override void Act(Transform player, Transform npc)
	{

		Vector3 relativePos = npc.position + new Vector3 (10, 0, 10);
		npc.GetComponent<PlayerControler> ().RotateByDirection (relativePos);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		npc.GetComponent<Rigidbody> ().velocity = relativePos.normalized * 0.15f*50;  

		
	}

}
