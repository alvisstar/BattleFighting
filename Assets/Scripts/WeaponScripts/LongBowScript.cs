using UnityEngine;
using System.Collections;

public class LongBowScript : MonoBehaviour {

	// Use this for initialization
	GameObject arrow;
	public GameObject arrowPrefabs;
	public Vector3 characterPosition;
	public Quaternion characterRotation;
	public Transform characterTransform;
	void Start () {
		//arrow = Instantiate(arrowPrefabs, characterPosition, characterRotation) as GameObject;
		//arrow.transform.Rotate(0,180,0);

	}
	
	// Update is called once per frame
	void Update () {

		if (arrow != null) {
			arrow.transform.position += characterTransform.forward*1;
		}
	
	
	}

	public void Attack()
	{
		arrow = Instantiate(arrowPrefabs, characterPosition, characterRotation) as GameObject;
		arrow.transform.position += new Vector3 (0, 2, 0);
		arrow.transform.Rotate(0,180,0);
	}
}
