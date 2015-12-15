using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	Animator anim;
	
	[SerializeField]
	float collisionForce = 3.0f;


	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pounce()
	{
		if(anim != null)
			anim.SetTrigger("Pounce");

	}

	public void Death()
	{
		StartCoroutine(DeathCoroutine());
	}

	IEnumerator DeathCoroutine()
	{
		anim.SetTrigger("Collide");

		GetComponent<AudioSource>().Play();

		transform.SetParent(null);

		Rigidbody rBody = gameObject.AddComponent<Rigidbody>();

		rBody.AddForce(collisionForce, 0f, collisionForce, ForceMode.VelocityChange);

		Manager.singleton.updateCatScore();

		yield return new WaitForSeconds(8.0f);


		GameObject.Destroy(gameObject);
		
	}

	public void Win()
	{
		
	}
}
