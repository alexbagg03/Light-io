using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			other.GetComponent<PlayerController> ().DecreaseLight (); 
			other.GetComponent<PlayerController> ().DecreaseLight (); 
			other.GetComponent<PlayerController> ().DecreaseLight (); 
		}
	}
		
}


