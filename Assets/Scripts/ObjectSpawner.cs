using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		//GameObject go = Resources.Load<GameObject>("CatPrefab");

		GameObject go = Instantiate<GameObject>(prefab);

		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
