﻿using UnityEngine;
using System.Collections;

public class OneOffCat : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetTrigger("Idle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
