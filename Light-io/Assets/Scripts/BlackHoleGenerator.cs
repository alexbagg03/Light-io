using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleGenerator : MonoBehaviour {

	public GameObject blackHole;
	public int blackHolesOnStart = 10;
	public float mapWidth = 100f;

	void Start ()
	{
		for (int i = 0; i < blackHolesOnStart; i++)
		{
			spawnBlackHoles();
		}

	}

	public void spawnBlackHoles()
	{
		Vector2 randPosition = new Vector2(Random.Range(-mapWidth, mapWidth), Random.Range(-mapWidth , mapWidth));
		GameObject.Instantiate(blackHole, randPosition, Quaternion.identity);
	}
}
