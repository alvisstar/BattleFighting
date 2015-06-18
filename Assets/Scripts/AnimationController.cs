using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	public enum AnimationState{
		Idle,
		Attack,
		BeHit,
		Die,
		Run
	}

	public AnimationClip animIdle; 
	public AnimationClip animAttack; 
	public AnimationClip animBeHit; 
	public AnimationClip animDie; 
	public AnimationClip animRun;

	private AnimationState _currentState;
	private Animation _animation; 
	private AnimationClip _currentAnimation = null;

	// Use this for initialization
	void Start () {								
		InitAnimations(); 	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		// local references for component 
		_animation = GetComponent<Animation>();
	}

	private void InitAnimations(){		
		_animation.Stop(); 
		
		_animation[animIdle.name].wrapMode = WrapMode.Loop; 
		_animation[animRun.name].wrapMode = WrapMode.Loop; 
		_animation[animAttack.name].wrapMode = WrapMode.Once;
		_animation[animBeHit.name].wrapMode = WrapMode.Once; 
		_animation[animDie.name].wrapMode = WrapMode.Once;
	}

	public AnimationState CurrentState{
		get{
			return _currentState; 
		}
		set{
			// cancel any invokes we may have scheduled 
//			CancelInvoke("OnAnimationFinished"); 

			if(_currentState == value)
				return;

			_currentState = value; 
			
			// take action based on state 
			switch( _currentState ){
			case AnimationState.Idle:
				SetCurrentAnimation( animIdle ); 				
				break;			
			case AnimationState.Attack:
				SetCurrentAnimation( animAttack );				
				break;
			case AnimationState.BeHit:
				SetCurrentAnimation( animBeHit ); 
				break;
			case AnimationState.Die:		
				SetCurrentAnimation( animDie ); 
				break;
			case AnimationState.Run:
				SetCurrentAnimation( animRun ); 
				break;
			}
		}		
	}
	
	public void SetCurrentAnimation(AnimationClip animationClip){
		_currentAnimation = animationClip; 
		_animation[_currentAnimation.name].time = 0.0f; 
		_animation.CrossFade( _currentAnimation.name, 0.1f ); 
		
		// if the animation is not looping then we want to schedule a invoke to fire when the animation is finished 
//		if( _currentAnimation.wrapMode != WrapMode.Loop ){
//			Invoke ("OnAnimationFinished", _animation[_currentAnimation.name].length /  _animation[_currentAnimation.name].speed );
//		}
	}	
}
