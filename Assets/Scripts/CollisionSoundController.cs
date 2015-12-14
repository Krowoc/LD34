using UnityEngine;
using System.Collections;

public class CollisionSoundController : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(!audioSource.isPlaying)
			audioSource.Play();
	}
}
