using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LandMine : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;
	float currentTime;
	bool isExplosed;
	public float activeTime = 1;
	public float explosionTime = 10;

	void Start () {
       
	}
    
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime > activeTime + explosionTime && !isExplosed) {
			isExplosed = true;
			Explose();
		}
	}

	void Explose () {
		Hashtable hash = new Hashtable();
		hash["Position"] = gameObject.transform.position;
		NotificationCenter.DefaultCenter.PostNotification(this, "OnMineExplode",hash);
		Destroy (gameObject);
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
			Hashtable hash = new Hashtable();
			hash["Position"] = gameObject.transform.position;
			NotificationCenter.DefaultCenter.PostNotification(this, "OnMineExplode",hash);
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Player") {
			Hashtable hash = new Hashtable();
			hash["Position"] = gameObject.transform.position;
			NotificationCenter.DefaultCenter.PostNotification(this, "OnMineExplode",hash);
			Destroy (gameObject);
		}
	}
}
