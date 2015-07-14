using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	protected PlayerControler _player;
	protected GameManager _gameManager;
	protected float currentTimeSkill1 = 0;
	protected bool isBeingActiveSkill1 = false;
	protected Vector3 startPos;
	public float rangeOfSkill1 = 10;
	public float timeOfSkill1 = 0.5f;
	
	public bool isUsedSkill1;
	public bool isUsedSkill2;
	public float delayTimeSkill1;
	public float delayTimeSkill2;
	// skill 2
	protected float currentTimeSkill2 = 0;
	protected bool isBeingActiveSkill2 = false;
	public float rangeOfSkill2 = 5;
	public float timeOfSkill2 = 7.0f;
	public GameObject prefabSkill2;
	protected GameObject skill2Object;
	protected float currentTimeAddForce = 0.0f;
	public virtual void activeSkill1() {
		_player._animator.SetBool ("IsSkill1",true);
		_player._animator.SetBool ("IsSkill",true);
		_player.setAllowControl (false);
	}

	public virtual void activeSkill2() {

		
	}
	public virtual void deActiveSkill1() {


	}
	
	public virtual void deActiveSkill2() {

		
	}
	public virtual void finishAnimation() {
		
		
	}
	public virtual bool readyToSkill1() {
		return true;
		
	}
	public virtual bool readyToSkill2() {
		return true;
		
	}
	

}
