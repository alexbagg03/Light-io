using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorebarController : MonoBehaviour {

    private Image scorebarFill;
    private float lightGained;

	void Start ()
    {
        scorebarFill = GetComponent<Image>();
	}

    public void AddLight(float light)
    {
        lightGained += light;
        scorebarFill.fillAmount = lightGained / GameManager.Instance.maxLight;
    }

}
