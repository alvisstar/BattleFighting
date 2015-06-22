using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Gun : MonoBehaviour {

	// Use this for initialization
	public GameObject bulletPrefabs;
	public Transform characterTransform;
	List<Bullet> _bullets;
	void Start () {
		
		_bullets = new List<Bullet> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void Attack()
	{
		GameObject bullet = Instantiate(bulletPrefabs, characterTransform.position, characterTransform.rotation) as GameObject;
		bullet.transform.position += new Vector3 (0, 2, 0);
		bullet.transform.Rotate(0,180,0);
		Bullet script = bullet.GetComponent<Bullet> ();
		script.Init (characterTransform.forward);
		_bullets.Add (script);
	}
}
