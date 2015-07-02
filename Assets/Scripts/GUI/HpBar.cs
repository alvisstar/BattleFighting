using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {
	
	public float currentHp = 1;
//	Vector2 pos = new Vector2(0,0);
//	Vector2 size = new Vector2(150,15);
//	public Texture2D progressBarEmpty;
//	public Texture2D progressBarFull;
	public GameObject owner;
	public GameObject currentHpObject;
	public GameObject maxHpObject;

	// Use this for initialization
	void Start () {
		UpdateHpBar (0.4f);
	}
	
	// Update is called once per frame
	void Update () {
//		if (GetComponent<PlayerControler> ()) {
//			currentHp = GetComponent<PlayerControler> ().hp / GetComponent<PlayerControler> ().maxHp;
//		} else if (GetComponent<BotControler> ()) {
//			currentHp = GetComponent<BotControler> ().hp / GetComponent<BotControler> ().maxHp;
//		}
		if (owner.GetComponent<PlayerControler>())
			transform.position = owner.GetComponent<PlayerControler>().headTranform.position;
		else if (owner.GetComponent<BotControler>())
			transform.position = owner.GetComponent<BotControler>().headTranform.position;
	}

	void UpdateHpBar(float currentHpz) {
		currentHp = currentHpz;

		Vector3 scale = new Vector3 (currentHp * maxHpObject.transform.localScale.x, currentHpObject.transform.localScale.y, currentHpObject.transform.localScale.z);
		currentHpObject.transform.localScale = scale;

//		Vector3 position = new Vector3 (- (maxHpObject.transform.localScale.x - scale.x) /2 , currentHpObject.transform.position.y, currentHpObject.transform.position.z);
//		currentHpObject.transform.position = position;
	}
	
	void OnGUI()
	{
		//Vector3 viewportPos = Camera.current.WorldToScreenPoint (transform.position);
		//Vector2 guiPos = new Vector2 (viewportPos.x - size.x / 2, (-viewportPos.y + Screen.height));
		//pos = guiPos;
		//GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
		//GUI.BeginGroup(new Rect (pos.x, pos.y, size.x * Mathf.Clamp01(currentHp), size.y));
		//GUI.DrawTexture(new Rect(0, 0, size.x, size.y), progressBarFull);
		//GUI.EndGroup();		
	}
}
