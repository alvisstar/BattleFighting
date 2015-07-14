using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaseState : FSMState
{

	public ChaseState (AICharacterManager controller1)
	{ 
		controller = controller1;
		stateID = FSMStateID.Chasing;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
	
	}
	
	public override void Reason (Transform player, Transform npc)
	{
		//Set the target position as the player position
		destPos = player.position;
		
		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance (npc.position, destPos);
		float range = 3.5f;
		if (npc.GetComponent<Equipment> ()._weapon != null) {
			range = npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().rangeAttack;
		}

		List<GameObject> listItem = controller.GetListNearItem (npc);

		int index = checkBestItem (listItem, npc);
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

			
		} 
		else if (dist <= range) 
		{
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.ReachPlayer);
			
		}
		else if (dist > 20) 
		{
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

	private Vector3 steer (Transform npc)
	{  
		Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
		Vector3 center = flock.flockCenter - npc.localPosition;  // cohesion
		Vector3 velocity = flock.flockVelocity - npc.GetComponent<Rigidbody> ().velocity;  // alignment
		Vector3 follow = npc.GetComponent<PlayerControler> ().targetObject.transform.localPosition - npc.localPosition;  // follow leader
		Vector3 separation = Vector3.zero;
		foreach (PlayerControler player in flock.botScripts) {     
			if (player != npc.GetComponent<PlayerControler> ()) {        
				Vector3 relativePos = npc.localPosition - player.transform.localPosition;
				separation += relativePos / (relativePos.sqrMagnitude);    
				
			}    
		}
		// randomize    
		Vector3 randomize = new Vector3 ((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize ();
		return (flock.centerWeight * center + 
			flock.velocityWeight * velocity + flock.separationWeight * separation + 
			flock.followWeight * follow + flock.randomizeWeight * randomize);  
	} 
}
