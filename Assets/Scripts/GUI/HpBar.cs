using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {
	
	public float currentHp = 1;
	public GameObject owner;
	public GameObject currentHpObject;
	public GameObject maxHpObject;

	private float currentTime;
	private float remainShowTime;
	private float showTime = 2;
	private float oldHp;

	// Use this for initialization
	void Start () {
		ShowHpBar (false);
		oldHp = currentHp;
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

		if (oldHp != currentHp) {
			remainShowTime = showTime;
			Debug.Log(currentHp);
		}
		remainShowTime -= Time.deltaTime;
		ShowHpBar (remainShowTime > 0);
		oldHp = currentHp;
	}

	void UpdateHpBar() {
		Vector3 scale = new Vector3 (currentHp, currentHpObject.transform.localScale.y, currentHpObject.transform.localScale.z);
		currentHpObject.transform.localScale = scale;
	}

	void ShowHpBar(bool isShow) {
		currentHpObject.GetComponentInChildren<MeshRenderer> ().enabled = isShow;
		maxHpObject.GetComponent<MeshRenderer> ().enabled = isShow;
	}
}
