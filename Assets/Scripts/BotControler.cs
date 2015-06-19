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

	int isAttackedHash = Animator.StringToHash("IsAttacked");
	int isDeadHash = Animator.StringToHash("IsDead");

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
		
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
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
