using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Rigidbody rb;
	protected Transform socket; //socket to attach to

	[HideInInspector] public bool isAttached;
	protected bool isLeft; //if the weapon is attached to the left side and should be mirrored
	protected bool isFiring;
	protected bool isEmpty;

	public float massDamage;

	protected float currentCoolDown;

	//common variables
	public int maxAmmo;
	protected int ammo;

	public float maxCoolDown;
	
	public virtual void Start () {
		ammo = maxAmmo;
	}

	// Update is called once per frame
	public virtual void Update () {
		if (currentCoolDown > 0) {
			currentCoolDown -= 0.1f;
		} else {
			if (isFiring) {
				Fire ();
				currentCoolDown = maxCoolDown;
			}
		}

		//TEMPORARY
		Material mat = GetComponentInChildren<Renderer>().material;
		mat.color = Color.	white * (ammo / (maxAmmo + 0.01f));
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Enemy" && rb.velocity.magnitude > 4) {
			DamageModule dmod = collision.transform.root.GetComponent<DamageModule> ();
			Debug.Log (dmod);
			dmod.takeDamage("Generic", massDamage);
			Debug.Log (dmod.currentHealth ());
		}
	}

	//parent function for fire code
	public virtual void Fire() {
	}

	//parent function for attach code
	public void Attach(string side, Transform attachTo) {
		isAttached = true;

		if (side == "L") {
			isLeft = true;
		} else {
			isLeft = false;
		}

		if (isLeft) {
			transform.localScale = new Vector3(-1,1,1); //mirror weapon if on left side
		} else {
			transform.localScale = new Vector3(1,1,1); //mirror weapon if on left side
		}
		
		transform.SetParent(attachTo); //attach to socket
		transform.SetPositionAndRotation(transform.parent.position,transform.parent.rotation); //snap to socket position and rotation

		rb.isKinematic = true;
		rb.useGravity = false;
		rb.transform.SetPositionAndRotation (transform.parent.position, transform.parent.rotation);
	}

	//parent function for eject code
	public void Eject() {
		isAttached = false;

		rb.isKinematic = false;
		rb.useGravity = true;
		rb.AddForce (transform.right*transform.localScale.x*2200);
		rb.AddForce (Vector3.up*500);

		transform.SetParent (null);
	}

	public void SetIsFiring(bool fire) {
		isFiring = fire;
	}
}
