using UnityEngine;
using System.Collections;

public class PickItemState : FSMState
{
	public PickItemState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.TakingItem;
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		
		//find next Waypoint position
		
	}
	
	public override void Reason(Transform player, Transform npc)
	{
		//Check the distance with the player tank
		float dist = Vector3.Distance(npc.position, player.position);
		if (npc.GetComponent<PlayerControler> ().focusItem == false)
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawPlayer);
		
	}
	

	public override void Act(Transform player, Transform npc)
	{
		//Rotate to the target point
		//if (npc.GetComponent<PlayerControler> ().recoveryTime <= 0) {
		Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
		Vector3 relativePos = steer (npc) * Time.deltaTime;
		
		npc.GetComponent<PlayerControler> ().RotateByDirection (relativePos);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		if (relativePos != Vector3.zero)          
			npc.GetComponent<Rigidbody> ().velocity = relativePos;
		// enforce minimum and maximum speeds for the boids      
		float speed = npc.GetComponent<Rigidbody> ().velocity.magnitude;      
		if (speed > flock.maxVelocity) {        
			npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * flock.maxVelocity;      
		} else if (speed < flock.minVelocity) {        
			npc.GetComponent<Rigidbody> ().velocity = npc.GetComponent<Rigidbody> ().velocity.normalized * flock.minVelocity;     
		}    
		//} else {
		//	npc.GetComponent<Animator> ().SetFloat ("Speed", 0);
		//}
		
	}
	private Vector3 steer (Transform npc) {  
		Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
		Vector3 center = flock.flockCenter -         npc.localPosition;  // cohesion
		Vector3 velocity = flock.flockVelocity -         npc.GetComponent<Rigidbody>().velocity;  // alignment
		Vector3 follow = npc.GetComponent<PlayerControler> ().targetObject.transform.localPosition -         npc.localPosition;  // follow leader
		Vector3 separation = Vector3.zero;
		foreach (PlayerControler player in flock.botScripts) {     
			if (player != npc.GetComponent<PlayerControler>()) {        
				Vector3 relativePos = npc.localPosition -             player.transform.localPosition;
				separation += relativePos / (relativePos.sqrMagnitude);    
				
			}    
		}
		// randomize    
		Vector3 randomize = new Vector3( (Random.value * 2) - 1,         (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize();
		return (flock.centerWeight * center +         
		        flock.velocityWeight * velocity +         flock.separationWeight * separation +        
		        flock.followWeight * follow +         flock.randomizeWeight * randomize);  
	} 
}
