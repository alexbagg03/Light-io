using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour {

    public float lightGain = 5;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Light>().range += 0.25f;
            other.transform.localScale = new Vector2(other.transform.localScale.x + 0.25f, other.transform.localScale.y + 0.25f); 
            other.GetComponent<PlayerController>().light += lightGain; //this amount might need to be tweaked 
            GameManager.Instance.AddLight(lightGain);
            if (other.GetComponent<PlayerController>().speed > 0.25)
            {
                other.GetComponent<PlayerController>().speed -= 0.25f;
            } 
            if (GetTrail(other.gameObject) != null)
            {
                GetTrail(other.gameObject).GetComponent<ParticleSystem>().startSize += 0.25f;
            }
            Destroy(gameObject);
        }
    }


    public GameObject GetTrail(GameObject parent)
    {
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            if(parent.transform.GetChild(i).name == "Trail")
            {
                return parent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

}
