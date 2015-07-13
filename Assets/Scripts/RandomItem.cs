using UnityEngine;
using System.Collections;

public class RandomItem : MonoBehaviour
{
	public GameObject[] _prefabWeapons;
	public GameObject swordPrefabs;
	public GameObject minePrefabs;
	public GameObject bombPrefabs;
	public GameObject hammerPrefabs;
	public GameObject gunPrefabs;
	public int type;
	public int piority;
	public GameObject RandomItemz ()
	{
		int random = Random.Range (0, _prefabWeapons.Length);
//		Debug.Log ("random " + random);
		return _prefabWeapons [random];
	}

	public GameObject GetItem ()
	{

		switch (type) {
		case 1:
			return swordPrefabs;
		case 2:
			return minePrefabs;
		case 3:
			return bombPrefabs;
		case 4:
			return hammerPrefabs;
		case 5:
			return gunPrefabs;
		default:
			return swordPrefabs;
		}

	}

}
