using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EscapeState : FSMState
{
	float timeToEscape;
	GameObject map;
	bool isBack;
	float timeToChangeDirection;
	Vector3 direction;
	public EscapeState(AICharacterManager controller1) 
	{ 
		controller = controller1;
		stateID = FSMStateID.Running;
		
		curRotSpeed = 1.0f;
		curSpeed = 100.0f;
		hpDecrease = 0;
		//find next Waypoint position
		timeToEscape = 0;
		map = GameObject.Find("Ground");

	}
	public override void ReInit ()
	{
		hpDecrease = 0;
		timeToEscape = 5.5f;
		timeToChangeDirection = 2;
		isBack = false;
	}
	public override void Reason(Transform player, Transform npc)
	{
		//Set the target position as the player position
		destPos = player.position;
		
		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(npc.position, destPos);
		float minRange = 3.5f;
		float maxRange = 3.5f;
		if(npc.GetComponent<Equipment> ()._weapon !=null)
		{
			minRange=npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon>().minRangeAttack;
			maxRange=npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon>().maxRangeAttack;
		}
		List<GameObject> listItem = controller.GetListNearItem (npc);

			int index =checkBestItem(listItem,npc);
			if(index !=-1)
			{
				npc.GetComponent<PlayerControler>().focusItem = true;
				npc.GetComponent<PlayerControler>().itemToTake = listItem[index];
				if(npc.GetComponent<PlayerControler> ().targetObject!=null)
				{
					Flock flock = npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ();
					npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove(npc.GetComponent<PlayerControler> ());
				}
				npc.GetComponent<PlayerControler>().targetObject = listItem[index];
				npc.GetComponent<PlayerControler>().targetObject.GetComponent<Flock> ().botScripts.Add(npc.GetComponent<PlayerControler> ());
				controller.target =listItem[index].transform;
				npc.GetComponent<PlayerControler>().PerformTransition(Transition.SawItem);

			
		}
		else

		if ( timeToEscape<=0)
		{
			//npc.GetComponent<PlayerControler> ().targetObject.GetComponent<Flock> ().botScripts.Remove(npc.GetComponent<PlayerControler> ());
			npc.GetComponent<PlayerControler>().SetTransition(Transition.NoTarget);
			
		}
		
		
	}
	int checkBestItem(List<GameObject> item, Transform npc)
	{
		int index = 0;
		int min = 1000;
		if (item.Count == 0)
			return -1;
		for (int i =0; i< item.Count; i++) 
		{
			if(item[i].GetComponent<RandomItem>().piority < min)
			{
				index =i;
				min = item[i].GetComponent<RandomItem>().piority;
			}
			
		}
		if (npc.GetComponent<Equipment> ()._weapon != null) {
			if(item[index].GetComponent<RandomItem>().piority >= npc.GetComponent<Equipment> ()._weapon.GetComponent<Weapon>().piority )
			{
				index =-1;
			}
		} 
		return index;
		
		
	}
	public override void Act(Transform player, Transform npc)
	{

		if(!isBack)
		{
		direction = npc.position - player.position ;
		direction.y = 0;
		}
		if(npc.position.x< -map.GetComponent<Renderer>().bounds.size.x/2 +5
		   ||npc.position.x>map.GetComponent<Renderer>().bounds.size.x/2 -5
		   ||npc.transform.position.z< -map.GetComponent<Renderer>().bounds.size.z/2 +5
		   ||npc.transform.position.z>map.GetComponent<Renderer>().bounds.size.z/2 -5)
		{
			if(!isBack)
			{
				direction = new Vector3 (Random.Range (-npc.position.x, npc.position.x+10), 0, Random.Range (- npc.position.z,  npc.position.z+10));
				direction =direction - npc.position;
				direction.y = 0;
				isBack =true;
				timeToChangeDirection = Random.Range(2f,3f);
			}
		}
		if (isBack)
			timeToChangeDirection -= Time.deltaTime;
		if (timeToChangeDirection <= 0)
			isBack = false;
		npc.GetComponent<Animator> ().SetFloat ("Speed", 1);
		npc.rotation = Quaternion.Slerp(npc.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);
		npc.GetComponent<Rigidbody> ().velocity = direction.normalized * 80 * 0.15f;
		timeToEscape -= Time.deltaTime;

		
	}

}
