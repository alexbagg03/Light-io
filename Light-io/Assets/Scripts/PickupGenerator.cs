using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour {

    public GameObject pickup;
    public int pickupsOnStart = 20;
    public float mapWidth = 100f;

	void Start ()
    {
        for (int i = 0; i < pickupsOnStart; i++)
        {
            SpawnPickup();
        }

	}

    public void SpawnPickup()
    {
        Vector2 randPosition = new Vector2(Random.Range(-mapWidth / 2, mapWidth / 2), Random.Range(-mapWidth / 2, mapWidth / 2));
        GameObject.Instantiate(pickup, randPosition, Quaternion.identity);
    }



}
