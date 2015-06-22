using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position += direction;
	}

	public void Init(Vector3 direction)
	{
		this.direction = direction;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Bot" )
		{
		

			other.gameObject.GetComponent<BotControler>().BeHitted();
			Destroy(gameObject);
		}
	}
}
