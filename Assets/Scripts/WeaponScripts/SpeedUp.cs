using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {

	private PlayerControler _character;
	public float timeEffect = 2;
	public float speedUp = 1.5f;
	private float currentTime;
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= timeEffect) {
			_character.speed /= speedUp;
			Destroy(gameObject);
		}
	}

	public void Effect(PlayerControler character) {
		_character = character;
		_character.speed *= speedUp;
	}
}
