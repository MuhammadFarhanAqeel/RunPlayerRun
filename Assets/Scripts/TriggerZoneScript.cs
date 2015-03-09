using UnityEngine;
using System.Collections;

public class TriggerZoneScript : MonoBehaviour {

	private void OnCollisionEnter(Collision other){
	
			Destroy (other.gameObject);
	
	}

}
