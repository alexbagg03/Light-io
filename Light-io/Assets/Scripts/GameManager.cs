using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameObject scorebarFill;

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
    void Update ()
    {
		// Nothing yet
	}

    ///////////////////////////////////////////////
    /// PUBLIC METHODS
    ///////////////////////////////////////////////
    public void GameOver()
    {
        gameOver = true;
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
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void AddLight(float light)
    {
        scorebarFill.GetComponent<ScorebarController>().AddLight(light);
    }

}
