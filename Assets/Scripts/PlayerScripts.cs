using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScripts : MonoBehaviour
{
    public Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float speed = 10f;
    public float[] arrAngles = { -75, -60, -45, -30, -15, 0, 15, 30, 45, 60, 75 };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 pos = rb.position;
        Vector2 movePos = pos + (move * speed * Time.deltaTime);
        rb.MovePosition(movePos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            int r = Random.Range(0, arrAngles.Length);
            Vector3 tmp = collision.transform.eulerAngles;
            tmp.z = arrAngles[r];
            collision.transform.eulerAngles = tmp;
        }
    }
}
