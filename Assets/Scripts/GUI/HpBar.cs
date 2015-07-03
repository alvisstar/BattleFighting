using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {
	
	public float currentHp = 1;
	public GameObject owner;
	public GameObject currentHpObject;
	public GameObject maxHpObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (owner.GetComponent<PlayerControler> ()) {
			transform.position = owner.GetComponent<PlayerControler> ().headTranform.position;
			currentHp = owner.GetComponent<PlayerControler>().hp / owner.GetComponent<PlayerControler>().maxHp;
		} else if (owner.GetComponent<BotControler> ()) {
			transform.position = owner.GetComponent<BotControler> ().headTranform.position;
			currentHp = owner.GetComponent<BotControler>().hp / owner.GetComponent<BotControler>().maxHp;
		}
		UpdateHpBar ();

		if (currentHp <= 0) {
			Destroy (gameObject);
		}
	}

	void UpdateHpBar() {
		Vector3 scale = new Vector3 (currentHp, currentHpObject.transform.localScale.y, currentHpObject.transform.localScale.z);
		currentHpObject.transform.localScale = scale;
	}
}
