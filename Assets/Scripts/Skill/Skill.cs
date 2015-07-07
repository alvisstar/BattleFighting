using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	protected PlayerControler _player;

	public virtual void activeSkill1() {
		_player._animator.SetBool ("IsSkill1",true);
		_player._animator.SetBool ("IsSkill",true);
		_player.setAllowControl (false);
	}

	public virtual void activeSkill2() {
		
	}
}
