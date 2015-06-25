using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIBotManager : MonoBehaviour {

	// Use this for initialization
	GameObject character;
	public List<BotControler> botScripts ;
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



	void Start () {

		maxNumberNearPLayer = 0;
		currentNumberNearPLayer = 0;

	}
	public void Init(GameObject character,List<BotControler> botScripts)
	{
		this.character = character;
		this.botScripts = botScripts;
		target = character.transform;

	}
	// Update is called once per frame
	void FixedUpdate () {

	}
	void Update () {


		Vector3 center = Vector3.zero;    
		Vector3 velocity = Vector3.zero;
		foreach (BotControler bot in botScripts) 
		{   center += bot.transform.localPosition;      
			velocity += bot.gameObject.GetComponent<Rigidbody>().velocity;    
		}
		
		
		flockCenter = center / flockSize;    
		flockVelocity = velocity / flockSize;  
	 
	
	}

	public void UpdateUnit(GameObject character,List<BotControler> botScripts )
	{
		/*this.character = character;
		this.botScripts = botScripts;
		target = character.transform;
		foreach (BotControler bot in botScripts) 
		{
			bot.controller = this;
			bot.transform.parent = transform;
		}*/
	}

	void Aim()
	{

		for(int i =0;i<botScripts.Count;i++)
		{
			Vector3 direction =  character.transform.position - botScripts[i].gameObject.transform.position ;


				if(direction.magnitude >3)
				{
					//botScripts[i].Move(direction);
				}	
				else
				{

					//botScripts[i].AttackTarget();
				
				}


			
		}
	}

	int GetNumberBotNearPlayer()
	{
		int number = 0;
		for(int i = 0;i < botScripts.Count;i++)
		{
			Vector3 direction =  character.transform.position - botScripts[i].gameObject.transform.position ;
			if(direction.magnitude <= 6)
			{
				number++;
			}

		}
		return number;
	}

	int GetNumberBotAttackPlayer()
	{
		int number = 0;
		for(int i = 0;i < botScripts.Count;i++)
		{

			if(botScripts[i].CheckIsAnimation("TripleKick") )
			{
				number++;
			}
			
		}
		return number;
	}


}