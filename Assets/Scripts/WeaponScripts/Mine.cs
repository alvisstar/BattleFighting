using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mine : MonoBehaviour {

	// Use this for initialization
	public GameObject minePrefabs;
	public Transform characterTransform;
	public int availableMine = 5;
	public float coolDownTime = 1;
	private float currentTime;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		
	}
	
	public void Attack()
	{
		if (currentTime < coolDownTime) {
			return;
		} else {
			currentTime = 0;
		}
		GameObject landMine = Instantiate(minePrefabs, characterTransform.position, characterTransform.rotation) as GameObject;
		landMine.transform.Rotate(0,180,0);
		LandMine script = landMine.GetComponent<LandMine> ();
		script.Init (characterTransform.forward);

		availableMine--;
		if (availableMine <= 0) {
			Destroy(gameObject);
		}
	}
}
