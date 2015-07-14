using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;
	void Update () {
		
		gameObject.transform.position += direction;
	}
	
	public void Init(Vector3 direction)
	{
		this.direction = direction;
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player" )
		{
			
			
			//other.gameObject.GetComponent<PlayerControler>().BeHitted();
			//Destroy(gameObject);
		}
	}

}
