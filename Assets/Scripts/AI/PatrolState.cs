using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PatrolState : FSMState
{
	float timeToChangeDirection;
	public PatrolState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Patrolling;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
		timeToChangeDirection = 0;
	}
	
	public override void Reason(Transform player, Transform npc)
	{
		List<PlayerControler> list = controller.GetListNearPlayer (npc);
		float distItem =-1;
		if(GameObject.Find("Item(Clone)")!=null)
			distItem = Vector3.Distance(npc.position,GameObject.Find("Item(Clone)").transform.position);
		if (distItem !=-1 && distItem < 10) {
			npc.GetComponent<PlayerControler>().focusItem = true;
			npc.GetComponent<PlayerControler>().itemToTake = GameObject.Find("Item(Clone)");
			if(npc.GetComponent<PlayerControler> ().targetObject!=null)
			{
				npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove(npc.GetComponent<PlayerControler> ());
			}
			npc.GetComponent<PlayerControler>().targetObject = GameObject.Find("Item(Clone)");
			npc.GetComponent<PlayerControler>().targetObject.GetComponent<Flock> ().botScripts.Add(npc.GetComponent<PlayerControler> ());
			controller.target = GameObject.Find("Item(Clone)").transform;
			npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawItem);

		}
		else
			if (list.Count > 0) {
				npc.GetComponent<PlayerControler>().targetObject = list[0].gameObject;
				list[0].GetComponent<Flock> ().botScripts.Add (npc.GetComponent<PlayerControler>());
				npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawPlayer);

			}
		


		
	}
	
	public override void Act(Transform player, Transform npc)
	{
		float n = Random.Range (-1, 1);
		Vector3 relativePos =new Vector3 (n , 0, n)+ npc.position ;
		npc.GetComponent<PlayerControler> ().RotateByDirection (npc.forward);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		timeToChangeDirection -= Time.deltaTime;
		
		if (timeToChangeDirection <= 0) {
			ChangeDirection(npc);
		}
		
		npc.GetComponent<Rigidbody> ().velocity  = npc.forward  * 0.15f*50;
	
		//npc.GetComponent<Rigidbody> ().velocity = relativePos.normalized * 0.15f*50;  
		
		
	}
	private void ChangeDirection(Transform npc) {
		float angle = Random.Range(0f, 360f);
		Quaternion quat = Quaternion.AngleAxis(angle, Vector3.one);
		Vector3 newUp = quat * Vector3.forward;
		newUp.y = 0;
		newUp.Normalize();
		npc.forward = newUp;
		timeToChangeDirection = 1.5f;
	}
	
}
