using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	
	public GameObject itemPrefab;
	public float range = 30;
	private float time = 0;

	// Use this for initialization
	void Start () {
		time = 10;
	}
	
	// Update is called once per frame
	void Update () {	
		//float t =;
		Vector3 x = GameObject.Find ("Ground").GetComponent<Collider> ().bounds.size;
		time += Time.deltaTime;
		if (time > 10) {
			time = 0;
			Vector3 posRandom = new Vector3(Random.Range(-GameObject.Find ("Ground").GetComponent<Renderer>().bounds.size.x/2 + 2, GameObject.Find ("Ground").GetComponent<Renderer>().bounds.size.x/2-2), 0, Random.Range(-GameObject.Find ("Ground").GetComponent<Renderer>().bounds.size.z/2 +2, GameObject.Find ("Ground").GetComponent<Renderer>().bounds.size.z/2-2));
			Instantiate(itemPrefab, transform.position + posRandom, transform.rotation);
		}
	}
}
