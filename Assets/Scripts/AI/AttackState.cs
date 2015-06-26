using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{	protected AIBotManager controller;
	public AttackState(AIBotManager controller) 
	{ 
		controller = controller;
		stateID = FSMStateID.Attacking;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		
		//find next Waypoint position

	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Check the distance with the player tank
		float dist = Vector3.Distance(npc.position, player.position);
		if (dist >= 2.0f)
		{
			//Rotate to the target point

			npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}
		//Transition to patrol is the tank become too far
		else if (dist < 50.0f)
		{
			Debug.Log("Switch to Patrol State");
			//npc.GetComponent<BotControler>().PerformTransition(Transition.LostPlayer);
		}  
	}
	
	public override void Act(Transform player, Transform npc)
	{
		//Set the target position as the player position
		destPos = player.position;
		
		//Always Turn the turret towards the player
	
		
		//Shoot bullet towards the player
		npc.GetComponent<BotControler> ().AttackTarget ();
	}
}
