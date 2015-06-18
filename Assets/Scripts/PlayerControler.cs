using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	public float speed = 0.1f;
	private Vector3 directionMove = new Vector3(0,0,0);

	// Use this for initialization
	private bool _isMain = false;
	
	Animator _animator;
	int attackHash = Animator.StringToHash("Attack");

	void Start () {
		_animator = GetComponent<Animator>();
	}
	public void Init(Vector3 position,bool isMain)
	{
		gameObject.transform.position = position;
		_isMain = isMain;
	}
	// Update is called once per frame
	void Update () {
		KeyboardControl ();
		HandleAnimation ();
	}

	void FixedUpdate()
	{

	}
	void KeyboardControl()
	{
		//gem because use generic animation
		if (Input.GetKey (KeyCode.W)) {
			directionMove += new Vector3 (0, 0, 1);
		}
		if (Input.GetKey (KeyCode.A)) {
			directionMove += new Vector3 (-1, 0, 0);
		}
		if (Input.GetKey (KeyCode.S)){
			directionMove += new Vector3(0,0,-1);
		}
		if (Input.GetKey (KeyCode.D)){
			directionMove += new Vector3(1,0,0);
		}

		//approach 1 move with transform//////////////////////////
		/*directionMove.Normalize ();
		
		gameObject.transform.position += directionMove * speed;
		*/
		/////////////////////////////////////////////////////////


		//approach 2 move with velocity/////////////////
		directionMove.Normalize ();

		gameObject.GetComponent<Rigidbody> ().velocity = directionMove * speed * 100;
		////////////////////////////////////////////////
		/// 
		directionMove = new Vector3 (0, 0, 0);
	}

	void HandleAnimation(){		
		float moveVerticle = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float move = moveVerticle != 0 ? moveVerticle : moveHorizontal;
		_animator.SetFloat("Speed", move);
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger (attackHash);
		}
	}
}
