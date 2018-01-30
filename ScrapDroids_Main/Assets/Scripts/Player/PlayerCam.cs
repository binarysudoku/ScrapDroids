using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {

	public GameObject playerBot;
	public Transform mainCam;

	public float camSpeed = 1f;
	public float snapDistance = 20f;
	public float snapBias = 0.5f;
	public float aimOffsetDistance = 4f;
	public float defaultZoom = 20f;

	//internal variables
	private Vector3 targetPoint;
	private float currentZoom;
	private GameObject currentPOI;
	private Vector3 aimOffset;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		aimOffset = new Vector3 (Input.GetAxis ("X Aim"), 0f, Input.GetAxis ("Z Aim"));

		UpdateCamTarget ();

		Debug.DrawRay (targetPoint, Vector3.up * 10, Color.green);

		transform.position = Vector3.Lerp (transform.position, targetPoint, camSpeed);
		mainCam.localPosition = Vector3.Lerp (mainCam.localPosition, -1 * mainCam.forward * currentZoom, camSpeed);
	}

	void UpdateCamTarget() {

		//iterate all weapons and pick the closest that is within attachRange
		GameObject[] allPOIObjects = GameObject.FindGameObjectsWithTag("Point Of Interest");
		currentPOI = GetClosestValidPOI (allPOIObjects);

		if (currentPOI != null) {
			Debug.DrawRay (currentPOI.transform.position, Vector3.up * 10, Color.red);

			currentZoom = currentPOI.GetComponent<PointOfInterest> ().zoomDistance;

			targetPoint = ((playerBot.transform.position + currentPOI.transform.position) * snapBias) + (aimOffset * aimOffsetDistance);
		} else {
			currentZoom = defaultZoom;

			targetPoint = playerBot.transform.position + (aimOffset * aimOffsetDistance);
		}
	}

	GameObject GetClosestValidPOI(GameObject[] poiObjects)
	{
		GameObject bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = playerBot.transform.position;
		foreach(GameObject potentialTarget in poiObjects)
		{
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if((dSqrToTarget < closestDistanceSqr) && (dSqrToTarget < snapDistance*snapDistance))
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}
		return bestTarget;
	}
}
