using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	public float speed = 0.1f;
	private Vector3 directionMove = new Vector3(0,0,0);

	// Use this for initialization
	private bool _isMain = false;
	
	Animator _animator;
	int attackHash = Animator.StringToHash("Attack");

	private Vector3 m_Move;
	float m_TurnAmount;
	float m_ForwardAmount;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;

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
		bool isKeyTouching = KeyboardControl ();
		HandleAnimation (isKeyTouching);
	}
	public bool CheckIsAnimation(string name)
	{
		//int atakState = Animator.StringToHash(name); 
		if (_animator.GetCurrentAnimatorStateInfo (0).IsName(name))
			return true;
		return false;
	
	}
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		// we use world-relative directions in the case of no main camera
		m_Move = v*Vector3.forward + h*Vector3.right;
		Move(m_Move);
	}

	public void Move(Vector3 move)
	{		
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f)
			move.Normalize ();
		move = transform.InverseTransformDirection (move);
		move = Vector3.ProjectOnPlane (move, Vector3.up);
		m_TurnAmount = Mathf.Atan2 (move.x, move.z);
		m_ForwardAmount = move.z;
		
		ApplyExtraTurnRotation ();
	}
	
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	bool KeyboardControl()
	{
		bool isKeyTouching = false;

		//gem because use generic animation
		if (Input.GetKey (KeyCode.W)) {
			directionMove += new Vector3 (0, 0, 1);
			isKeyTouching = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			directionMove += new Vector3 (-1, 0, 0);
			isKeyTouching = true;
		}
		if (Input.GetKey (KeyCode.S)){
			directionMove += new Vector3(0,0,-1);
			isKeyTouching = true;
		}
		if (Input.GetKey (KeyCode.D)){
			directionMove += new Vector3(1,0,0);
			isKeyTouching = true;
		}
		//approach 2 move with velocity
		directionMove.Normalize ();
		gameObject.GetComponent<Rigidbody> ().velocity = directionMove * speed * 100;
		directionMove = new Vector3 (0, 0, 0);

		return isKeyTouching;
	}

	void HandleAnimation(bool isKeyTouching){
		float move = isKeyTouching ? 1 : 0;
		_animator.SetFloat("Speed", move);
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger (attackHash);
		}

	}
}
