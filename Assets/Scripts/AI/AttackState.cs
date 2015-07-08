using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{
	public AttackState(AIBotManager controller1) 
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
		if (dist > 3.5f)
		{
			//Rotate to the target point

			npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}

	}
	
	public override void Act(Transform player, Transform npc)
	{
		Vector3 relativePos = player.position - npc.position ;
		
		npc.GetComponent<BotControler>().RotateByDirection (relativePos);
		//if(npc.GetComponent<BotControler>().recoveryTime <= 0)
			//npc.GetComponent<BotControler> ().AttackTarget ();
	}
}
