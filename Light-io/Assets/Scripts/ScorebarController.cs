using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorebarController : MonoBehaviour {

    private Image scorebarFill;
    private float lightGained;
    private GameObject gameManager;

	void Start ()
    {
        scorebarFill = GetComponent<Image>();
        gameManager = GameObject.Find("GameManager");
	}

    private void Update()
    {
        AdjustLight();
    }

    public void AdjustLight()
    {
        lightGained = gameManager.GetComponent<GameManager>().totalLightGained;
        scorebarFill.fillAmount = lightGained / GameManager.Instance.maxLight;

        if (lightGained >= GameManager.Instance.maxLight)
        {
            GameManager.Instance.WinState();
        }
    }

}
