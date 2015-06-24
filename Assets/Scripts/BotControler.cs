using UnityEngine;
using System.Collections;

public class BotControler : MonoBehaviour {

	// Use this for initialization
	public float speed = 0.1f;
	private Vector3 directionMove = new Vector3(0,0,0);
	public bool isDie;
	public float hp = 2;
	// Use this for initialization
	private bool _isMain = false;
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	int isAttackedHash = Animator.StringToHash("IsAttacked");
	int isDeadHash = Animator.StringToHash("IsDead");
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
	void Start () {
		speed = 0.1f;
		isDie = false;
		NotificationCenter.DefaultCenter.AddObserver(this, "OnBombExplode");
		NotificationCenter.DefaultCenter.AddObserver(this, "OnMineExplode");

	}

	void OnDestroy () {
		NotificationCenter.DefaultCenter.RemoveObserver(this, "OnBombExplode");
		NotificationCenter.DefaultCenter.RemoveObserver(this, "OnMineExplode");
	}

	void OnBombExplode (NotificationCenter.Notification arg)
	{
		Hashtable hash  = arg.data;
		Vector3 position =(Vector3) hash["Position"];
		if((position - gameObject.transform.position).magnitude <3)
			if (hp <= 0) {
				isDie = true;
				GetComponent<Animator>().SetTrigger(isDeadHash);
			} else {
				hp--;
				GetComponent<Animator>().SetTrigger(isAttackedHash);
			}
	}
	
	void OnMineExplode (NotificationCenter.Notification arg)
	{
		Hashtable hash  = arg.data;
		Vector3 position =(Vector3) hash["Position"];
		if((position - gameObject.transform.position).magnitude <3)
		if (hp <= 0) {
			isDie = true;
			GetComponent<Animator>().SetTrigger(isDeadHash);
		} else {
			hp--;
			GetComponent<Animator>().SetTrigger(isAttackedHash);
		}
	}
	
	public void Init(Vector3 position,bool isMain)
	{
		gameObject.transform.position = position;
		_isMain = isMain;
	}
	// Update is called once per frame
	void Update () {
		if (controller) 
		{      Vector3 relativePos = steer() * Time.deltaTime;
			RotateByDirection (relativePos);
			GetComponent<Animator>().SetFloat("Speed", 1);
			if (relativePos != Vector3.zero)          
				GetComponent<Rigidbody>().velocity = relativePos;
			// enforce minimum and maximum speeds for the boids      
			float speed = GetComponent<Rigidbody>().velocity.magnitude;      
			if (speed > controller.maxVelocity) {        
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized *           controller.maxVelocity;      
			}      
			else if (speed < controller.minVelocity) {        
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized *             controller.minVelocity;     
			}    
		} 	

	}
	void FixedUpdate () {
		

	}
	private Vector3 steer () {    
		Vector3 center = controller.flockCenter -         transform.localPosition;  // cohesion
		Vector3 velocity = controller.flockVelocity -         GetComponent<Rigidbody>().velocity;  // alignment
		Vector3 follow = controller.target.localPosition -         transform.localPosition;  // follow leader
		Vector3 separation = Vector3.zero;
		foreach (BotControler flock in controller.botScripts) {     
			if (flock != this) {        
				Vector3 relativePos = transform.localPosition -             flock.transform.localPosition;
				separation += relativePos / (relativePos.sqrMagnitude);    

			}    
		}
		// randomize    
		Vector3 randomize = new Vector3( (Random.value * 2) - 1,         (Random.value * 2) - 1, (Random.value * 2) - 1);
		randomize.Normalize();
		return (controller.centerWeight * center +         
		        controller.velocityWeight * velocity +         controller.separationWeight * separation +        
		        controller.followWeight * follow +         controller.randomizeWeight * randomize);  
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
			GetComponent<Animator>().SetTrigger(isDeadHash);
		} else {
			hp--;
			GetComponent<Animator>().SetTrigger(isAttackedHash);
		}
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Hand" && col.GetComponentInParent<PlayerControler>().CheckIsAnimation("TripleKick")) {
			BeHitted();
		}
	}
}
