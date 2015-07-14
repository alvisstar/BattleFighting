using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : FSMState
{
	float timeToChangeDirection;
	bool isBack;
	GameObject map;

	public PatrolState (AICharacterManager controller1)
	{ 
		controller = controller1;
		stateID = FSMStateID.Patrolling;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
		timeToChangeDirection = 0;
		isBack = false;
		map = GameObject.Find("Ground");
	}
	
	public override void Reason (Transform player, Transform npc)
	{
		List<PlayerControler> list = controller.GetListNearPlayer (npc);
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

			
		} else
			if (list.Count > 0) {
			npc.GetComponent<PlayerControler> ().targetObject = list [0].gameObject;
			list [0].GetComponent<Flock> ().botScripts.Add (npc.GetComponent<PlayerControler> ());
			npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawPlayer);

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


	
		timeToChangeDirection -= Time.deltaTime;
		
		if (timeToChangeDirection <= 0) {
			ChangeDirection (npc);
		} else {

			if(npc.GetComponent<PlayerControler> ().transform.position.x< -map.GetComponent<Renderer>().bounds.size.x/2 +5
			   ||npc.GetComponent<PlayerControler> ().transform.position.x>map.GetComponent<Renderer>().bounds.size.x/2 -5
			   ||npc.GetComponent<PlayerControler> ().transform.position.z< -map.GetComponent<Renderer>().bounds.size.z/2 +5
			   ||npc.GetComponent<PlayerControler> ().transform.position.z>map.GetComponent<Renderer>().bounds.size.z/2 -5)
			{
				if(!isBack)
				{
					npc.forward=-npc.forward;
					timeToChangeDirection =1.5f;
					isBack = true;
				}


			}
		}

		npc.GetComponent<PlayerControler> ().RotateByDirection (npc.forward);
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
	
		npc.GetComponent<Rigidbody> ().velocity = npc.forward * 0.15f*50;  
		
		
	}

	private void ChangeDirection (Transform npc)
	{
		float angle = Random.Range (0f, 360f);
		Quaternion quat = Quaternion.AngleAxis (angle, Vector3.one);
		Vector3 newUp = quat * Vector3.forward;
		newUp.y = 0;
		newUp.Normalize ();
		npc.forward = newUp;
		timeToChangeDirection = Random.Range(1.5f,3f);
		isBack = false;

	}
	
}
