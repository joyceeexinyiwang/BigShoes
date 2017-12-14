using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

	static public PlayerController S; 
	private CharacterController controller;

	public float speed = 6.0F;
	private float jumpSpeed = 15F;
	private float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	public float leftSideOffset;

	private GameObject gameCamera;
	private bool stillAlive;
	private Animator animator;

	private SpriteRenderer heroSprite;
	private bool faceRight;

	private float startZ;

	public GameObject Notification;
	private Text NotificationText;

	void Start() {
		S = this;

		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator> ();
		gameCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		heroSprite = GetComponent<SpriteRenderer> ();

		stillAlive = true;
		faceRight = false;
		startZ = transform.position.z;

		NotificationText = Notification.GetComponent<Text> ();
	}

	void Update() {
		
		if (controller.isGrounded) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			moveDirection *= speed;
			if (Input.GetButton ("Jump"))
				moveDirection.y = jumpSpeed;

		} else {
			moveDirection.x += Input.GetAxis ("Horizontal") * 0.2f;
			moveDirection.x = Mathf.Clamp (moveDirection.x, -speed, speed);
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		Vector3 checkposition = new Vector3(transform.position.x, transform.position.y, startZ);
		transform.position = checkposition;

		animator.SetBool ("grounded", controller.isGrounded);
		animator.SetFloat ("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
		animator.SetBool ("alive", stillAlive);

		if (stillAlive) {
			// sprite direction
			if (Input.GetAxis ("Horizontal") < 0 && faceRight) {
				faceRight = false;
				heroSprite.flipX = false;
			} else if (Input.GetAxis ("Horizontal") > 0 && !faceRight) {
				faceRight = true;
				heroSprite.flipX = true;
			}
		}
			
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.moveDirection == Vector3.up) {
			if ((hit.transform.tag == "Platform") && (moveDirection.y > 0.0f)) {
				Debug.Log (hit.transform.tag);
				moveDirection.y = 0;
			}
		}
		if (hit.collider.tag == "Ground") {
			KillPlayer ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Gate") {
			if (GameScript.S.score >= GameScript.S.targetScore) {
				StartCoroutine(PrepForLevelUp(other));
			}
		}
	}

	public void BouncePlayer() {
		//moveDirection.y = 2.0f;
	}

	// level related stuff

	public void KillPlayer() {
		stillAlive = false;
		controller.enabled = false;
		GameScript.S.lives -= 1;
		if (GameScript.S.lives < 0) {
			StartCoroutine (PlayerDies ());
		} else {
			StartCoroutine (RestartLevel ());
		}
	}

	IEnumerator PrepForLevelUp(Collider other) {
		if (SceneManager.GetActiveScene ().name == "Level 3") {
			NotificationText.text = "Good job big girl!";
		} else {
			NotificationText.text = "BIGGGGGER SHOES!";
		}
		for (int i = 0; i < 80; i++) {
			yield return new WaitForSeconds(0.005f);
			other.transform.Rotate(0, 0, 6f);
		}
		NotificationText.text = "";
		GameScript.S.LevelUp ();
	}

	IEnumerator PlayerDies() {
		NotificationText.text = "Game over :(";
		yield return new WaitForSeconds(3f);
		NotificationText.text = "";
		GameScript.S.GoHome ();
	}

	IEnumerator RestartLevel() {
		yield return new WaitForSeconds(3f);
		GameScript.S.ReStartLevel ();
	}

}
