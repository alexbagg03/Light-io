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
    private bool boosting;
    private float enemyAttackDecreaseLightAmount = 5f;
    private float rangeChangeRate = 0.025f;
    private float speedChangeRate = 0.025f;
    private float lightChangeRate = 0.5f;
    private float localScaleChangeRate = 0.025f;
    private float trailChangeRate = 0.025f;
    public GameObject trail;
    private Color current_color;
    private Color original_color;
    public float boostForce = 100f;
    public GameObject bank;
    public GameObject current_target;

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
        trail = transform.FindChild("Trail").gameObject;
        original_color = GetComponent<Light>().color;
	}

    private void Update()
    {
        if (GameManager.Instance.gamePaused)
        {
            return;
        }

        if (light > 0)
        {
            gameObject.tag = "Player";

            if (!trail.activeSelf)
            {
                trail.SetActive(true);
            }

            GetComponent<Light>().color = original_color;
        }

        if (light <= 0)
        {
            gameObject.tag = "Dead";
        }

        if (gameObject.tag == "Dead")
        {
            m_Rigidbody.velocity = new Vector2(0, 0);

            GetComponent<Light>().color = new Color(19f / 255f, 19f / 255f, 19f / 255f, 122f / 255f);
            if (trail.activeSelf)
            {
                trail.SetActive(false);
            }
            

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

        // Player 1 light transfer input
        if(Input.GetAxisRaw("Fire1") == 1 && Input.GetAxisRaw("Fire2") == 1 && givelight)
        {
            particleObject.SetActive(true);
            particleObject.transform.position = this.transform.position;
            Vector3 targetDir = current_target.transform.position - particleObject.transform.position;
            float step = 6f;
            Vector3 newDir = Vector3.RotateTowards(particleObject.transform.forward, targetDir, step, 0.0F);
            particleObject.transform.rotation = Quaternion.LookRotation(newDir);

            if (light >= 0f && !decrease)
            {
                if (current_target.tag == "Player" || current_target.tag == "Dead")
                {
                    InvokeRepeating("TransferLightP1", 0f, 0.25f);
                }

                if (current_target.tag == "Bank")
                {
                    InvokeRepeating("TransferBank", 0f, 0.25f);
                }

                decrease = true;

            }
        }

        // Player 1 light transfer input
        if ((Input.GetAxisRaw("Fire1") != 1 && Input.GetAxisRaw("Fire2") != 1) || !givelight || light <= 5)
        {
            particleObject.SetActive(false);
            CancelInvoke("TransferLightP1");
            CancelInvoke("TransferBank");

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

        // Player 1 boost input
        if (Input.GetButtonDown("BoostR1") && !boosting && light > 5f)
        {
            m_Angle = Mathf.Atan2(m_Vertical, m_Horizontal);
            transform.eulerAngles = new Vector3(0, 0, m_Angle * Mathf.Rad2Deg);
            force.x = Mathf.Cos(m_Angle);
            force.y = Mathf.Sin(m_Angle);
            force.x = force.x * boostForce;
            force.y = force.y * boostForce;
            GetComponent<Rigidbody2D>().AddForce(force);
            light -= 5f;
            GetComponent<Light>().range -= 0.25f;
            transform.localScale = new Vector2(transform.localScale.x - 0.25f, transform.localScale.y - 0.25f); //this amount might need to be tweaked 
            speed += 0.25f;
            trail.GetComponent<ParticleSystem>().startSize -= 0.25f;
        }



    }

    public void Player2Control()
    {
        m_Vertical = Input.GetAxis("VerticalP2");
        m_Horizontal = Input.GetAxis("HorizontalP2");

        // Player 2 light transfer input
        if (Input.GetAxisRaw("Fire1P2") == 1 && Input.GetAxisRaw("Fire2P2") == 1 && givelight)
        {
            particleObject.SetActive(true);
            particleObject.transform.position = this.transform.position;
            Vector3 targetDir = current_target.transform.position - particleObject.transform.position;
            float step = 6f;
            Vector3 newDir = Vector3.RotateTowards(particleObject.transform.forward, targetDir, step, 0.0F);
            particleObject.transform.rotation = Quaternion.LookRotation(newDir);

            if (light >= 0f && !decrease)
            {
                if (current_target.tag == "Player" || current_target.tag == "Dead")
                {
                    InvokeRepeating("TransferLightP2", 0f, 0.25f);
                }

                if (current_target.tag == "Bank")
                {
                    InvokeRepeating("TransferBank", 0f, 0.25f);
                }

                decrease = true;

            }
        }

        // Player 2 light transfer input
        if ((Input.GetAxisRaw("Fire1P2") != 1 && Input.GetAxisRaw("Fire2P2") != 1) || !givelight || light <= 5)
        {
            particleObject.SetActive(false);
            CancelInvoke("TransferLightP2");
            CancelInvoke("TransferBank");


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

        if (Input.GetButtonDown("BoostR2") && !boosting && light > 5f)
        {
            m_Angle = Mathf.Atan2(m_Vertical, m_Horizontal);
            transform.eulerAngles = new Vector3(0, 0, m_Angle * Mathf.Rad2Deg);
            force.x = Mathf.Cos(m_Angle);
            force.y = Mathf.Sin(m_Angle);
            force.x = force.x * boostForce;
            force.y = force.y * boostForce;
            GetComponent<Rigidbody2D>().AddForce(force);
            light -= 5f;
            GetComponent<Light>().range -= 0.25f;
            transform.localScale = new Vector2(transform.localScale.x - 0.25f, transform.localScale.y - 0.25f); //this amount might need to be tweaked 
            speed += 0.25f;
            trail.GetComponent<ParticleSystem>().startSize -= 0.25f;
        }
    }

    public void TransferLightP1()
    {
        // these values are dependent to how much light and size is increased when a player picks up an orb. Right now size and light is 1 to 1
        // Change values from self
        light -= lightChangeRate;
        this.GetComponent<Light>().range -= rangeChangeRate;
        GetTrail(this.gameObject).GetComponent<ParticleSystem>().startSize -= trailChangeRate;
        transform.localScale = new Vector2(transform.localScale.x - localScaleChangeRate, transform.localScale.y - localScaleChangeRate);
        speed += speedChangeRate;

        // Change values for other player
        player2.GetComponent<Light>().range += rangeChangeRate;
        GetTrail(player2).GetComponent<ParticleSystem>().startSize += trailChangeRate;
        player2.GetComponent<PlayerController>().speed -= speedChangeRate;
        player2.GetComponent<PlayerController>().light += lightChangeRate;
        player2.transform.localScale = new Vector2(player2.transform.localScale.x + localScaleChangeRate, player2.transform.localScale.y + localScaleChangeRate);
    }

    public void TransferLightP2()
    {
        // these values are dependent to how much light and size is increased when a player picks up an orb. Right now size and light is 1 to 1
        // Change values from self
        light -= lightChangeRate;
        this.GetComponent<Light>().range -= rangeChangeRate;
        GetTrail(this.gameObject).GetComponent<ParticleSystem>().startSize -= trailChangeRate;
        transform.localScale = new Vector2(transform.localScale.x - localScaleChangeRate, transform.localScale.y - localScaleChangeRate);
        speed += speedChangeRate;

        // Change values for other player
        player1.GetComponent<Light>().range += rangeChangeRate;
        GetTrail(player1).GetComponent<ParticleSystem>().startSize += trailChangeRate;
        player1.GetComponent<PlayerController>().speed -= speedChangeRate;
        player1.GetComponent<PlayerController>().light += lightChangeRate;
        player1.transform.localScale = new Vector2(player1.transform.localScale.x + localScaleChangeRate, player1.transform.localScale.y + localScaleChangeRate);
    }

    public void TransferBank()
    {
        light -= lightChangeRate;
        this.GetComponent<Light>().range -= rangeChangeRate;
        GetTrail(this.gameObject).GetComponent<ParticleSystem>().startSize -= trailChangeRate;
        transform.localScale = new Vector2(transform.localScale.x - localScaleChangeRate, transform.localScale.y - localScaleChangeRate);
        speed += speedChangeRate;

        // Change values for other player
        bank.GetComponent<Bank>().light += lightChangeRate;
        bank.transform.localScale = new Vector2(bank.transform.localScale.x + localScaleChangeRate, bank.transform.localScale.y + localScaleChangeRate);
    }


    public void DecreaseLight()
    {
        if (light <= 0)
        {
            return;
        }

        light -= enemyAttackDecreaseLightAmount;
        GameManager.Instance.totalLightGained -= enemyAttackDecreaseLightAmount;
        this.GetComponent<Light>().range -= 0.25f;
        GetTrail(this.gameObject).GetComponent<ParticleSystem>().startSize -= 0.25f;
        transform.localScale = new Vector2(transform.localScale.x - 0.25f, transform.localScale.y - 0.25f);
        speed += 0.25f;
    }

    public GameObject GetTrail(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).name == "Trail")
            {
                return parent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

}
