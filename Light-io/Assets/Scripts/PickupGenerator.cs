using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour {

    public GameObject pickup;
    public int numberOfPickups = 20;
    public float mapLength = 100f;

	void Start ()
    {
        for (int i = 0; i < numberOfPickups; i++)
        {
            Vector2 position = new Vector2(Random.Range(-mapLength, mapLength), Random.Range(-mapLength, mapLength));
            GameObject.Instantiate(pickup, position, Quaternion.identity);
        }
	}

}
