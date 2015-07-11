using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Flock : MonoBehaviour {
	
	// Use this for initialization

	public List<PlayerControler> botScripts ;

	
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
		

		
	}

	// Update is called once per frame
	void FixedUpdate () {
		
	}
	void Update () {
		
		flockSize = botScripts.Count;
		if(flockSize ==0)
			flockSize=1;
		Vector3 center = Vector3.zero;    
		Vector3 velocity = Vector3.zero;
		foreach (PlayerControler bot in botScripts) 
		{   center += bot.transform.localPosition;      
			velocity += bot.gameObject.GetComponent<Rigidbody>().velocity;    
		}
		
		
		flockCenter = center / flockSize;    
		flockVelocity = velocity / flockSize;  
		
		
	}

	
	
}