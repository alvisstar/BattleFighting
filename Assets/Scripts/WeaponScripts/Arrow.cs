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

			//Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = col.contacts[0].point;
			
			GameObject explosionIceBall = Instantiate(prefabExplosionIceBall,pos,instanceIceBall.transform.rotation) as GameObject;
			Destroy(instanceIceBall);
			Destroy(gameObject);
		}
	}
	/*void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Bot" )
		{
			col.gameObject.GetComponent<BotControler>().BeHitted();
			ContactPoint contact = col.GetComponent<ContactPoint>();
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;

			GameObject explosionIceBall = Instantiate(prefabExplosionIceBall,instanceIceBall.transform.position,instanceIceBall.transform.rotation) as GameObject;
			Destroy(instanceIceBall);
			Destroy(gameObject);
		}
	}}*/
}
