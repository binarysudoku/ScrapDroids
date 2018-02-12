using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBurnDetector : MonoBehaviour {

	public FlameThrowerOil oil;

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<FlameThrowerJet>() != null & oil.burnCounter == -1) {
			oil.Burn();
		}
	}
}
