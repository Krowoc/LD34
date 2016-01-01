using UnityEngine;
using System.Collections;

public class BeesEffect : MonoBehaviour {
	void Update () {
		transform.Rotate (Vector3.up, Time.deltaTime * 50f, Space.World);
	}
}
