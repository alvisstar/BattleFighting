using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	public GameObject bombPrefabs;
	public Transform characterTransform;
	Vector3 direction;
	//List<Arrow> _arrows;
	bool attack = false;
	void Start () {
		
		//attack = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(attack)
			gameObject.transform.position += direction*0.1f;
	}

	public void Init(Transform transForm)
	{
		direction = transform.forward;
		/*characterTransform = transform;
		gameObject.transform.position = characterTransform.position;
		gameObject.transform.position += new Vector3 (0, 2,0);
		gameObject.transform.rotation = transform.rotation;
		gameObject.transform.Rotate(0,180,0);*/
	}
	
	public void Attack()
	{
		attack = true;

	}
}
