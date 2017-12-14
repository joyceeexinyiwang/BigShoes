using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

	public GameObject player;

	private float xVelocity = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 playerposition = player.transform.position + new Vector3(0f, 3f, 0f);
		Vector3 cameraposition = transform.position;

		// check for advancing camera

		cameraposition.x = Mathf.SmoothDamp (cameraposition.x, playerposition.x, ref xVelocity, 0.5f);
		cameraposition.y = playerposition.y + 5f;

		transform.position = cameraposition;
		
	}
}
