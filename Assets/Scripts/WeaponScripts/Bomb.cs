using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	Vector3 direction;
	//List<Arrow> _arrows;
	bool attack = false;
	bool beginExplode;
	float timeToExplode;
	void Start () {
		
		//attack = false;
		beginExplode = false;
		timeToExplode = 0;
	}

	// Update is called once per frame
	void Update () {
		if (beginExplode)
			timeToExplode += Time.deltaTime;
		if (timeToExplode >= 2)
		{
			Hashtable hash = new Hashtable();
			hash.Add("Position", gameObject.transform.position);
			NotificationCenter.DefaultCenter.PostNotification(this, "OnBombExplode",hash);
			Destroy (gameObject);

		}
	
		//gameObject.transform.position += direction;
	}

	public void Init(Transform transForm)
	{
		direction = characterTransform.forward;
		direction += new Vector3 (0, 1f	, 0);

		/*characterTransform = transform;
		gameObject.transform.position = characterTransform.position;
		gameObject.transform.position += new Vector3 (0, 2,0);
		gameObject.transform.rotation = transform.rotation;
		gameObject.transform.Rotate(0,180,0);*/
	}
	
	public void Attack()
	{
		attack = true;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		gameObject.GetComponent<Rigidbody> ().AddForce(direction*10,ForceMode.Impulse);

	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Terrain" )
		{
			beginExplode = true;
		}
	}



}
