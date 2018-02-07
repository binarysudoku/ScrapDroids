using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlasterBolt : Projectile {

	public GameObject impactFX;
	public LineRenderer traceFX;

	public LayerMask hitMask;

	public float traceFade;
	public float defaultTraceWidth;
	private float currentTraceWidth;
	public bool alive;

	void Start () {
		
		RaycastHit hitData;

		traceFX.SetPosition(0, transform.position);

		if (Physics.Linecast (transform.position, transform.position + transform.forward * 10000, out hitData, hitMask)) {
			Instantiate (impactFX, hitData.point, Quaternion.LookRotation (hitData.normal));

			traceFX.SetPosition (1, hitData.point);

			DamageModule dmgmodule = hitData.transform.root.GetComponent<DamageModule> ();

			if (dmgmodule != null) {
				dmgmodule.TakeDamage("Generic", damage);
			}

		} else {
			traceFX.SetPosition (1, transform.position + transform.forward * 10000);
		}
		
		currentTraceWidth = defaultTraceWidth;
	}
	
	// Update is called once per frame
	void Update () {
		currentTraceWidth = Mathf.Lerp (currentTraceWidth, 0, traceFade);
		traceFX.widthMultiplier = currentTraceWidth;

		if (currentTraceWidth <= 0.01) {
			Destroy (gameObject);
		}
	}
}
