using UnityEngine;
using System.Collections;

public class FBholder : MonoBehaviour {
	public GameObject UIFBLoggedIn;
	public GameObject UIFBNotLoggedIn;


	void Awake()
	{
		FB.Init (SetInit, onHideUnity);
	}

	private void SetInit()
	{
				Debug.Log ("Fb Init Done!!");
				if (FB.IsLoggedIn) {
						Debug.Log ("already logged in");
						DealWithFBMenus (true);
				} else {
						DealWithFBMenus (false);
				}
		}

	private void onHideUnity(bool isGameOver)
	{	
		if (!isGameOver) {
						Time.timeScale = 0;		
				} else {
			Time.timeScale = 1;		
		}
	}

	public void FBLogin()
	{
		FB.Login ("email", AuthCallBack);
	}


	void AuthCallBack(FBResult result)
	{
		if (FB.IsLoggedIn) {
						Debug.Log ("FB login worked");	
						DealWithFBMenus (true);
				} else {
						Debug.Log ("problem with login");
						DealWithFBMenus (false);

				}

	}

	void DealWithFBMenus(bool isLoggedIn)
	{
				if (isLoggedIn) {
						UIFBLoggedIn.SetActive (true);
						UIFBNotLoggedIn.SetActive (false);
				} else {
						UIFBLoggedIn.SetActive (false);
						UIFBNotLoggedIn.SetActive (true);		
				}
		}
}
