using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {


	public GUISkin mySkin; // custom guiskin reference
	public string levelToLoad;
	public bool paused = false; //  to check if the game is paused or not
	private GameControlScript control;
	public GameObject playerSoundRun;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		control = GetComponent<GameControlScript> ();

	}
	
	// Update is called once per frame
	void Update () {
				if (!control.isGameOver) {
						if (Input.GetKeyDown (KeyCode.Escape)) {
								if (paused) {
										paused = false;

								} else {
										paused = true;
								}

								if (paused) {
									//	Time.timeScale = 0; // setting it to 0 so that all process are halted.
										playerSoundRun.GetComponent<AudioSource> ().GetComponent<AudioSource>().Stop ();
								} else {
										Time.timeScale = 1; // unpausing and setting it to 1 so that the process are resumed
										playerSoundRun.GetComponent<AudioSource> ().GetComponent<AudioSource>().Play ();
				
								}
						}
				}					
		}
	private void OnGUI()
	{
				GUI.skin = mySkin;

				if (paused) {

						GUI.Box (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "PAUSED!!!");
						if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Resume")) {
								paused = false;
						}
						if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + 2 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Restart")) {
								Application.LoadLevel (Application.loadedLevel);
						}
						if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + 3 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Main Menu")) {
								Application.LoadLevel (levelToLoad);
						}

						Time.timeScale = 0;
				}
		}
					}