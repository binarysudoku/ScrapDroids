using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerJet : Projectile {

	public Vector3 startScale;
	public float lifeTime;
	public float growthRate;

	private float lifeCounter;

	void Start () {
		gameObject.transform.localScale = startScale;

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce(gameObject.transform.forward * Random.Range(80000,200000) );

		lifeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale += Vector3.one*growthRate;
		lifeCounter += 0.1f;

		if (lifeCounter > lifeTime) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		DamageModule dmgmodule = col.transform.root.GetComponent<DamageModule>();

		if (dmgmodule != null) {
			dmgmodule.TakeDamage("Generic", damage);
		}
	}
}
