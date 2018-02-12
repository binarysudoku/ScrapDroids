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
	public float burnCounter;
	private GameObject fireObject;

	void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce(gameObject.transform.forward * Random.Range(50000,100000) );
		pool.localScale = gameObject.transform.lossyScale;

		burnCounter = -1;
	}

	// Update is called once per frame
	void Update() {
		if (burnCounter != -1) {
			if (burnCounter <= 0) {
				Destroy(fireObject);
				Destroy(gameObject);
			} else {
				burnCounter -= 0.01f;
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		pool.localScale = poolSize;
		Debug.Log(col.gameObject);
	}

	public void Burn() {
		burnCounter = burnTime;
		fireObject = Instantiate(burningFX, gameObject.transform.position , Quaternion.identity, gameObject.transform);
	}
}