using UnityEngine;
using System.Collections;

public class Poision : MonoBehaviour {
	
	private PlayerControler _character;
	public float timeEffect = 2;
	public float dealHp = 0.5f;
	public float timePerOneDealHp = 0.5f;
	private float currentTime;
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= timePerOneDealHp) {
			currentTime -= timePerOneDealHp;
			timeEffect -= timePerOneDealHp;
			_character.hp -= dealHp;
		}

		if (timeEffect <= 0) {
			Destroy(gameObject);
		}
		Debug.Log (_character.hp);
	}
	
	public void Effect(PlayerControler character) {
		_character = character;
	}
}
