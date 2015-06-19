using UnityEngine;
using System.Collections;

public class RandomItem : MonoBehaviour {
	public GameObject[] _prefabWeapons;

	public GameObject RandomWeapon(){
		int random = Random.Range (0, _prefabWeapons.Length);
		Debug.Log ("random " + random);
		return _prefabWeapons[random];
	}
}
