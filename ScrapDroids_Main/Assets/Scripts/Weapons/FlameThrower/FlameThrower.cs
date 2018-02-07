using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon {

	public GameObject projectile;

	//vfx vars
	public Transform muzzleFlashSocket;
	public GameObject muzzleFlashFX;
	public GameObject emptyFX;

	private float spinUp;

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}

	public override void Update () {
		base.Update ();

		if (!isEmpty && ammo <= 0) {
			Instantiate (emptyFX, muzzleFlashSocket.position, muzzleFlashSocket.rotation * Quaternion.Euler(0,-90,0), null);
			isEmpty = true;
		}
	}

	public override void Fire() {
		if (currentCoolDown <= 0) {
			if (ammo > 0) {
				ammo -= 1;

				Instantiate (muzzleFlashFX, muzzleFlashSocket.position, muzzleFlashSocket.rotation, muzzleFlashSocket);

				Instantiate (projectile, muzzleFlashSocket.position, muzzleFlashSocket.rotation);
			}
		}
	}
}
