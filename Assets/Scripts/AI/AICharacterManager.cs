using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AICharacterManager : MonoBehaviour {
	
	// Use this for initialization
	GameObject character;
	public List<PlayerControler> botScripts ;

	bool a;

	public Transform target;
	//Center position of the flock in the group  

	


	void Start () {
		
		a = false;
		
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
		
		ChangeTarget ();
	}

	public void ChangeTarget()
	{

			for (int i =0; i< botScripts.Count; i++) {
			if (botScripts [i].needChangeTarget == true ) {
				if(botScripts.Count>1)
				{
					int n = Random.Range (0, botScripts.Count);
					if (n == i) {
						if (i == 0)
							n++;
						else
						if (i == botScripts.Count - 1)
							n--;
						else
							n++;
					}


					botScripts [i].targetObject = botScripts [n].gameObject;
					botScripts [n].GetComponent<Flock> ().botScripts.Add (botScripts [i]);
					botScripts [i].needChangeTarget = false;
					
				}
			}
			else
			{
				botScripts [i].targetObject = character;
				character.GetComponent<Flock> ().botScripts.Add (botScripts [i]);
				botScripts [i].needChangeTarget = false;
			}


			
		}
	}
	/*public void ChangeTarget()
	{
		
		for (int i =1; i< botScripts.Count; i++) {
			if (botScripts [i].needChangeTarget == true) {
				
				botScripts [i].targetObject = botScripts [0].gameObject;
				botScripts [0].GetComponent<Flock> ().botScripts.Add (botScripts [i]);
				botScripts [i].needChangeTarget = false;
				
			}
			
			
			
		}
	}*/


}