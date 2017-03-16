using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

	private float mapSize = 100f; 
	// Use this for initialization
	void Start () {
		randomPosition (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void randomPosition()
	{
		Vector2 position = new Vector2 (Random.Range (-mapSize, mapSize), Random.Range (-mapSize, mapSize)); 
		GameObject.Instantiate (this, position, Quaternion.identity); 
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			other.GetComponent<PlayerController> ().DecreaseLight (); 
			other.GetComponent<PlayerController> ().DecreaseLight (); 
		}
	}

}


