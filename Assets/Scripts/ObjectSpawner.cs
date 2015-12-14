using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject prefab;
	float posX;

	// Use this for initialization
	void Start () {
		posX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != posX)
		{
			posX = transform.position.x;
			Spawn();
		}
	}

	void Spawn()
	{
		//GameObject go = Resources.Load<GameObject>("CatPrefab");

		GameObject go = Instantiate<GameObject>(prefab);

		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
	}
}
