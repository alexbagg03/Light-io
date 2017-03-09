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
        speed = 3f;
	}

    private void Update()
    {
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

    public void Move()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.05f);
        newPosition.z = 0;
        transform.position = newPosition;
    }
    public void Move2()
    {
        Vector3 velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        velocity.z = 0;
        m_Rigidbody.velocity = velocity * 2;
    }

    public void Player1Control()
    {
        m_Vertical = Input.GetAxis("Vertical");
        m_Horizontal = Input.GetAxis("Horizontal");
    }

    public void Player2Control()
    {
        m_Vertical = Input.GetAxis("VerticalP2");
        m_Horizontal = Input.GetAxis("HorizontalP2");
    }

}
