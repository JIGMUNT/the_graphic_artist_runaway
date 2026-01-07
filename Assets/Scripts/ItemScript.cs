using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    BallSizeUp,
    PlayerWidthUp, 
    PlayerWidthDown
}

public class ItemScript : MonoBehaviour
{
    public ItemType type;
    public float fallSpeed = 3f;

    private void Update()
    {
        // 매 프레임 아래로 이동
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // 화면 밖으로 나가면 삭제
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했는지 확인
        if(collision.CompareTag("Player"))
        {
            // 사운드 재생 코드 추가
            // FindObjectOfType<GameManager>().PlaySFX(FindObjectOfType<GameManager>().itemEatClip)
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null) gm.PlaySFX(gm.itemEatClip); // 아이템 먹는 소리


            // 플레이어 스크립트의 효과 적용 함수 호출
            PlayerScripts player = collision.GetComponent<PlayerScripts>();
            if (player != null) 
            {
                player.ApplyItemEffect(type);
            }

            // 아이템 먹으면 삭제
            Destroy(gameObject);
        }
    }
}
