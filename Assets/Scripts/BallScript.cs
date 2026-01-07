using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    const float C_Radian = 180f;
    public Rigidbody2D rb;
    public float speed;

    public GameObject[] itemPrefabs;

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

            // GameManager를 찾아 GameStart 함수 실행
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.GameStart();
            }
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
            FindObjectOfType<GameManager>().PlaySFX(FindObjectOfType<GameManager>().blockBreakClip);
            if (gm != null)
                gm.AddScore(100);

            collision.collider.enabled = false;

            if (Random.value <= 0.3f) 
            {
                int randomIndex = Random.Range(0, itemPrefabs.Length);
                Instantiate(itemPrefabs[randomIndex], collision.transform.position, Quaternion.identity);
            }

            //collision.gameObject.SetActive(false);
            // 오브젝트를 삭제 (0.1초 뒤에 삭제하여 물리 연산 꼬임 방지)
            Destroy(collision.gameObject, 0.1f);
        }
        else if(collision.collider.CompareTag("BallOut"))
        {
            isOnSpaceKey = false;
            FindObjectOfType<GameManager>().GameOverUI();
            
        }
    }
}
