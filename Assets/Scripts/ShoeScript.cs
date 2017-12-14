using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeScript : MonoBehaviour {


	private float speed;

	// Use this for initialization
	void Start () {
		speed = Random.Range (10, 15);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, Time.deltaTime * speed);
	}
}
