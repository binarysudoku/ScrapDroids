using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon {

	public GameObject oilProjectile;
	public GameObject flameProjectile;

	//vfx vars
	public Transform muzzleFlashSocket;
	public GameObject oilMuzzleFX;
	public GameObject fireMuzzleFX;
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

		if (!isFiring) {
			spinUp = 0f;
		}
	}

	public override void Fire() {
		if (currentCoolDown <= 0) {
			if (ammo > 0) {
				ammo -= 1;
				spinUp += 0.1f;
				if (spinUp < 1) {
					Instantiate(oilMuzzleFX, muzzleFlashSocket.position, muzzleFlashSocket.rotation * Quaternion.Euler(0, -90, 0), null);
					Instantiate(oilProjectile, muzzleFlashSocket.position, muzzleFlashSocket.rotation);
				} else {
					Instantiate(fireMuzzleFX, muzzleFlashSocket.position, muzzleFlashSocket.rotation, muzzleFlashSocket);
					Instantiate(flameProjectile, muzzleFlashSocket.position, muzzleFlashSocket.rotation);
				}
			}
		}
	}
}
