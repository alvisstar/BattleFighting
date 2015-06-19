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

	public GameObject targetObject {
		get {
			return _targetObject;
		}
		set {
			_targetObject = value;
		}
	}

	void Start () {
		isDie = false;
	}



	public void Init(Vector3 position,bool isMain)
	{
		gameObject.transform.position = position;
		_isMain = isMain;
	}
	// Update is called once per frame
	void Update () {

		Aim ();

	}

	void Aim()
	{
		Vector3 direction =  targetObject.transform.position -gameObject.transform.position ;
		if(direction.magnitude >2)
		{
			Move (direction);
			GetComponent<Animator>().SetFloat("Speed", 1);
			gameObject.GetComponent<Rigidbody> ().velocity = direction * speed * 10;
		}	
		else
		{
			//AttackTarget();
		}
	}

	void AttackTarget()
	{
		GetComponent<Animator>().SetTrigger(isDeadHash);
		GetComponent<Animator>().SetFloat("Speed", 1);
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
		
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerControler>().CheckIsAnimation("TripleKick"))
		{
			if (hp <= 0) {
 				isDie = true;
				GetComponent<Animator>().SetTrigger(isDeadHash);
			} else {
 				hp--;
				GetComponent<Animator>().SetTrigger(isAttackedHash);
			}

		}
	}
	void FixedUpdate()
	{

	}
}
