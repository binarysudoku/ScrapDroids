using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerOil : Projectile {

	public GameObject impactFX;
	public GameObject burningFX;
	public Transform pool;

	Rigidbody rb;

	public Vector3 poolSize;
	public float burnTime;
	public float lifeTime;

	private float lifeCounter;
	private float burnCounter;
	private GameObject fireObject;

	void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce(gameObject.transform.forward * Random.Range(50000,100000) );
		pool.localScale = gameObject.transform.lossyScale;

		burnCounter = -1;
	}

	// Update is called once per frame
	void Update() {
		if (burnCounter <= 0 & burnCounter != -1) {
			Destroy(fireObject);
			Destroy(gameObject);
		}
	}

	/*
	private void OnTriggerStay(Collider other) {

		if (other.GetComponent<DamageModule>() != null) {
			if (other.GetComponent<DamageModule>().team != 0) {
				other.GetComponent<DamageModule>().TakeDamage("Fire", damage);
			}
		}
	}
	*/
	
	void OnCollisionEnter(Collision col) {
		pool.localScale = poolSize;
		Debug.Log(col.gameObject);
	}

	public void Burn() {
		burnCounter = burnTime;
		fireObject = Instantiate(burningFX, gameObject.transform, false);
	}
}