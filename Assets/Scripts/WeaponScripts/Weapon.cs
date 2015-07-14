using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public int numberOfWeapon = 5;
	public float delayTime = 1;
	private float currentTime;
	public float maxRangeAttack;
	public float minRangeAttack;
	public int piority;
	// Use this for initialization
	void Start () {
		currentTime = delayTime;
	
	}
	
	// Update is called once per frame
	public void Update () {		
		currentTime += Time.deltaTime;

	}

	public bool CheckAllowAttack()
	{
		if (currentTime < delayTime) {
			return false;
		} else {
			return true;
		}
	}

	public void Attack()
	{
		if (currentTime < delayTime) {
			return;
		} else {
			currentTime = 0;
		}

		OnAttack ();

		if(numberOfWeapon ==-1)
		{
			return;
		}
		else{
		
			if (numberOfWeapon <= 0) {
				Destroy(gameObject);
				Hashtable hash = new Hashtable();
				hash.Add("Type", "None");
				NotificationCenter.DefaultCenter.PostNotification(this, "OnWeaponChange",hash);
			}
		}
	}

	virtual public void OnAttack() {

	}
}
