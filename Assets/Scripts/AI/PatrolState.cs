using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : FSMState
{
	float timeToChangeDirection;
	bool isBack;
	GameObject map;
	Vector3 ds;
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
	public override void ReInit ()
	{
		hpDecrease = 0;
	}
	public override void Reason (Transform player, Transform npc)
	{
		List<PlayerControler> list = controller.GetListNearPlayer (npc);
		List<GameObject> listItem = controller.GetListNearItem (npc);
		float maxRange = 3.5f;
		float minRange = 1f;
		if (npc.GetComponent<Equipment> ()._weapon != null) {
			maxRange = npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().maxRangeAttack;
			minRange = npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon> ().minRangeAttack;
		}
		int index = checkBestItem (listItem, npc);
		int indexEnemy = checkWeakestEnemy (list, npc);
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
		else if (indexEnemy!=-1) 
		{
			npc.GetComponent<PlayerControler> ().targetObject = list [indexEnemy].gameObject;
			list [indexEnemy].GetComponent<Flock> ().botScripts.Add (npc.GetComponent<PlayerControler> ());
			float dist= Vector3.Distance(npc.GetComponent<PlayerControler> ().targetObject.transform.position,npc.transform.position);
			if (dist >= (maxRange +minRange)/2) 
			{

				npc.GetComponent<PlayerControler> ().PerformTransition (Transition.SawPlayer);
			}

		}
		


		
	}
	int checkWeakestEnemy (List<PlayerControler> enemy, Transform npc)
	{
		int index = 0;
		float min = 1000;
		if (enemy.Count == 0)
			return -1;
		for (int i =0; i< enemy.Count; i++) {
			if (enemy [i].hp <min) {
				index = i;
				min = enemy [i].hp;
			}
			
		}

		return index;
		
		
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
					//Quaternion quat = Quaternion.AngleAxis (360, Vector3.back);
					//Vector3 newUp = quat * Vector3.forward;
					//newUp.y = 0;
					//newUp.Normalize ();
					//npc.forward = newUp;
					//npc.forward=-npc.forward;
					ds = new Vector3 (Random.Range (-npc.position.x, npc.position.x+10), 0, Random.Range (- npc.position.z,  npc.position.z+10));
					ds =ds - npc.position;
					ds.y = 0;
					timeToChangeDirection = Random.Range(2f,5f);
					isBack = true;
				}


			}
		}
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		npc.rotation = Quaternion.Lerp (npc.rotation,  Quaternion.LookRotation(ds ), Time.deltaTime * 4);
		npc.GetComponent<Rigidbody> ().velocity = (ds ).normalized * 0.15f*60;  
		
		
	}

	private void ChangeDirection (Transform npc)
	{


		ds = new Vector3 (Random.Range (-map.GetComponent<Renderer>().bounds.size.x/2 +5,map.GetComponent<Renderer>().bounds.size.x/2 -5)
		                  , 0, Random.Range (-map.GetComponent<Renderer>().bounds.size.z/2 +5,map.GetComponent<Renderer>().bounds.size.z/2 -5));
		ds =ds - npc.position;
		ds.y = 0;
		timeToChangeDirection = Random.Range(2f,5f);
		isBack = false;

	}
	
}
