using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
public class PlayerControler : MonoBehaviour {

	private float currentSpeed = 0.15f;
	public float normalSpeed = 0.15f;
	public float maxSpeed = 0.3f;
	public float minSpeed = 0.075f;
	private float _force;
	public TextAsset imageAsset;
	public float CurrentSpeed {
		get {
			return currentSpeed;
		}
		set {
			if (value < minSpeed)
				currentSpeed = minSpeed;
			else if (value > maxSpeed)
				currentSpeed = maxSpeed;
			else
				currentSpeed = value;
		}
	}

	public float hp = 10;
	private Vector3 directionMove = new Vector3(0,0,0);

	// Use this for initialization
	private bool _isMain = false;
	
	public Animator _animator;
	int attackHash = Animator.StringToHash("Attack");
	int isAttackedHash = Animator.StringToHash("IsAttacked");
	int isDeadHash = Animator.StringToHash("IsDead");

	bool _isRunningAnimation;
	bool _isAttack;
	public bool isDie;
	bool _isKeyMovePressing;
	bool _isTouchingDPad;
	bool _firstPress;
	private Vector3 m_Move;
	float m_TurnAmount;
	float m_ForwardAmount;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	public TouchController	ctrlPrefab;
	private TouchController	ctrl;
	public GUISkin	guiSkin;
	TouchStick walkStick ;
	TouchZone 	zoneFight;
	TouchZone 	zoneSkill1	;
	TouchZone 	zoneSkill2	;
	void Start () {
		_animator = GetComponent<Animator>();
		ctrl = Instantiate (ctrlPrefab);
		walkStick = this.ctrl.GetStick (0);
		zoneFight		= this.ctrl.GetZone(0);
		TextAsset tmp = Resources.Load("Button-Punch", typeof(TextAsset)) as TextAsset;
		zoneFight.GetDisplayTex().LoadImage(tmp.bytes);
		zoneSkill1	= this.ctrl.GetZone(1);
		zoneSkill2	= this.ctrl.GetZone(2);
		_isAttack = false;
		_isRunningAnimation = false;
		isDie = false;
		hp = 50;
		_force = 0;
		_firstPress = false;
		_animator.SetBool ("IsEquipNone", true);
		NotificationCenter.DefaultCenter.AddObserver(this, "OnWeaponChange");
	}
	void OnWeaponChange (NotificationCenter.Notification arg)
	{
		Hashtable hash = arg.data;
		string name = (string)hash ["Type"];
		if (name == "Longbow03(Clone)") {
			TextAsset tmp = Resources.Load ("Button-A", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
		} 
		else if (name == "None") {
			TextAsset tmp = Resources.Load ("Button-B", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
			_animator.SetBool("IsEquipSword",false);
			_animator.SetBool("IsEquipBomb",false);
			_animator.SetBool("IsEquipNone",true);
			_animator.SetBool("IsEquipGun",false);

		
			
		}  
		else if (name == "Gun(Clone)") {
			TextAsset tmp = Resources.Load ("Button-B", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
			_animator.SetBool("IsEquipSword",false);
			_animator.SetBool("IsEquipBomb",false);
			_animator.SetBool("IsEquipNone",false);
			_animator.SetBool("IsEquipGun",true);
			_animator.SetBool("IsEquipMine",false);
		}  
		else if (name.CompareTo("Bomb(Clone)")==0) {
			TextAsset tmp = Resources.Load ("Button-C", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
			_animator.SetBool("IsEquipSword",false);
			_animator.SetBool("IsEquipBomb",true);
			_animator.SetBool("IsEquipNone",false);
			_animator.SetBool("IsEquipGun",false);
			_animator.SetBool("IsEquipMine",false);
		}  
		else if (name.CompareTo("Sword(Clone)")==0) {
			TextAsset tmp = Resources.Load ("Button-C", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
			_animator.SetBool("IsEquipSword",true);
			_animator.SetBool("IsEquipBomb",false);
			_animator.SetBool("IsEquipNone",false);
			_animator.SetBool("IsEquipGun",false);
			_animator.SetBool("IsEquipMine",false);
		} 
		else if (name.CompareTo("Mine(Clone)")==0) {
			TextAsset tmp = Resources.Load ("Button-C", typeof(TextAsset)) as TextAsset;
			zoneFight.GetDisplayTex ().LoadImage (tmp.bytes);
			_animator.SetBool("IsEquipSword",false);
			_animator.SetBool("IsEquipBomb",false);
			_animator.SetBool("IsEquipNone",false);
			_animator.SetBool("IsEquipGun",false);
			_animator.SetBool("IsEquipMine",true);
		}  
		
	

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
		if(_isAttack)
		{

			Attack();

			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("ManagerState"))
				Debug.Log( "ManagerState");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("BombState"))
				Debug.Log( "BombState");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("BombIdle"))
				Debug.Log( "BombIdle");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("BombAttack"))
			{
				Debug.Log( "BombAttack");	
				//GetComponent<Equipment> ()._weapon.GetComponent<Bomb> ().Attack ();	
			}
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("BombWalk"))
				Debug.Log( "BombWalk");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("GunState"))
				Debug.Log( "GunState");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("GunIdle"))
				Debug.Log( "GunIdle");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("GunWalk"))
				Debug.Log( "GunWalk");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("GunAttack"))
				Debug.Log( "GunAttack");	

			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("SwordState"))
				Debug.Log( "SwordState");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("SwordIdle"))
				Debug.Log( "SwordIdle");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("SwordWalk"))
				Debug.Log( "SwordWalk");	
			if(_animator.GetCurrentAnimatorStateInfo (0).IsName("SwordAttack"))
				Debug.Log( "SwordAttack");	


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
		if (this.ctrl) {	
			// Get stick and zone references by IDs...			


			if (walkStick.Pressed ()) {
				if(!_firstPress)
				{
					_firstPress = true;
					_force =40;
				}
				RotateByDirection (walkStick.GetVec3d (true, 0));
				//gameObject.transform.position += walkStick.GetVec3d (true, 0)  * currentSpeed ;
				GetComponent<Rigidbody> ().velocity = walkStick.GetVec3d (true, 0) * currentSpeed * _force;
				_force +=1f;
				if(_force >=80)
					_force =80;
				_isTouchingDPad = true;
			}			
			// Stop when stick is released...			
			else {

				_firstPress = false;


				if(_force <=0)
				{
					_force =0;
					_isTouchingDPad = false;	
				}
				else
				{
					_isTouchingDPad = true;	
					GetComponent<Rigidbody> ().velocity = transform.forward* currentSpeed * _force;
					_force -=3;
					RotateByDirection (walkStick.GetVec3d (true, 0));
				}

			}

			if (zoneFight.JustUniPressed(true, true))
			{
				_animator.SetTrigger (attackHash);
				_isAttack = true;

			}
			
		} else {			
			// processing for keyboard
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;
			RotateByDirection(m_Move);
		}

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
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//_animator.SetTrigger (attackHash);
			_isAttack = true;
		
		}
		//approach 2 move with velocity
		directionMove.Normalize ();
		//gameObject.GetComponent<Rigidbody> ().velocity = directionMove * speed * 100;
		gameObject.transform.position += directionMove * currentSpeed ;
		directionMove = new Vector3 (0, 0, 0);

		return isKeyTouching;
	}

	void HandleAnimation(){
		float move = _isKeyMovePressing || _isTouchingDPad ? 1 : 0;
		_animator.SetFloat("Speed", move);


	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "BotHand" && col.GetComponentInParent<BotControler>().CheckIsAnimation("TripleKick")) {
			GetComponent<Rigidbody> ().velocity = col.gameObject.transform.forward * currentSpeed *30;
			BeHitted();
		}
	}
	IEnumerator WaitOneSecond() { yield return new WaitForSeconds(1); }
	public void BeHitted()
	{
		if (hp <= 0) {
			GetComponent<Animator>().SetTrigger(isDeadHash);
			isDie = true;

		} else {
			hp--;
			GetComponent<Animator>().SetTrigger(isAttackedHash);

		}
	}
	public void SetAnimationAttack()
	{
		if(!_isRunningAnimation)
		{
			_animator.SetTrigger (attackHash);
			_isRunningAnimation = true;
		}
	}
	public void FinishAttack()
	{
		_isAttack = false;
		_isRunningAnimation = false;
	}
	public bool GetIsAttack()
	{
		return _isAttack;
	}
	void Attack()
	{
		if (GetComponent<Equipment> ()._weapon == null) {
			SetAnimationAttack();
			FinishAttack();
			return;
		}
		if (GetComponent<Equipment> ()._weapon.name == "Longbow03(Clone)") 
		{
			GetComponent<Equipment> ()._weapon.GetComponent<LongBowScript> ().characterTransform = gameObject.transform;
			GetComponent<Equipment> ()._weapon.GetComponent<LongBowScript> ().Attack ();

		}
		else if (GetComponent<Equipment> ()._weapon.name == "Gun(Clone)") 
		{
			GetComponent<Equipment> ()._weapon.GetComponent<Gun> ().characterTransform = gameObject.transform;
			GetComponent<Equipment> ()._weapon.GetComponent<Gun> ().Attack ();	
	
		}
		else if (GetComponent<Equipment> ()._weapon.name == "Bomb(Clone)") 
		{
			if(GetComponent<Equipment> ()._weapon.GetComponent<Bomb> ().CheckAllowAttack())
			{			
				SetAnimationAttack();
				_animator.GetBehaviour<BombAttackBehaviour>().player = this.gameObject;
					
			}


		}
		else if (GetComponent<Equipment> ()._weapon.name == "Mine(Clone)") 
		{
			GetComponent<Equipment>()._weapon.GetComponent<Mine> ().characterTransform = gameObject.transform;
			GetComponent<Equipment> ()._weapon.GetComponent<Mine> ().equipTransform = GetComponent<Equipment> ()._weapon.transform;
			GetComponent<Equipment> ()._weapon.GetComponent<Mine> ().Attack ();	

		}	
		else if (GetComponent<Equipment> ()._weapon.name == "Sword(Clone)") 
		{
			GetComponent<Equipment>()._weapon.GetComponent<Sword> ().characterTransform = gameObject.transform;
			GetComponent<Equipment> ()._weapon.GetComponent<Sword> ().Attack ();

		}		
	}

}
