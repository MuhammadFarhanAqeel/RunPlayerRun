using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {

	public float timeRemaining =10; // pre-earned time
	float timeExtension = 2f; // time to extend by collecting powerup
	float timeDeduction = 5f; // time to reduce
	float totalTimeElapsed = 0;
	public float score =0f;
	public bool isGameOver = false;
	public GUISkin skin;






	// Use this for initialization
	void Start () {
		Time.timeScale = 1;


	}
	
	// Update is called once per frame
	void Update () {
				if (isGameOver)
						return;

				totalTimeElapsed += Time.deltaTime;
				score = totalTimeElapsed * 10; // calculate the score based on totalTimeElapsed
				timeRemaining -= Time.deltaTime; // decreament of time by 1 second on every second
				if (timeRemaining < 0) {
						isGameOver = true;		
						
				}
				
	}
	void OnGUI()
	{
		GUI.skin = skin;
		//check if game is not over, if so, display the score and the time left
		if(!isGameOver)    
		{
			GUI.Label(new Rect(10, 10, Screen.width/5, Screen.height/6),"Time: "+((int)timeRemaining).ToString());
			GUI.Label(new Rect(Screen.width-(Screen.width/6), 10, Screen.width/6, Screen.height/6), "Score: "+((int)score).ToString());
		}
		//if game over, display game over menu with score
		else
		{
			Time.timeScale = 0; //set the timescale to zero so as to stop the game world
			
			//display the final score
			GUI.Box(new Rect(Screen.width/4, Screen.height/4 , Screen.width/2, Screen.height/2), "Game Over\n Your Score :" +(int)score);
			//restart the game on click
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+Screen.height/10+10, Screen.width/2-20, Screen.height/10), "Restart")){
				Application.LoadLevel(Application.loadedLevel);
			}
			
			//load the main menu, which as of now has not been created
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+2*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "Main menu")){
				Application.LoadLevel(0);
			}
			
			//exit the game
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+3*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "Exit")){
				Application.Quit();
			}
		}
	}


	public  void PowerUpCollected()
	{
		timeRemaining += timeExtension;
	}

	public  void AlchoholCollected()
	{
		timeRemaining -= timeDeduction;
	}
}
