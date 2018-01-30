using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour {

	public CharacterController control = null;

	//bot parts
	public Transform hull;
	public Transform legs;
	public Transform socketLeft;
	public Transform socketRight;

	private GameObject weaponLeft;
	private GameObject weaponRight;

	//vfx

	public GameObject ejectFX;

	//state variables
	private bool inputEnabled; // MAYBE REPLACE WITH AN ENUM STATE SYSTEM LATER
	private bool fireEnabled;
	private Vector3 moveInput = new Vector3(0, 0, 0);
    private Vector3 aimInput = new Vector3(0, 0, 0);

	//gameplay config
	public float accel = 320;
	public float drag = 0.8f;
    public float turnTime = 0.05f;
    public float aimTime = 0.05f;
    public float turnTolerance = 30;
	public float attachRange = 2;
	public bool autoPickup = false;

	//gameplay variables
    public float maxHP;

	// Use this for initialization
	void Start () {
		inputEnabled = true; //CHANGE LATER WHEN GAMEPLAY LOOP IS IMPLEMENTED
		fireEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (inputEnabled) {
			UpdateMovement();

			UpdateBotAngles();

			if (fireEnabled) {
				UpdateWeapon("L", weaponLeft);
				UpdateWeapon("R", weaponRight);
			}
		}
	}

	void UpdateMovement() {

        //movement input
        if ((Input.GetAxis ("X Move") != 0) || (Input.GetAxis ("Z Move") != 0)) {
			moveInput = new Vector3 (Input.GetAxis ("X Move"), 0f, Input.GetAxis ("Z Move"));
		}

        //aim input
        if ((Input.GetAxis("X Aim") != 0) || (Input.GetAxis("Z Aim") != 0))
        {
            aimInput = new Vector3(Input.GetAxis("X Aim"), 0f, Input.GetAxis("Z Aim"));
        }

	    //make sure legs are facing movement direction
		if (moveInput != Vector3.zero) {
			float testangle = (Mathf.Abs (legs.eulerAngles.y - Quaternion.LookRotation (moveInput).eulerAngles.y));

			if (360 - turnTolerance < testangle | testangle < turnTolerance) {
				if ((Input.GetAxis ("X Move") != 0) || (Input.GetAxis ("Z Move") != 0)) {
					moveInput += moveInput.normalized * Time.deltaTime * accel;
				}
				control.SimpleMove (moveInput);
			} else {
				control.SimpleMove (Vector3.zero);
			}
		}

		moveInput *= drag; //friction
	}

	//update hull and leg rotation based on input
	void UpdateBotAngles() {
		//legs angle
		float lyVelocity = 0.0f;
		if (moveInput != Vector3.zero) {
			float lAngle = Mathf.SmoothDampAngle (legs.eulerAngles.y, Quaternion.LookRotation (moveInput.normalized).eulerAngles.y, ref lyVelocity, turnTime);
		
			if ((Input.GetAxis ("X Move") != 0) || (Input.GetAxis ("Z Move") != 0)) {
				legs.eulerAngles = new Vector3 (0, lAngle, 0);
			}
		}

		if (aimInput != Vector3.zero) {
			//hull angle
			float hyVelocity = 0.0f;
			float hAngle = Mathf.SmoothDampAngle (hull.eulerAngles.y, Quaternion.LookRotation (aimInput.normalized).eulerAngles.y, ref hyVelocity, aimTime);

			hull.eulerAngles = new Vector3 (0, hAngle, 0);
		}
	}

	//pass inputs to the arm based on the associated input
	GameObject UpdateWeapon(string input, GameObject weaponObject) {
		if (weaponObject != null) {
			
			Weapon weapon = weaponObject.GetComponent(typeof(Weapon)) as Weapon; //get weapon script from the weapon object

			if (input == "R") {
				(weaponRight.GetComponent(typeof(Weapon)) as Weapon).SetIsFiring((Input.GetAxis("FireR") != 0));
			}
			if (input == "L") {
				(weaponLeft.GetComponent(typeof(Weapon)) as Weapon).SetIsFiring((Input.GetAxis("FireL") != 0));
			}

			if (Input.GetButtonDown ("Eject" + input)) {

				if (input == "R") {
					(weaponRight.GetComponent(typeof(Weapon)) as Weapon).SetIsFiring(false);
					weaponRight = null;
					Instantiate (ejectFX, socketRight.position, hull.rotation, socketRight);
				}
				if (input == "L") {
					(weaponLeft.GetComponent(typeof(Weapon)) as Weapon).SetIsFiring(false);
					weaponLeft = null;
					Instantiate (ejectFX, socketLeft.position, hull.rotation * Quaternion.Euler(0,180,0), socketLeft);
				}

				weapon.Eject();
			}
		} else {
			if (Input.GetButtonDown ("Eject" + input)) {

				//iterate all weapons and pick the closest that is within attachRange
				GameObject[] allWeaponObjects = GameObject.FindGameObjectsWithTag("Weapon");
				weaponObject = GetClosestValidWeapon(allWeaponObjects);
				if (weaponObject != null) {
					
					Weapon weapon = weaponObject.GetComponent (typeof(Weapon)) as Weapon;

					if (input == "R") {
						if (weaponRight == null) { //make sure weapon isnt already attached
							weaponRight = weaponObject;
							weapon.Attach(input, socketRight.transform);
						}
					}
					if (input == "L") {
						if (weaponLeft == null) { //make sure weapon isnt already attached
							weaponLeft = weaponObject;
							weapon.Attach (input, socketLeft.transform);
						}
					}
				}
			}
		}
		return weaponObject;
	}

	GameObject GetClosestValidWeapon(GameObject[] weaponObjects)
	{
		GameObject bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach(GameObject potentialTarget in weaponObjects)
		{
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if((dSqrToTarget < closestDistanceSqr) && (dSqrToTarget < attachRange*attachRange) && (potentialTarget.GetComponent(typeof(Weapon)) as Weapon).isAttached != true)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}
		return bestTarget;
	}
}
