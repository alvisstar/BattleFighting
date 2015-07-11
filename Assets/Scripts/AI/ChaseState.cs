using UnityEngine;
using System.Collections;

public class ChaseState : FSMState
{

	public ChaseState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Chasing;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		
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
		if (dist <= range)
		{

				npc.GetComponent<PlayerControler>().PerformTransition(Transition.ReachPlayer);
			
		}

			
	}
	
	public override void Act(Transform player, Transform npc)
	{
		//Rotate to the target point
		//if (npc.GetComponent<PlayerControler> ().recoveryTime <= 0) {
			Vector3 relativePos = steer (npc) * Time.deltaTime;
		
			npc.GetComponent<PlayerControler> ().RotateByDirection (relativePos);
			npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
			if (relativePos != Vector3.zero)          
				npc.GetComponent<Rigidbody> ().velocity = relativePos;
			// enforce minimum and maximum speeds for the boids      
			float speed = npc.GetComponent<Rigidbody> ().velocity.magnitude;      
			if (speed > controller.maxVelocity) {        
				npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * controller.maxVelocity;      
			} else if (speed < controller.minVelocity) {        
				npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * controller.minVelocity;     
			}    
		//} else {
		//	npc.GetComponent<Animator> ().SetFloat ("Speed", 0);
		//}
	
	}
	private Vector3 steer (Transform npc) {    
		Vector3 center = controller.flockCenter -         npc.localPosition;  // cohesion
		Vector3 velocity = controller.flockVelocity -         npc.GetComponent<Rigidbody>().velocity;  // alignment
		Vector3 follow = controller.target.localPosition -         npc.localPosition;  // follow leader
		Vector3 separation = Vector3.zero;
		foreach (PlayerControler flock in controller.botScripts) {     
			if (flock != npc.GetComponent<PlayerControler>()) {        
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
