using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YKill : MonoBehaviour {
	
	// Use this for initialization
	void OnCollisionEnter(Collision col) {
		Debug.Log ("AAAAAA");
		if (col.gameObject.GetComponent<DamageModule> () != null) {
			col.gameObject.GetComponent<DamageModule> ().takeDamage ("Generic", 100000);
		}
	}
}
