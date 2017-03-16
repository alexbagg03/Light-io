using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveLight : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Dead" || other.tag == "Bank")
        {
            transform.parent.GetComponent<PlayerController>().givelight = true;
            transform.parent.GetComponent<PlayerController>().current_target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Dead" || other.tag == "Bank")
        {
            transform.parent.GetComponent<PlayerController>().givelight = false;
            transform.parent.GetComponent<PlayerController>().current_target = null;
        }
    }
}
