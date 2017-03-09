using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D m_Rigidbody;
	public float light;
    public int playerNumber;
    private float m_Horizontal;
    private float m_Vertical;
    private float m_Angle;
    private Vector2 force;
    public float speed;
    public bool givelight;
    public GameObject particleObject;
    public GameObject player1;
    public GameObject player2;
    public float lightrate;
    private bool decrease;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        if (name == "Player1")
        {
            GameObject.Find("Player1Camera").GetComponent<CameraFollow>().player = this.gameObject;
        }
        else if (name == "Player2")
        {
            GameObject.Find("Player2Camera").GetComponent<CameraFollow>().player = this.gameObject;
        }
    }

	private void Start()
	{
		light = 5f;
        speed = 4f;
        givelight = false;
        particleObject.SetActive(false);
        lightrate = 0.01f;
        decrease = false;
	}

    private void Update()
    {
        if (GameManager.Instance.gamePaused)
        {
            return;
        }

        switch (playerNumber)
        {
            case 1:
                Player1Control();
                break;
            case 2:
                Player2Control();
                break;
        }

        if (m_Vertical == 0 && m_Horizontal == 0)
        {
            m_Rigidbody.velocity = new Vector2(0, 0);
        }

        else
        {
            m_Angle = Mathf.Atan2(m_Vertical, m_Horizontal);
            transform.eulerAngles = new Vector3(0, 0, m_Angle * Mathf.Rad2Deg);
            force.x = Mathf.Cos(m_Angle);
            force.y = Mathf.Sin(m_Angle);
            force.x = force.x * speed;
            force.y = force.y * speed;
            m_Rigidbody.velocity = new Vector2(force.x, force.y) * speed;
        }

    }

    public void Player1Control()
    {
        m_Vertical = Input.GetAxis("Vertical");
        m_Horizontal = Input.GetAxis("Horizontal");

        if(Input.GetAxisRaw("Fire1") == 1 && Input.GetAxisRaw("Fire2") == 1 && givelight)
        {
            particleObject.SetActive(true);
            particleObject.transform.position = this.transform.position;
            Vector3 targetDir = player2.transform.position - particleObject.transform.position;
            float step = 6f;
            Vector3 newDir = Vector3.RotateTowards(particleObject.transform.forward, targetDir, step, 0.0F);
            particleObject.transform.rotation = Quaternion.LookRotation(newDir);

            if (light >= 0f && !decrease)
            {
                InvokeRepeating("TransferLightP1", 0f, 0.5f);
                decrease = true;
            }
        }

        if((Input.GetAxisRaw("Fire1") != 1 && Input.GetAxisRaw("Fire2") != 1) || !givelight || light <= 0)
        {
            particleObject.SetActive(false);
            CancelInvoke("TransferLightP1");
            if (light < 0)
            {
                light = 0;
            }

            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(0.1f, 0.1f);
            }
            decrease = false;
        }

    }

    public void Player2Control()
    {
        m_Vertical = Input.GetAxis("VerticalP2");
        m_Horizontal = Input.GetAxis("HorizontalP2");

        if (Input.GetAxisRaw("Fire1P2") == 1 && Input.GetAxisRaw("Fire2P2") == 1 && givelight)
        {
            particleObject.SetActive(true);
            particleObject.transform.position = this.transform.position;
            Vector3 targetDir = player1.transform.position - particleObject.transform.position;
            float step = 6f;
            Vector3 newDir = Vector3.RotateTowards(particleObject.transform.forward, targetDir, step, 0.0F);
            particleObject.transform.rotation = Quaternion.LookRotation(newDir);

            if (light >= 0f && !decrease)
            {
                InvokeRepeating("TransferLightP2", 0f, 0.5f);
                decrease = true;
            }
        }

        if ((Input.GetAxisRaw("Fire1P2") != 1 && Input.GetAxisRaw("Fire2P2") != 1) || !givelight || light <= 0)
        {
            particleObject.SetActive(false);
            CancelInvoke("TransferLightP2");

            if (light < 0)
            {
                light = 0;
            }

            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(0.1f, 0.1f); //so the player doesn't get a negative scale
            }
            decrease = false;
        }
    }

    public void TransferLightP1()
    {
        // these values are dependent to how much light and size is increased when a player picks up an orb. Right now size and light is 1 to 1
        light -= 0.5f;
        this.GetComponent<Light>().range -= 0.025f;
        player2.GetComponent<Light>().range += 0.025f;
        player2.GetComponent<PlayerController>().speed -= 0.025f;
        player2.GetComponent<PlayerController>().light += 0.5f;
        player2.transform.localScale = new Vector2(player2.transform.localScale.x + 0.025f, player2.transform.localScale.y + 0.025f);
        transform.localScale = new Vector2(transform.localScale.x - 0.025f, transform.localScale.y - 0.025f);
        speed += 0.0125f;
    }

    public void TransferLightP2()
    {
        // these values are dependent to how much light and size is increased when a player picks up an orb. Right now size and light is 1 to 1
        light -= 0.5f;
        this.GetComponent<Light>().range -= 0.025f;
        player1.GetComponent<Light>().range += 0.025f;
        player1.GetComponent<PlayerController>().speed -= 0.025f;
        player1.GetComponent<PlayerController>().light += 0.5f;
        player1.transform.localScale = new Vector2(player1.transform.localScale.x + 0.025f, player1.transform.localScale.y + 0.025f);
        transform.localScale = new Vector2(transform.localScale.x - 0.025f, transform.localScale.y - 0.025f);
        speed += 0.0125f;
    }

    public void DecreaseLight()
    {
        light -= 5f;
        GameManager.Instance.totalLightGained -= 5f;
        GetComponent<Light>().range -= 0.025f;
        transform.localScale = new Vector2(transform.localScale.x - 0.025f, transform.localScale.y - 0.025f);
        speed += 0.0125f;
    }

}
