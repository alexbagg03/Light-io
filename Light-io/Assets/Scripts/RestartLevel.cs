using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour {

    private void Update()
    {
        if(enabled && (Input.GetButtonDown("EnterP1") || Input.GetButtonDown("EnterP2")))
        {
            GameManager.Instance.RestartGame();
        }
    }


}
