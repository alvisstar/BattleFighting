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
			for (int i =0; i <3; i++) {
				Vector3 sizeOfGround = GameObject.Find ("Ground").GetComponent<Renderer> ().bounds.size;
				Vector3 posRandom = new Vector3 (Random.Range (-sizeOfGround.x / 2 + 2, sizeOfGround.x / 2 - 2), 0, Random.Range (-sizeOfGround.z / 2 + 2, sizeOfGround.z / 2 - 2));			
				int random = Random.Range (1, 5);
				GameObject item = Instantiate (itemPrefabs, transform.position + posRandom, transform.rotation) as GameObject;
				item.GetComponent<RandomItem> ().type = random;
				switch (random) {
				case 1:
					item.GetComponent<RandomItem> ().piority = 1000;
					break;
				case 2:
					item.GetComponent<RandomItem> ().piority = 800;
					break;
				case 3:
					item.GetComponent<RandomItem> ().piority = 600;
					break;
				case 4:
					item.GetComponent<RandomItem> ().piority = 400;
					break;
				case 5:
					item.GetComponent<RandomItem> ().piority = 200;
					break;
				}
			}
		
		}
	}
}
