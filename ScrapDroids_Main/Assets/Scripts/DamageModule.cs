using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageModule : MonoBehaviour {
	
	public int team;
		//0 = player team
		//1 = enemy team
		//2 = environment team

	public Dictionary<string, float> DamageModifier = new Dictionary<string, float>()
		{
			{"Generic",1},
			{"Fire",   1},
			{"Energy", 1},
			{"Acid",   1}
		};

	public float maxHealth;
	public float health;

	public GameObject deathFX;

	void Start () {
		health = maxHealth;
	}

	void Update () {
		if (health <= 0) {
			kill ();
		}
	}

	public void takeDamage(string type, float dmg) {
		float mod = 1;
		Debug.Log (dmg + " Damage * " + type);
		DamageModifier.TryGetValue(type, out mod);
		health -= dmg * mod;
	}

	public float currentHealth() {
		return health;
	}

	void kill() {
		Destroy (gameObject);
		//play deathFX
		//put back into the recycle pool
	}

}