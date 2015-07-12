using UnityEngine;
using System.Collections;

public class ThrowingShit : MonoBehaviour {
	
	// Use this for initialization

	public GameObject explosePrefabs;
	public GameObject warmPrefabs;
	public GameObject warmObject;
	public GameObject target;
	public Transform characterTransform;
	Vector3 direction;
	bool beginEffect;
	float timeEffect;
	
	//public GameObject prefabExplode = null;
	
	void Start () {
		beginEffect = false;
		timeEffect = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (beginEffect) {
			timeEffect += Time.deltaTime;
			warmObject.transform.position = new Vector3(target.transform.position.x,warmObject.transform.position.y,target.transform.position.z);
			Debug.Log(target.transform.position.x +" and " + target.transform.position.z);
		}
	
		if (timeEffect >= 5	)
		{
			//Instantiate(prefabExplode,gameObject.transform.position,Quaternion.identity);

			//Destroy (warmObject);
		}
	}
	
	public void Init(Vector3 directionz)
	{
		direction = directionz;
		//direction += new Vector3 (0, 1f	, 0);
		//Physics.IgnoreCollision(GetComponent<Collider>(), characterTransform.gameObject.GetComponent<Collider>());
		
		GetComponent<Rigidbody> ().velocity =  characterTransform.GetComponent<Rigidbody> ().velocity;
		GetComponent<Rigidbody> ().AddForce(direction*30,ForceMode.Impulse);
		
	}
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Ground") {
			//Destroy (gameObject);	
		} else if (col.gameObject.tag == "Player") {
			//Destroy(gameObject);
			beginEffect =true;
			Instantiate(explosePrefabs,col.contacts[0].point,Quaternion.identity);
			warmObject = Instantiate(warmPrefabs,col.contacts[0].point,Quaternion.identity) as GameObject;
			target = col.gameObject;
		}
	}
}
