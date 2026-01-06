using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    const float C_Radian = 180f;
    public Rigidbody2D rb;
    public float speed;

    private bool isOnSpaceKey;  // 스페이스 바 확인용

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 게임 시작 전 스페이스 바를 누르면 움직임 활성화
        if (!isOnSpaceKey && Input.GetKeyDown(KeyCode.Space))
        {
            isOnSpaceKey = true;
        }
    }

    private void FixedUpdate()
    {
        if(isOnSpaceKey)
        {
            Vector3 pos = rb.position;
            Vector3 movePos = pos + transform.up * speed * Time.deltaTime;
            rb.MovePosition(movePos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("TopWall"))
        {
            Vector3 tmp = transform.eulerAngles;
            tmp.z = C_Radian - tmp.z;
            transform.eulerAngles = tmp;
        }
        else if(collision.collider.CompareTag("Wall"))
        {
            Vector3 tmp = transform.eulerAngles;
            tmp.z = (C_Radian * 2) - tmp.z;
            transform.eulerAngles = tmp;
        }
        else if(collision.collider.CompareTag("block"))
        {
            Vector3 tmp = transform.eulerAngles;
            int r = Random.Range(0, 2);
            if (r == 0)
                tmp.z = C_Radian - tmp.z;
            else
                tmp.z = (C_Radian * 2) - tmp.z;
            transform.eulerAngles = tmp;

            // GameManager를 찾아 점수 추가
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
                gm.AddScore(100);

            collision.gameObject.SetActive(false);
        }
        else if(collision.collider.CompareTag("BallOut"))
        {
            isOnSpaceKey = false;
            FindObjectOfType<GameManager>().GameOverUI();
            
        }
    }
}
