using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	public float speed = 50;
	private Vector3 directionMove = new Vector3(0,0,0);

	// Use this for initialization
	private bool _isMain = false;
	
	Animator _animator;
	int attackHash = Animator.StringToHash("Attack");
	int isAttackedHash = Animator.StringToHash("IsAttacked");
	bool _isKeyMovePressing;
	bool _isTouchingDPad;

	private Vector3 m_Move;
	float m_TurnAmount;
	float m_ForwardAmount;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	public TouchController	ctrlPrefab;
	private TouchController	ctrl;
	public GUISkin	guiSkin;

	void Start () {
		speed = 0.15f;
		_animator = GetComponent<Animator>();
		ctrl = Instantiate (ctrlPrefab);
	}
	public void Init(Vector3 position,bool isMain)
	{
		gameObject.transform.position = position;
		_isMain = isMain;
	}
	// Update is called once per frame
	void Update () {
		_isKeyMovePressing = KeyboardControl ();
		HandleAnimation ();
		if (this.ctrl) {	
			// Get stick and zone references by IDs...			
			TouchStick walkStick = this.ctrl.GetStick (0);
			
			if (walkStick.Pressed ()) {
				RotateByDirection (walkStick.GetVec3d (true, 0));
				gameObject.transform.position += walkStick.GetVec3d (true, 0)  * speed ;
				//	GetComponent<Rigidbody> ().velocity = walkStick.GetVec3d (true, 0) * speed * 100;
				
				_isTouchingDPad = true;
			}			
			// Stop when stick is released...			
			else {
				_isTouchingDPad = false;		
			}
			// Shoot when right stick is pressed...
			
		} else {			
			// processing for keyboard
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;
			RotateByDirection(m_Move);
		}
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
		// processing for D-Pad

	}

	void OnGUI()
	{
		// Manually draw the controller...		
		if (this.ctrl != null)
			this.ctrl.DrawControllerGUI();
	}
	
	public void RotateByDirection(Vector3 direction)
	{		
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (direction.magnitude > 1f)
			direction.Normalize ();
		direction = transform.InverseTransformDirection (direction);
		direction = Vector3.ProjectOnPlane (direction, Vector3.up);
		m_TurnAmount = Mathf.Atan2 (direction.x, direction.z);
		m_ForwardAmount = direction.z;
		
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
		//gameObject.GetComponent<Rigidbody> ().velocity = directionMove * speed * 100;
		gameObject.transform.position += directionMove * speed ;
		directionMove = new Vector3 (0, 0, 0);

		return isKeyTouching;
	}

	void HandleAnimation(){
		float move = _isKeyMovePressing || _isTouchingDPad ? 1 : 0;
		_animator.SetFloat("Speed", move);
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			_animator.SetTrigger (attackHash);
			Attack();
		}

	}
	void Attack()
	{
		if (gameObject.GetComponent<Equipment> ()._weapon == null) {
			return;
		}
		if (gameObject.GetComponent<Equipment> ()._weapon.name.CompareTo ("Longbow03(Clone)") == 0) {
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<LongBowScript> ().characterTransform = gameObject.transform;
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<LongBowScript> ().Attack ();

		}
		else if (gameObject.GetComponent<Equipment> ()._weapon.name.CompareTo ("Gun(Clone)") == 0) {
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Gun> ().characterTransform = gameObject.transform;
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Gun> ().Attack ();
			
		}
		else if (gameObject.GetComponent<Equipment> ()._weapon.name.CompareTo ("Bomb(Clone)") == 0) {
			Equipment script  =gameObject.GetComponent<Equipment> ();
			GameObject bomb = Instantiate (script._prefabWeapon, script._righthandTransform.position, script._righthandTransform.rotation) as GameObject;
			bomb.AddComponent<Rigidbody>();
			bomb.GetComponent<Bomb>().characterTransform = gameObject.transform;
			bomb.GetComponent<Bomb>().Init (gameObject.transform);
			bomb.GetComponent<Bomb>().Attack ();
			//gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Bomb> ().characterTransform = gameObject.transform;
			//gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Bomb> ().Attack ();
			Destroy( gameObject.GetComponent<Equipment> ()._weapon);
			
		}
		else if (gameObject.GetComponent<Equipment> ()._weapon.name.CompareTo ("Mine(Clone)") == 0) {
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Mine> ().characterTransform = gameObject.transform;
			gameObject.GetComponent<Equipment> ()._weapon.GetComponent<Mine> ().Attack ();
			
		}
		
	}
}
