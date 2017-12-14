using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private float speed = 1.5f;
	private float gravity = 20.0f;

	private CharacterController controller;
	private Animator animator;
	private SpriteRenderer heroSprite;

	private Vector3 moveDirection = Vector3.zero;

	public bool walkLeft = true;

	private float startZ;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
		heroSprite = GetComponent<SpriteRenderer> ();
		heroSprite.flipX = !walkLeft;
		startZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (walkLeft) {
			moveDirection.x = -speed;
		} else {
			moveDirection.x = speed;
		}

		if (controller.isGrounded) {
			moveDirection.y = 0;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

	}
		
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Ground") {
			KillMe ();
		}
		if (hit.gameObject.tag == "Player") {
			if (hit.normal.y < -0.7f) {
				GameScript.S.AddScore (10f);
				KillMe ();
			} else {
				GameScript.S.AddScore (-0.5f);
			}
		} 

	}

	void KillMe() {
		controller.enabled = false;
		Destroy (this.gameObject);
		PlayerController.S.BouncePlayer ();
	}

	public void Turn(string str) {
		if (str == "left") {
			walkLeft = true;
		} else {
			walkLeft = false;
		}
	}


}
