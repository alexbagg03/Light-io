using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D m_Rigidbody;
	public float light;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().player = this.gameObject;
    }

	private void Start()
	{
		light = 5f;
	}

    private void Update()
    {
        //Move();
        Move2();
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

}
