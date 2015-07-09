using UnityEngine;
using System.Collections;

public class SkillSecondBehaviour : StateMachineBehaviour
{
	public GameObject particles;            // Prefab of the particle system to play in the state.
	public AvatarIKGoal attackLimb;         // The limb that the particles should follow.
	
	
	private Transform particlesTransform;       // Reference to the instantiated prefab's transform.
	private ParticleSystem particleSystem; 
	
	// Reference to the instantiated prefab's particle system.
	
	public GameObject player;       
	// This will be called when the animator first transitions to this state.
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{


		
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
		

		
		
	}
	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// When leaving the special move state, stop the particles.
		player.GetComponent<PlayerControler> ()._playerSkill.finishAnimation ();

	}
	
	
	// This will be called every frame whilst in the state.
	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// OnStateExit may be called before the last OnStateIK so we need to check the particles haven't been destroyed.
		
	}
}