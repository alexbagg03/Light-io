using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    private void Update()
    {
        if ((Input.GetButtonDown("EnterP1") || Input.GetButtonDown("EnterP2")) && SceneManager.GetActiveScene().name == "TitleScreen")
        {
            SceneManager.LoadScene("Level1");
        }

        if((Input.GetButtonDown("EnterP1") || Input.GetButtonDown("EnterP2")) && SceneManager.GetActiveScene().name == "WinScene")
        {
            SceneManager.LoadScene("Level1");
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

}
