using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public GameObject _prefabWeapon = null;
	public Transform _righthandTransform = null;
	// Use this for initialization
	private GameObject _weapon = null;

	void Start () {

	}
	
	public void EquipWeapon (GameObject prefabWeapon) {
		if(_weapon != null)
			Destroy(_weapon);
		_weapon = Instantiate (prefabWeapon, _righthandTransform.position, _righthandTransform.rotation) as GameObject;
		_weapon.transform.Rotate(0,180,0);
		_weapon.transform.SetParent(_righthandTransform);
	}
}
