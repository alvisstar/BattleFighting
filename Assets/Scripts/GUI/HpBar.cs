using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {
	
	public float currentHp = 1;
	Vector2 pos = new Vector2(0,0);
	Vector2 size = new Vector2(150,15);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<PlayerControler> ()) {
			currentHp = GetComponent<PlayerControler> ().hp;
		} else if (GetComponent<BotControler> ()) {
			currentHp = GetComponent<BotControler> ().hp;
		}
	}
	
	void OnGUI()
	{
		Vector3 viewportPos = Camera.current.WorldToScreenPoint (transform.position);
		Vector2 guiPos = new Vector2 (viewportPos.x - size.x / 2, (-viewportPos.y + Screen.height));
		pos = guiPos;
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
		GUI.BeginGroup(new Rect (pos.x, pos.y, size.x * Mathf.Clamp01(currentHp), size.y));
		GUI.DrawTexture(new Rect(0, 0, size.x, size.y), progressBarFull);
		GUI.EndGroup();		
	}
}
