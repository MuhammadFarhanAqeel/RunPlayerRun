using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject obstacle;
	public GameObject powerup;

	float timeElapsed = 0;
	public float spawnCycle = 0.5f;
	bool spawnPowerUp = true;
	public GameControlScript pControl;
	private float spawnPosition;

	// Use this for initialization
	void Start () {
		pControl = GetComponent<GameControlScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnPosition = Random.Range (1, 9);
		if (spawnPosition <= 3) {
						spawnPosition = -3f;		
				} else if (spawnPosition > 3 && spawnPosition < 7) {
						spawnPosition = 0f;		
				} else if (spawnPosition > 7) {
						spawnPosition = 4f;		
				}
		 

		timeElapsed += Time.deltaTime;
		if (timeElapsed > spawnCycle) {
						GameObject temp;
						if (spawnPowerUp) {
								temp = (GameObject)Instantiate (powerup);
								//Vector3 pos = temp.transform.position;
								temp.transform.position = new Vector3 (spawnPosition, 1*0.7f, 100);
						} else {
								temp = (GameObject)Instantiate (obstacle);
								//Vector3 pos = temp.transform.position;
								temp.transform.position = new Vector3 (spawnPosition, 1*0.7f, 100);
						}

						timeElapsed -= spawnCycle;
						spawnPowerUp = ! spawnPowerUp;

						if (pControl.score > 100) {
								spawnCycle = 0.3f;
						}
						if (pControl.score > 200) {
								spawnCycle = 0.25f;		
						}
						if (pControl.score > 300) {
								spawnCycle = 0.2f;		
						}
						if (pControl.score > 400) {
								spawnCycle = 0.15f;		
						}
						if (pControl.score > 500) {
								spawnCycle = 0.1f;
						}
						if (pControl.score > 600) {
								spawnCycle = 0.08f;
						}
				}		

	}
	/*void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "wall")
		   {
			Destroy(col.gameObject);
			Debug.Log("Done");
		}
	}*/
}
