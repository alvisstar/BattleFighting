using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;
	public GameObject prefabIceBall = null;
	private GameObject instanceIceBall = null;
	public GameObject prefabExplosionIceBall = null;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<Rigidbody>().velocity = direction*40f;
	}

	public void Init(Vector3 direction)
	{
		this.direction = direction;
		instanceIceBall = Instantiate (prefabIceBall, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		instanceIceBall.transform.SetParent (gameObject.transform);
	}
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Bot" )
		{
			col.gameObject.GetComponent<BotControler>().BeHitted();
			Vector3 pos = col.contacts[0].point;
			
			GameObject explosionIceBall = Instantiate(prefabExplosionIceBall,pos,instanceIceBall.transform.rotation) as GameObject;
			Destroy(instanceIceBall);
			Destroy(gameObject);
		}
	}
}
