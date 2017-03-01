using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;


    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        Vector3 newPosition = player.transform.position; //Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        newPosition.z = -10;
        transform.position = newPosition;
    }
}
