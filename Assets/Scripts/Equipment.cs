using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public GameObject _prefabWeapon = null;
	public Transform _righthandTransform = null;
	// Use this for initialization
	public GameObject _weapon = null;
	public Vector3 _initRotation = new Vector3(0,0,90);
	void Start () {

	}
	
	public void EquipWeapon (GameObject prefabWeapon) {
		_prefabWeapon = prefabWeapon;
		if(_weapon != null)
			Destroy(_weapon);
		_weapon = Instantiate (prefabWeapon, _righthandTransform.position, _righthandTransform.rotation) as GameObject;
		_weapon.transform.Rotate(_initRotation);
		_weapon.transform.SetParent(_righthandTransform);

		Hashtable hash = new Hashtable();
		hash.Add("Type", _weapon.name);
		NotificationCenter.DefaultCenter.PostNotification(this, "OnWeaponChange",hash);
	}
}
