using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AICharacterManager : MonoBehaviour {
	
	// Use this for initialization
	GameObject character;
	public List<PlayerControler> botScripts ;
	int maxNumberNearPLayer;
	int currentNumberNearPLayer;
	
	public float minVelocity = 500;  
	public float maxVelocity = 800; 
	public int flockSize = 7;  
	public float centerWeight = 4;
	public float velocityWeight = 6;  
	
	
	public float separationWeight = 3;
	//How close each boid should follow to the leader (the more   //weight make the closer follow)  
	public float followWeight = 3;
	//Additional Random Noise  
	public float randomizeWeight = 3; 
	
	public Transform target;
	//Center position of the flock in the group  
	internal Vector3 flockCenter;    
	internal Vector3 flockVelocity;  //Average Velocity
	
	public float rangeAttack;

	void Start () {
		
		maxNumberNearPLayer = 0;
		currentNumberNearPLayer = 0;
		rangeAttack = 3.5f;
		
	}
	public void Init(GameObject character,List<PlayerControler> botScripts)
	{
		this.character = character;
		this.botScripts = botScripts;
		target = character.transform;
		for(int i =1 ;i< botScripts.Count ;i++)
		{
			botScripts[i].targetObject = botScripts[0].gameObject;
			botScripts[i].GetComponent<Flock>().botScripts.Add(botScripts[i]);

		}
		
	}
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	void Update () {
		

		Vector3 center = Vector3.zero;    
		Vector3 velocity = Vector3.zero;
		foreach (PlayerControler bot in botScripts) 
		{   center += bot.transform.localPosition;      
			velocity += bot.gameObject.GetComponent<Rigidbody>().velocity;    
		}
		
		
		flockCenter = center / flockSize;    
		flockVelocity = velocity / flockSize;  
		
		ChangeTarget ();
	}
	public void ChangeTarget()
	{
		for(int i =0 ;i< botScripts.Count ;i++)
		{
			if(botScripts[i].needChangeTarget == true)
			{
				int n = Random.Range(0,botScripts.Count);
				if(n==i)
				{
					if(i ==0)
						n++;
					else
						if(i ==botScripts.Count-1)
							n--;
					else
						n++;
				}


				botScripts[i].targetObject = botScripts[n].gameObject;
				botScripts[n].GetComponent<Flock>().botScripts.Add(botScripts[i]);
				botScripts[i].needChangeTarget = false;
			}

			
		}
	}


}