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

	void FixedUpdate()
	{
		// read inputs
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool crouch = Input.GetKey(KeyCode.C);
		
		// calculate move direction to pass to character
//		if (m_Cam != null)
//		{
//			// calculate camera relative direction to move:
//			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
//			m_Move = v*m_CamForward + h*m_Cam.right;
//		}
//		else
//		{
			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;
//		}
//		#if !MOBILE_INPUT
//		// walk speed multiplier
//		if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
//		#endif
		
		// pass all parameters to the character control script
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

		//approach 1 move with transform//////////////////////////
		/*directionMove.Normalize ();
		
		gameObject.transform.position += directionMove * speed;
		*/
		/////////////////////////////////////////////////////////


		//approach 2 move with velocity/////////////////
		directionMove.Normalize ();

		gameObject.GetComponent<Rigidbody> ().velocity = directionMove * speed * 100;
		////////////////////////////////////////////////
		
//		float turnSpeed = 10.0f;
//		float targetAngle = Mathf.Atan2(directionMove.z, directionMove.x) * Mathf.Rad2Deg;
//		transform.rotation = 
//			Quaternion.Slerp( transform.rotation, 
//			                 Quaternion.Euler( targetAngle, 0, targetAngle ), 
//			                 turnSpeed * Time.deltaTime );

		directionMove = new Vector3 (0, 0, 0);

		return isKeyTouching;
	}

	void HandleAnimation(bool isKeyTouching){		
//		float moveVerticle = Mathf.Abs(Input.GetAxis ("Vertical"));
//		float moveHorizontal = Mathf.Abs(Input.GetAxis ("Horizontal"));
//		float move = moveVerticle > moveHorizontal ? moveVerticle : moveHorizontal;
//		Debug.Log (move);
		float move = isKeyTouching ? 1 : 0;
		_animator.SetFloat("Speed", move);
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger (attackHash);
		}
	}
}
