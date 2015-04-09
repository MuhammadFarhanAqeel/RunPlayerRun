using UnityEngine;
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

	private float pSpeed = 5f;
	private GameObject platformHolder;
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


	public List<Transform> Platform;
	Bounds _bounds;
	Transform lastPlatform;
	
	List<Transform> platforms = new List<Transform>();



	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		transform.position = Vector3.zero;
				#region smasher
				for (int i = 0; i<10; i++) {
			
						Transform platform = (Transform)Instantiate (Platform [Random.Range (0, Platform.Count)]);

						platform.transform.position = new Vector3(-13.51f,-4.05f,20.78f);
						if (i > 0) {
								Bounds prevbounds = platforms [i - 1].FindChild ("Surface").GetComponent<MeshRenderer> ().bounds;
								Bounds bounds = platform.FindChild ("Surface").GetComponent<MeshRenderer> ().bounds;
				
								platform.transform.position = platforms[i - 1].position + new Vector3(0,0,bounds.extents.z + prevbounds.extents.z); 
			
								_bounds = bounds;
						} else {
								platform.transform.position = Vector3.zero;
						}
			
						//lastPlatform = platform;
			
						platforms.Add (platform);

						#endregion

				}
		}
	
void FixedUpdate() {
				#region playerMovement
				
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
		#endregion		

		for(int i = 0;i < platforms.Count; i++){
			if(platforms[i].position.z < transform.position.z - _bounds.extents.z){
				Destroy(platforms[i].gameObject);
				platforms.RemoveAt(i);
				Transform platform = (Transform)Instantiate(Platform[Random.Range(0,Platform.Count)]);
				
				Bounds prevbounds = platforms[platforms.Count - 1].FindChild("Surface").GetComponent<MeshRenderer>().bounds;
				Bounds bounds = platform.FindChild("Surface").GetComponent<MeshRenderer>().bounds;
				
				platform.transform.position = platforms[platforms.Count - 1].position + new Vector3(0,0,bounds.extents.z + prevbounds.extents.z); 
				
				_bounds = bounds;
				
				platforms.Add(platform);
				
				break;
				//platforms[i].position = lastPlatform.position + new Vector3(0,0,_bounds.extents.z * 2);
				//lastPlatform = platforms[i];
			}
		}



	}

	void OnCollisionEnter(Collision other)
	{

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