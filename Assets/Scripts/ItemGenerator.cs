using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	
	public GameObject itemPrefab;
	public float range = 30;
	private float time = 0;

	// Use this for initialization
	void Start () {
		time = 18;
	}
	
	// Update is called once per frame
	void Update () {		
		time += Time.deltaTime;
		if (time > 20) {
			time = 0;
			Vector3 posRandom = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
			Instantiate(itemPrefab, transform.position + posRandom, transform.rotation);
		}
	}
}
