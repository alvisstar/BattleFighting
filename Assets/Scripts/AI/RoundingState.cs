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
		if (Vector3.Distance(npc.position, player.position) > 3.0f)
		{
			//Debug.Log("Switch to Chase State");
			npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
		}
		else
			if (Vector3.Distance(npc.position, player.position) <=3.0f)
			{
				if(controller.GetNumberBotAttackPlayer() < 2)	
					npc.GetComponent<BotControler>().PerformTransition(Transition.SawPlayer);
			}
	}
	
	public override void Act(Transform player, Transform npc)
	{
		//Find another random patrol point if the current point is reached
		/*Vector3 relativePos = steer (npc) * Time.deltaTime*10;
		
		npc.GetComponent<BotControler>().RotateByDirection (relativePos);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		if (relativePos != Vector3.zero)          
			npc.GetComponent<Rigidbody> ().velocity = relativePos;
		// enforce minimum and maximum speeds for the boids      
		float speed = npc.GetComponent<Rigidbody> ().velocity.magnitude;      
		if (speed > controller.maxVelocity) {        
			npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * controller.maxVelocity;      
		} else if (speed < controller.minVelocity) {        
			npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * controller.minVelocity;     
		}    */

		var q = npc.GetComponent<BotControler>().transform.rotation;
		npc.GetComponent<BotControler>().transform.RotateAround(player.position, Vector3.forward, 20*Time.deltaTime);
		npc.GetComponent<BotControler>().transform.rotation = q;

	}
	private Vector3 steer (Transform npc) {    
		Vector3 center = controller.flockCenter -         npc.localPosition;  // cohesion
		Vector3 velocity = controller.flockVelocity -         npc.GetComponent<Rigidbody>().velocity;  // alignment
		Vector3 follow = controller.target.localPosition -         npc.localPosition;  // follow leader
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
