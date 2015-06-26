using UnityEngine;
using System.Collections;

public class RoundingState : FSMState {

	// Use this for initialization
	protected AIBotManager controller;
	public RoundingState(AIBotManager controller) 
	{ 
		controller = controller;
		stateID = FSMStateID.Rounding;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Check the distance with player tank
		//When the distance is near, transition to chase state
		if (Vector3.Distance(npc.position, player.position) <= 300.0f)
		{
			//Debug.Log("Switch to Chase State");
			//npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}
	}
	
	public override void Act(Transform player, Transform npc)
	{
		//Find another random patrol point if the current point is reached
		

	}

}
