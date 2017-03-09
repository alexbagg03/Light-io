using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private GameObject target;

	void Start()
	{
		target = FindNearestPlayer ();
	}

	void Update()
	{
        if (GameManager.Instance.gamePaused)
        {
            return;
        }

		target = FindNearestPlayer ();
		if (target != null) 
		{
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, 0.1f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
            other.GetComponent<PlayerController>().DecreaseLight();
		}
	}

	public GameObject FindNearestPlayer()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length == 0) 
		{
			return null;
		}
		GameObject closest = players [0];
		float closestDistance = Vector3.Distance (transform.position, closest.transform.position);
		for (int i = 0; i < players.Length; i++) 
		{
			if (Vector3.Distance (transform.position, players [i].transform.position) < closestDistance) 
			{
				closest = players [i];
				closestDistance = Vector3.Distance (transform.position, closest.transform.position);
			}
		}
		return closest;
	}
}
