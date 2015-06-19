using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour {
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "RandomItem" || col.gameObject.name == "RandomItem(Clone)")
		{
			GameObject randomWeapon = col.gameObject.GetComponent<RandomItem>().RandomWeapon();
			GetComponent<Equipment>().EquipWeapon(randomWeapon);
			Destroy(col.gameObject);
		}
	}
}
