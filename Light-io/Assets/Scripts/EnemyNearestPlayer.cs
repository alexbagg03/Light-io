using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearestPlayer : MonoBehaviour {
    private GameObject target;
    private float attackSpeed;
    private int foundPlayerIndex;

    void Start()
    {
        attackSpeed = 0;
        target = FindNearestPlayer();
    }

    void Update()
    {
        if (GameManager.Instance.gamePaused)
        {
            return;
        }
        if (attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }

        target = FindNearestPlayer();
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && attackSpeed <= 0)
        {
            other.GetComponent<PlayerController>().DecreaseLight();
            attackSpeed = 0.5f;
        }
    }

    public GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            return null;
        }

        GameObject closest = players[0];
        float closestDistance = Vector3.Distance(transform.position, closest.transform.position);

        if (closest.GetComponent<PlayerController>().light <= 0)
        {
            closest.tag = "Dead";
        }


        for (int i = 0; i < players.Length; i++)
        {
            float testDistance = Vector3.Distance(transform.position, players[i].transform.position);
            if(testDistance < closestDistance)
            {
                closest = players[i];
                closestDistance = testDistance;
            }
        }
        return closest;
    }
}
