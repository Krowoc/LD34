using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject prefab;
	float posX;

	// Use this for initialization
	void Start () {
		posX = transform.position.x;
		Spawn(prefab);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != posX)
		{
			posX = transform.position.x;
			Spawn(prefab);
		}
	}

	void Spawn(GameObject pf)
	{
		//GameObject go = Resources.Load<GameObject>("CatPrefab");

		GameObject go = Instantiate<GameObject>(pf);

		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
	}
}
