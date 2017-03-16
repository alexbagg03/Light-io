using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour {

    private void Awake()
    {
        transform.position = RandomPosition();
        GetComponent<ParticleSystem>().startColor = Random.ColorHSV();
    }

    private void Start()
    {
        Destroy(this.gameObject, 10f);
    }


    public Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-8f, 8f), Random.Range(-5f, 5f), 0);
    }


}
