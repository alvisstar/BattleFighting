using UnityEngine;
using System.Collections;

public class ThrowingBomb : MonoBehaviour {

	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	Vector3 direction;
	bool beginExplode;
	float timeToExplode;
	
	public GameObject prefabExplode = null;
	
	void Start () {
		beginExplode = false;
		timeToExplode = 0;
		beginExplode = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (beginExplode)
			timeToExplode += Time.deltaTime;
		if (timeToExplode >= 1.7	)
		{
			Instantiate(prefabExplode,gameObject.transform.position,Quaternion.identity);
			Hashtable hash = new Hashtable();
			hash.Add("Position", gameObject.transform.position);
			NotificationCenter.DefaultCenter.PostNotification(this, "OnBombExplode",hash);
			Destroy (gameObject);
		}
	}
	
	public void Init(Vector3 directionz)
	{
		direction = directionz;
		direction += new Vector3 (0, 1f	, 0);
		//Physics.IgnoreCollision(GetComponent<Collider>(), characterTransform.gameObject.GetComponent<Collider>());

		GetComponent<Rigidbody> ().velocity =  characterTransform.GetComponent<Rigidbody> ().velocity;
		GetComponent<Rigidbody> ().AddForce(direction*8,ForceMode.Impulse);

	}
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Terrain" )
		{
			//beginExplode = true;
			Physics.IgnoreCollision(GetComponent<Collider>(), characterTransform.gameObject.GetComponent<Collider>(),false);

		}
	}
}
