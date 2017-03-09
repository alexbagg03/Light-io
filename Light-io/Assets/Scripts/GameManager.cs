using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    ///////////////////////////////////////////////
    /// MEMBERS
    ///////////////////////////////////////////////
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public bool gameOver = false;
    public bool gamePaused = false;
    public float maxLight = 100;
    public float totalLightGained;
    public GameObject player1;
    public GameObject player2;
    public GameObject scorebarFill;
    public GameObject winStateUI;
    public GameObject startStateUI;
    public GameObject loseStateUI;

    ///////////////////////////////////////////////
    /// MONOBEHAVIOR METHODS
    ///////////////////////////////////////////////
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        float p1LightAmount = player1.GetComponent<PlayerController>().light;
        float p2LightAmount = player2.GetComponent<PlayerController>().light;
        AddLight(p1LightAmount + p2LightAmount);
    }
    void Update ()
    {
        if (totalLightGained <= 0)
        {
            LoseState();
        }
	}

    ///////////////////////////////////////////////
    /// PUBLIC METHODS
    ///////////////////////////////////////////////
    public void WinState()
    {
        GameOver();
        winStateUI.SetActive(true);
    }
    public void LoseState()
    {
        GameOver();
        loseStateUI.SetActive(true);
    }
    public void GameOver()
    {
        gameOver = true;
        PauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }
    public void StartGame()
    {
        startStateUI.SetActive(false);
        ContinueGame();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void AddLight(float light)
    {
        totalLightGained += light;
    }

}
