using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerJet : Projectile {

	public Vector3 startScale;
	public float lifeTime;
	public float growthRate;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		startScale += Vector3.one*growthRate;
		lifeTime += 0.1;
	}
}
