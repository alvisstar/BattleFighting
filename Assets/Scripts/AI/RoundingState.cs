using UnityEngine;
using System.Collections;

public class RoundingState : FSMState {

	// Use this for initialization

	public RoundingState(AIBotManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Rounding;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Check the distance with player tank
		//When the distance is near, transition to chase state
		destPos = player.position;
		
		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(npc.position, destPos);

		if (dist > 10.0f)
		{
			//Debug.Log("Switch to Chase State");
			npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}

	}
	
	public override void Act(Transform player, Transform npc)
	{
		//Find another random patrol point if the current point is reached
		Vector3 relativePos = player.position - npc.position ;
		npc.GetComponent<BotControler>().RotateByDirection (relativePos);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 0);

	}
	private Vector3 steer (Transform npc) {    
		Vector3 center = controller.flockCenter -         npc.localPosition;  // cohesion
		Vector3 velocity = controller.flockVelocity -         npc.GetComponent<Rigidbody>().velocity;  // alignment
		float random = Random.Range (-5, 5);
		Vector3 a =controller.target.localPosition + new Vector3(random,0,random);
		Vector3 follow = a -         npc.localPosition;  // follow leader
		Vector3 separation = Vector3.zero;
		foreach (BotControler flock in controller.botScripts) {     
			if (flock != npc.GetComponent<BotControler>()) {        
				Vector3 relativePos = npc.localPosition -             flock.transform.localPosition;
				separation += relativePos / (relativePos.sqrMagnitude);    
				
			}    
		}
		// randomize    
		Vector3 randomize = new Vector3( (Random.value * 2) - 1,         (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize();
		return (controller.centerWeight * center +         
		        controller.velocityWeight * velocity +         controller.separationWeight * separation +        
		        controller.followWeight * follow +         controller.randomizeWeight * randomize);  
	} 
}
