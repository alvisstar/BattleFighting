using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public GameObject _prefabWeapon = null;
	public Transform _transform = null;
	// Use this for initialization
	private GameObject _weapon = null;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			if(_weapon != null)
				Destroy(_weapon);
			_weapon = Instantiate (_prefabWeapon, _transform.position, _transform.rotation) as GameObject;
			_weapon.transform.Rotate(0,180,0);
			_weapon.transform.SetParent(_transform);
		}
	}
}
