using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour {
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "RandomItem" || col.gameObject.name == "RandomItem(Clone)") {
			GameObject randomItemPrefab = col.gameObject.GetComponent<RandomItem> ().RandomItemz ();
			if (randomItemPrefab.name == "SpeedUp") {
				GameObject speedUp = Instantiate (randomItemPrefab);
				speedUp.GetComponent<SpeedUp>().Effect(GetComponent<PlayerControler>());
			} else if (randomItemPrefab.name == "Poision") {
				GameObject poision = Instantiate (randomItemPrefab);
				poision.GetComponent<Poision>().Effect(GetComponent<PlayerControler>());
			} else {
				GetComponent<Equipment> ().EquipWeapon (randomItemPrefab);
			}
			Destroy (col.gameObject);
		}
	}
}
