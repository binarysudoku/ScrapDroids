using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : Enemy {

	public NavMeshAgent agent;
	public GameObject playerTest;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, playerTest.transform.position) < aggroRange) {
			agent.SetDestination (playerTest.transform.position);

			agent.isStopped = false;
		} else {
			agent.isStopped = true;
		}
	}
}
