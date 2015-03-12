﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {


	CharacterController controller;
	bool isGrounded = true;
	private float speed= 0.1f;
	public float jumpSpeed = 2.0f;
	private float gravity = 20f;
	public GameControlScript control;
	private Vector3 moveDirection = Vector3.zero;
	public CountDownScript count;
	public PauseMenuScript pause;

	//audio source reference variable.	
	public AudioSource powerupCollectSound;
	public AudioSource jumpSound;
	public AudioSource snagSoundCollected;
	public AudioSource gameOverSound;

	public GameObject CameraFollow;
	public CharacterController player;
	float playerZposition;
	public GameObject platform;
	private float pSpeed = 5f;
	private GameObject platformHolder;
	private List<GameObject> platformList;
	private int platformCount = 0;
	private float distance;
	// * * * * * spawn part* * * * * *
	
	
	public GameObject obstacle;
	public GameObject powerup;
	float timeElapsed = 0;
	public float spawnCycle = 1.0f;
	bool spawnPowerUp = true;
	private float spawnPosition;
	private float xPosition;
	private float zPosition;
	private Vector3 pMov;
	public Transform currentPlatform;




	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();

		platformList = new List<GameObject> ();
		player = GetComponent<CharacterController> ();
		//platformHolder = Instantiate (platform, new Vector3 (3.52f, 3.09f, 15.5f), Quaternion.identity);
		platformList.Add((GameObject)Instantiate (platform, new Vector3 (-13.35f,-4.16f, 19.5f), Quaternion.identity));

	}
	
	// Update is called once per frame
	void Update () {
				#region playerMovement
				
		distance = Vector3.Distance(player.transform.position,platform.transform.position);
					playerZposition = player.transform.position.z - 5.64f;
					transform.position += new Vector3 (0, 0, speed);
					CameraFollow.transform.position = new Vector3 (0f, 3.9f, playerZposition);
					pMov = new Vector3 (Input.GetAxis ("Horizontal") * pSpeed * 50, 0, 0);
					
					if (player.isGrounded) {
					player.SimpleMove (pMov);	
					GetComponent<Animation>().Play ("run");
			
					if (pause.paused == false) {
						gameObject.GetComponent<AudioSource> ().enabled = true;
					} else {
						gameObject.GetComponent<AudioSource> ().enabled = true;
					}
			jumpSound.Stop ();
		}
					
					if (Input.GetButton ("Jump") && player.isGrounded) {
						isGrounded = false;
						GetComponent<Animation>().Stop ("run");
						GetComponent<Animation>().Play ("jump_pose");
						jumpSound.Play ();
						gameObject.GetComponent<AudioSource> ().enabled = false;
						moveDirection.y = jumpSpeed;
		}
				
		player.Move (moveDirection);
		moveDirection.y -= gravity * Time.deltaTime;
		#endregion		

				if (controller.isGrounded)
						isGrounded = true;
				moveDirection.y -= gravity * Time.deltaTime;
				controller.Move (moveDirection * Time.deltaTime);

				if (control.isGameOver) {
						gameObject.GetComponent<AudioSource> ().enabled = false;
				} else {
						gameOverSound.Play ();
				}
		//Debug.Log ("distance is : " + player.bounds.center.z);
		if (distance > 3 && distance < 3.1) {
			platformList.Add ((GameObject)Instantiate (platform, new Vector3 (-13.35f, -4.16f, player.transform.forward.z + 30.0f), Quaternion.identity));	
			platformCount++;
		}

		if (currentPlatform == null)
						return;

		if ((currentPlatform.GetComponent<Renderer> ().bounds.max.z + currentPlatform.GetComponent<Renderer> ().bounds.min.z) / 2 < transform.position.z) {
						Debug.Log ("Half point reached");	
			platformList.Add ((GameObject)Instantiate (platform, new Vector3 (-13.35f, -4.16f, player.transform.forward.z + 30.0f), Quaternion.identity));	
			platformCount++;
			currentPlatform = platformList[platformList.Count - 1].transform.FindChild("Ground");
				} else {
			Debug.Log("Half point not reached")	;	
		}
	}

			

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("OnCollisionEnter " + other.gameObject.tag);
		if (other.gameObject.tag == "surface") {
			currentPlatform = other.gameObject.transform;
		}
		if (other.gameObject.tag == "obsticle") {
			// do something
			Debug.Log("hit player!!");
			control.AlchoholCollected();
			snagSoundCollected.Play(); // playing the obsticle sound
		} 
		if (other.gameObject.tag == "Powerup") {
			control.PowerUpCollected();
			powerupCollectSound.Play (); // playing the powerup sound
		}

	//Destroy(other.gameObject);
	}
}
