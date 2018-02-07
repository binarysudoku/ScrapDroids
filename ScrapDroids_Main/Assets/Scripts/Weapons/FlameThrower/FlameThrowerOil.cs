using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerOil : Projectile {

	public GameObject impactFX;
	public Transform pool;

	Rigidbody rb;

	public Vector3 poolSize;

	void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce(gameObject.transform.forward * Random.Range(50000,100000) );
		pool.localScale = gameObject.transform.lossyScale;
	}

	// Update is called once per frame
	void Update() {

	}

	void OnCollisionEnter(Collision col) {
		pool.localScale = poolSize;
		Debug.Log(col.gameObject);
	}
	
}