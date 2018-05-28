using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public Animator animBrute;
	public CharacterController bruteMove;
	public Vector3 bruteMoveDir=Vector3.zero;

	public float verticalMove;
	public float horizontalMove;
	public float spiritRun;

	public float playerSpeed = 5.0f;
	public float playerVelocity=0.0f;
	public float playerGrav=20.0f;
	public float playerJumpSpeed=1.0f;
	public bool actionPerform;

	public float getSpeed;
	public float getDirection;

	// Use this for initialization
	void Start () {
		actionPerform = false;
		animBrute = GetComponent<Animator> ();
		bruteMove = GetComponent<CharacterController> ();		
	}
	
	// Update is called once per frame
	void Update () {
		playerMovement ();
		playerAnimationController ();
	}

	public void playerAnimationController(){
		getSpeed = animBrute.GetFloat ("Speed");
		getDirection = animBrute.GetFloat ("Direction");

		verticalMove = Input.GetAxis ("Vertical");
		horizontalMove = Input.GetAxis ("Horizontal");

		if (getSpeed == 0 && getDirection == 0) {
			spiritRun = 0.0f;
			actionPerform = false;
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			if (getSpeed != 0 || getDirection != 0) {
				spiritRun = 0.2f;
				playerSpeed = 10.0f;
			} else {
				spiritRun = 0.0f;
				playerSpeed = 5.0f;
			}
		}
	}

	public void playerMovement(){
		bruteMoveDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		bruteMoveDir = transform.TransformDirection (bruteMoveDir);
		playerVelocity -= playerGrav * Time.deltaTime;
		bruteMoveDir.y = playerVelocity;
		bruteMoveDir *= playerSpeed;
		bruteMove.Move (bruteMoveDir * Time.deltaTime);
		actionPerform = true;

		if (bruteMove.isGrounded) {
			playerVelocity = 0;
			if (Input.GetKey (KeyCode.Space)) {
				playerVelocity = playerJumpSpeed;
			}
		}
	}

	void LateUpdate(){
		animBrute.SetFloat ("Speed", verticalMove);
		animBrute.SetFloat ("Direction", horizontalMove);
		animBrute.SetFloat ("Sprite", spiritRun);
		animBrute.SetFloat ("JumpSpeed", playerVelocity);
	}
}
