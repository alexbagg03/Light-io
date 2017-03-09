using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InstructionsPanelController : MonoBehaviour {

    public GameObject pressReadyP1;
    public GameObject pressReadyP2;
    public GameObject readyP1;
    public GameObject readyP2;

    private bool p1Ready;
    private bool p2Ready;
    private bool gameStarted;

    void Start ()
    {
        GameManager.Instance.PauseGame();
	}
	void Update ()
    {
        if (CrossPlatformInputManager.GetButtonDown("EnterP1") && !p1Ready)
        {
            pressReadyP1.SetActive(false);
            readyP1.SetActive(true);
            p1Ready = true;
        }
        if (CrossPlatformInputManager.GetButtonDown("EnterP2") && !p2Ready)
        {
            pressReadyP2.SetActive(false);
            readyP2.SetActive(true);
            p2Ready = true;
        }

        if (p1Ready && p2Ready)
        {
            if (!gameStarted)
            {
                // Time scale has to be set back to 1 for Invoke to work
                Time.timeScale = 1;
                // Start game after half a second
                Invoke("StartGame", 0.5f);
                gameStarted = true;
            }
        }
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
    }

}
