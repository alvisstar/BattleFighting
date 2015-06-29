using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public GameObject _prefabWeapon = null;
	public Transform _righthandTransform = null;
	// Use this for initialization
	public GameObject _weapon = null;

	bool hasWeapon;
	int  i =0;

	public Vector3 _initRotation = new Vector3(0,0,90);

	void Start () {
		hasWeapon = false;

	}
	
	public void EquipWeapon (GameObject prefabWeapon) {
		_prefabWeapon = prefabWeapon;
		if(_weapon != null)
			Destroy(_weapon);

		if(_prefabWeapon.name =="None")
		{
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipSword",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipBomb",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipNone",true);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipGun",false);

		}
		else if(_prefabWeapon.name =="Gun")
		{
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipSword",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipBomb",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipNone",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipGun",true);

		}
		else if(_prefabWeapon.name =="Bomb")
		{
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipSword",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipBomb",true);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipNone",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipGun",false);

		}
		else if(_prefabWeapon.name =="Sword")
		{
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipSword",true);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipBomb",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipNone",false);
			gameObject.GetComponent<PlayerControler>()._animator.SetBool("IsEquipGun",false);

		}
		_weapon = Instantiate (_prefabWeapon, _righthandTransform.position, _righthandTransform.rotation) as GameObject;
		_weapon.transform.Rotate (new Vector3 (0, 0, 90));
		_weapon.transform.SetParent(_righthandTransform);
		hasWeapon = true;
	}
	void Update()
	{
		if(hasWeapon)
		{
			i++;
			if(i>5)
			{
			//_weapon = Instantiate (_prefabWeapon, _righthandTransform.position, _righthandTransform.rotation) as GameObject;
			//_weapon.transform.Rotate (new Vector3 (0, 0, 90));
			//_weapon.transform.SetParent(_righthandTransform);

			
			
			//Hashtable hash = new Hashtable();
			//hash.Add("Type", _weapon.name);
			//NotificationCenter.DefaultCenter.PostNotification(this, "OnWeaponChange",hash);
			//hasWeapon = false;
				//i=0;
			}
		}
	

	}
}
