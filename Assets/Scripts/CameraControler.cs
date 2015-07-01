using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {

	// Use this for initialization
	private GameObject _targetObject = null;
	
	public GameObject targetObject {
		get {
			return _targetObject;
		}
		set {
			_targetObject = value;
		}
	}
	
	public float DistanceObject = 3.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	private Vector3 _positionTemp; 
	
	// Update is called once per frame
	void Update () {
		_positionTemp = gameObject.transform.position;
		gameObject.transform.position = Vector3.Lerp(_positionTemp,new Vector3(targetObject.transform.position.x, _positionTemp.y ,targetObject.transform.position.z - DistanceObject),Time.deltaTime*10);	
		//_positionTemp.z =  targetObject.transform.position.z - DistanceObject;
		//gameObject.transform.position = _positionTemp;	

		//transform.LookAt (Vector3.zero);
	}
}
