using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkSpawner : MonoBehaviour {

    private float timer;
    public GameObject firework;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Spawn();
            timer = Random.Range(0.01f, 1.5f);
        }
        timer -= Time.deltaTime;
    }

    public void Spawn()
    {
        GameObject.Instantiate(firework);
    }

}
