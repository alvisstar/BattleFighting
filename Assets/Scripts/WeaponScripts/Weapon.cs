using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public int numberOfWeapon = 5;
	public float delayTime = 1;
	private float currentTime;

	// Use this for initialization
	void Start () {
		currentTime = delayTime;
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
			Hashtable hash = new Hashtable();
			hash.Add("Type", "None");
			NotificationCenter.DefaultCenter.PostNotification(this, "OnWeaponChange",hash);
		}
	}

	virtual public void OnAttack() {

	}
}
