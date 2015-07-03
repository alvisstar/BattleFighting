using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {
	
	public float currentHp = 1;
	public GameObject owner;
	public GameObject currentHpPivot;
	public GameObject maxHpObject;
	public GameObject currentHpObject;

	private float currentTime;
	private float remainShowTime;
	private float showTime = 2;
	private float oldHp;

	public Material[] materials;

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

		// destroy hp bar when current hp <= 0
		if (currentHp <= 0) {
			Destroy (gameObject);
		}

		// auto hide and show hp bar
		if (oldHp != currentHp) {
			remainShowTime = showTime;
		}
		remainShowTime -= Time.deltaTime;
		ShowHpBar (remainShowTime > 0);
		oldHp = currentHp;

		// set color for health bar
		if (currentHp >= 0.75) {
			currentHpObject.GetComponent<Renderer>().material = materials[0];
		} else if (currentHp >= 0.5 && currentHp < 0.75) {
			currentHpObject.GetComponent<Renderer>().material = materials[1];
		} else if (currentHp >= 0.25 && currentHp < 0.5) {
			currentHpObject.GetComponent<Renderer>().material = materials[2];			
		} else if (currentHp > 0 && currentHp < 0.25) {
			currentHpObject.GetComponent<Renderer>().material = materials[3];			
		}
	}

	void UpdateHpBar() {
		Vector3 scale = new Vector3 (currentHp, currentHpPivot.transform.localScale.y, currentHpPivot.transform.localScale.z);
		currentHpPivot.transform.localScale = scale;
	}

	void ShowHpBar(bool isShow) {
		currentHpPivot.GetComponentInChildren<MeshRenderer> ().enabled = isShow;
		maxHpObject.GetComponent<MeshRenderer> ().enabled = isShow;
	}
}
