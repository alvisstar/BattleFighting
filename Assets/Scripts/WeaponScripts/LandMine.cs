using UnityEngine;
using System.Collections;

public class LandMine : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;
	float currentTime;
	public float activeTime = 1;

	void Start () {

	}

	void Update () {
		if (currentTime < activeTime)
			currentTime += Time.deltaTime;

	}
	
	public void Init(Vector3 direction)
	{
		this.direction = direction;
	}
	
	void OnCollisionEnter (Collision other) {
		if (currentTime < activeTime) {
			return;
		}

		if (other.gameObject.tag == "Bot") {
			other.gameObject.GetComponent<BotControler> ().BeHitted ();
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Player") {
//			other.gameObject.GetComponent<PlayerControler> ().BeHitted ();
			Destroy (gameObject);
		}
	}
}
