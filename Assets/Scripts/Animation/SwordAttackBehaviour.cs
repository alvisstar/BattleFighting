using UnityEngine;
using System.Collections;

public class SwordAttackBehaviour : StateMachineBehaviour
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
		if (player != null)
		if (player.GetComponent<PlayerControler> ().GetIsAttack ()) {
			if (player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> () != null) {
				player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> ().trail.Activate ();
				player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> ().characterTransform = player.transform;
				player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> ().Attack ();	
				player.GetComponent<PlayerControler> ().FinishAttack ();
			}
		
		}
		
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		if(player !=null)
		if ( stateInfo.normalizedTime < 0.5) {
			
			player.GetComponent<PlayerControler> ().onTrigger = true;
		}
		else
		{
			player.GetComponent<PlayerControler> ().onTrigger = false;
		}
			
			
		
	}
	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// When leaving the special move state, stop the particles.
		if(player!=null)
		{
			if(player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> ()!=null)
			{
				player.GetComponent<Equipment> ()._weapon.GetComponent<Sword> ().trail.Deactivate();
				player.GetComponent<PlayerControler> ().onTrigger = false;
			}
		}
	}
	
	
	// This will be called every frame whilst in the state.
	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// OnStateExit may be called before the last OnStateIK so we need to check the particles haven't been destroyed.
		
	}
}