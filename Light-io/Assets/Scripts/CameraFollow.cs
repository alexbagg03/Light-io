using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    private void Update()
    {
        if(player != null)
        {
            Vector3 newPosition = player.transform.position;
            newPosition.z = -10;
            transform.position = newPosition;
        }
    }
}
