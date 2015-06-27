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
		if (dist > 3.0f)
		{
			//Rotate to the target point

			npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}
		//Transition to patrol is the tank become too far
		else if (dist <= 3.0f)
		{
			if(controller.GetNumberBotAttackPlayer() >=2)	
				npc.GetComponent<BotControler>().PerformTransition(Transition.InclosurePlayer);
		}  
	}
	
	public override void Act(Transform player, Transform npc)
	{
		Vector3 relativePos = player.position - npc.position ;
		
		npc.GetComponent<BotControler>().RotateByDirection (relativePos);
		npc.GetComponent<BotControler> ().AttackTarget ();
	}
}
