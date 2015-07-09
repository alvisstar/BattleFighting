using UnityEngine;
using System.Collections;

public class BotControler : AdvancedFSM {

	// Use this for initialization
	public enum State
	{
		BOT_IDLE,
		BOT_CHASE,
		BOT_ATTACK,
		BOT_HURT
	};
	bool allowBeHit;
	public State state;
	public float speed = 0.1f;
	private Vector3 directionMove = new Vector3(0,0,0);
	public bool isDie;
	public float hp;
	public float maxHp;
	// Use this for initialization
	private bool _isMain = false;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	int beAttackHash = Animator.StringToHash("BeAttack");
	int dieHash = Animator.StringToHash("Die");
	int isAttackHash = Animator.StringToHash("Attack");
	float m_TurnAmount;
	float m_ForwardAmount;
	private GameObject _targetObject;
	public float timeFromAttack;
	public GameObject targetObject {
		get {
			return _targetObject;
		}
		set {
			_targetObject = value;
		}
	}
	public AIBotManager controller;
	public GameObject hpBarPrefab;
	public Transform headTranform;
	public float recoveryTime;
	void Start () {
		hp = maxHp = 20;
		speed = 0.1f;
		isDie = false;
		NotificationCenter.DefaultCenter.AddObserver(this, "OnBombExplode");
		NotificationCenter.DefaultCenter.AddObserver(this, "OnMineExplode");
		ConstructFSM ();
		
		GameObject hpBarObject = Instantiate (hpBarPrefab);
		hpBarObject.GetComponent<HpBar> ().owner = gameObject;
		GetComponent<Animator>().SetBool ("IsEquipNone", true);
		state = State.BOT_IDLE;
		recoveryTime = 0;
		allowBeHit = true;
	}
	private void ConstructFSM()
	{

		
		ChaseState chase = new ChaseState(controller);
		//chase.AddTransition(Transition.InclosurePlayer, FSMStateID.Rounding);
		//chase.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
		chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
		chase.AddTransition(Transition.NoHealth, FSMStateID.Dead);
		
		
		
		AttackState attack = new AttackState(controller);
		//attack.AddTransition(Transition.InclosurePlayer, FSMStateID.Rounding);
		attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
		attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);

		/*RoundingState rounding = new RoundingState(controller);
		rounding.AddTransition(Transition.ReachPlayer, FSMStateID.ChaseToAttack);
		rounding.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
		rounding.AddTransition(Transition.NoHealth, FSMStateID.Dead);
		

		ChaseToAttack chaseToAttack = new ChaseToAttack(controller);
		chaseToAttack.AddTransition(Transition.TouchPlayer, FSMStateID.Attacking);
		chaseToAttack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
		chaseToAttack.AddTransition(Transition.NoHealth, FSMStateID.Dead);*/

		AddFSMState(chase);
		AddFSMState(attack);
		//AddFSMState(rounding);
		//AddFSMState(chaseToAttack);
	}
	void OnDestroy () {
		NotificationCenter.DefaultCenter.RemoveObserver(this, "OnBombExplode");
		NotificationCenter.DefaultCenter.RemoveObserver(this, "OnMineExplode");
	}

	void OnBombExplode (NotificationCenter.Notification arg)
	{
		Hashtable hash  = arg.data;
		Vector3 position =(Vector3) hash["Position"];
		if((position - gameObject.transform.position).magnitude <5)
			BeHitted ();
	}
	
	void OnMineExplode (NotificationCenter.Notification arg)
	{
		Hashtable hash  = arg.data;
		Vector3 position =(Vector3) hash["Position"];
		if ((position - gameObject.transform.position).magnitude < 5)
			BeHitted ();
	}
	
	public void Init(Vector3 position,bool isMain)
	{
		gameObject.transform.position = position;
		_isMain = isMain;
	}
	// Update is called once per frame
	void Update () {
		if (controller) {

			
		
		} 	
		if(state == State.BOT_HURT)
		{
			recoveryTime -=Time.deltaTime;
		}

	}
	void FixedUpdate () {
		
		CurrentState.Reason (controller.target,transform);
		CurrentState.Act (controller.target,transform);
	}




	public void Move(Vector3 direction)
	{
		RotateByDirection (direction);
		GetComponent<Animator>().SetFloat("Speed", 1);

		gameObject.transform.position += direction.normalized * speed ;
	}
	public void MoveAround(Vector3 direction)
	{

		//direction= Quaternion.AngleAxis(Time.deltaTime * 20, Vector3.forward) * direction;
		//gameObject.transform.position += direction * speed ;
	}

	public void Idle()
	{
		GetComponent<Animator>().SetFloat("Speed", 0);
	}

	public void AttackTarget()
	{
		GetComponent<Animator>().SetTrigger(isAttackHash);
		GetComponent<Animator>().SetFloat("Speed", 1);
		state = State.BOT_ATTACK;
		recoveryTime = 0;


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
	public bool CheckIsAnimation(string name)
	{
		//int atakState = Animator.StringToHash(name); 
		if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName(name))
			return true;
		return false;
		
	}
	void OnCollisionEnter (Collision col)
	{
		//if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerControler>().CheckIsAnimation("TripleKick"))
		//{
		//	BeHitted();
		//
		//}
	}
	public void BeHitted()
	{
		if (hp <= 0) {
			isDie = true;
			GetComponent<Animator>().SetTrigger(dieHash);
		} else {
			hp--;
			GetComponent<Animator>().SetTrigger(beAttackHash);
			state = State.BOT_HURT;
			recoveryTime =1f;
		}
	}

	void OnTriggerExit(Collider col)
	{
		allowBeHit = true;
	}
	void OnTriggerEnter(Collider col)
	{
		if (allowBeHit && col.gameObject.tag == "Hand" && 
		    (col.GetComponentInParent<PlayerControler>().CheckIsAnimation("AttackR")||col.GetComponentInParent<PlayerControler>().CheckIsAnimation("AttackL")) && !isDie
		    && !col.GetComponentInParent<PlayerControler>().onTrigger) {
			GetComponent<Rigidbody> ().velocity = col.gameObject.transform.forward  *0.15f*60;		
			BeHitted();
			allowBeHit = false;

		}
	}
}
