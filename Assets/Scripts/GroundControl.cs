using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {


	public float speed = 0.25f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * speed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0, -offset);
	}
}
