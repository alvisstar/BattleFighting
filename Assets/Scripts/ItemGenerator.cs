using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	
	public GameObject itemPrefabs;
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
			Vector3 sizeOfGround = GameObject.Find ("Ground").GetComponent<Renderer>().bounds.size;
			Vector3 posRandom = new Vector3(Random.Range(-sizeOfGround.x/2 + 2, sizeOfGround.x/2-2), 0, Random.Range(-sizeOfGround.z/2 +2, sizeOfGround.z/2-2));			
			int random = Random.Range (1, 1);
			GameObject item = Instantiate(itemPrefabs, transform.position + posRandom, transform.rotation) as GameObject;
			item.GetComponent<RandomItem>().type = random;
		}
	}
}
