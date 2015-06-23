using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public int numberOfWeapon = 5;
	public float delayTime = 1;
	private float currentTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {		
		currentTime += Time.deltaTime;
	}

	
	public void Attack()
	{
		if (currentTime < delayTime) {
			return;
		} else {
			currentTime = 0;
		}

		OnAttack ();

		numberOfWeapon--;
		if (numberOfWeapon <= 0) {
			Destroy(gameObject);
		}
	}

	virtual public void OnAttack() {

	}
}
