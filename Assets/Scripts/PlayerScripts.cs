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

    public GameObject ballPrefab;

    private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 게임 시작 시의 플레이어 크기를 기억해둔다.
        originalScale = transform.localScale;
    }

    // 아이템 효과 적용 함수
    public void ApplyItemEffect(ItemType type)
    {
        StartCoroutine(ItemRoutine(type));
    }

    IEnumerator ItemRoutine(ItemType type)
    {
        switch (type)
        {
            case ItemType.BallSizeUp:
                // 모든 공의 크기를 키움
                GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                foreach (GameObject b in balls)
                {
                    if (b.GetComponent<BallScript>() != null) 
                    {
                        b.transform.localScale *= 1.5f;
                    }
                }
                    
                yield return new WaitForSeconds(3f);

                foreach (GameObject b in GameObject.FindGameObjectsWithTag("Ball"))
                {
                    if(b.GetComponent<BallScript>() != null)
                    {
                        b.transform.localScale = new Vector3(2.5f, 2.3f, 1f);
                    }
                }
                break;

            case ItemType.PlayerWidthUp:
                // 플레이어 가로 길이 증가 (이미지의 초록색 판)
                transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y, 1);
                yield return new WaitForSeconds(3f);

                transform.localScale = originalScale;
                break;

            case ItemType.PlayerWidthDown:
                // 플레이어 가로 길이 감소
                transform.localScale = new Vector3(transform.localScale.x * 0.5f, transform.localScale.y, 1);
                yield return new WaitForSeconds(3f);

                transform.localScale = originalScale;
                break;
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontal, 0);
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
            FindObjectOfType<GameManager>().PlaySFX(FindObjectOfType<GameManager>().paddleHitClip);
        }
    }
}
