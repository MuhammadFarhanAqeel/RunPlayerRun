﻿using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GUISkin mySkin;
	public string gameLevel;
	public string optionsLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI()
	{
				GUI.skin = mySkin;
				GUI.Box (new Rect (Screen.width / 4 + 10, Screen.height / 4, Screen.width / 2, Screen.height / 2), "MAIN MENU");

				if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Play")) {
						Application.LoadLevel (gameLevel);
				}

				if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + 2*Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Credits")) {
						Application.LoadLevel (optionsLevel);
				}

				if (GUI.Button (new Rect (Screen.width / 4 + 10, Screen.height / 4 + 3*Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "Exit")) {
						Application.Quit ();
				}

		}
}
