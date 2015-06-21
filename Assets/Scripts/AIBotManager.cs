using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIBotManager : MonoBehaviour {

	// Use this for initialization
	GameObject character;
	public List<BotControler> botScripts ;
	int maxNumberNearPLayer;
	int currentNumberNearPLayer;
	void Start () {
		maxNumberNearPLayer = 0;
		currentNumberNearPLayer = 0;
	
	}
	
	// Update is called once per frame
	void Update () {


		Aim ();
	
	}

	public void UpdateUnit(GameObject character,List<BotControler> botScripts )
	{
		this.character = character;
		this.botScripts = botScripts;
	}

	void Aim()
	{

		for(int i =0;i<botScripts.Count;i++)
		{
			Vector3 direction =  character.transform.position - botScripts[i].gameObject.transform.position ;

			if(GetNumberBotAttackPlayer()<=maxNumberNearPLayer)
			{
				if(direction.magnitude >3)
				{
					botScripts[i].Move(direction);
				}	
				else
				{

					botScripts[i].AttackTarget();
				
				}

			}
			else
			{
				botScripts[i].MoveAround(direction);

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