using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject GameOver;
    public Button RestartBT;
    public Button EndBT;
    [SerializeField] private TMP_Text Score;
    [SerializeField] private TMP_Text GOS;

    [SerializeField] private GameObject StartInfo;

    public bool isGameOver;
    private int currentScore = 0;   // 점수 저장용 변수

    [Header("Sound Settings")]
    public AudioSource sfxSource;
    public AudioClip blockBreakClip; // 블록 파괴음
    public AudioClip itemEatClip;   // 아이템 획득음
    public AudioClip paddleHitClip; // 플레이어-공 충돌음

    [Header("Block Spawn Settings")]
    public GameObject blockPrefab;    // 프로젝트 창에 있는 블록 프리팹
    public Transform blockParent;    // 하이어라키의 'Blocks' 오브젝트
    public float lineSpacing = 0.9f;  // 한 줄 내려가는 간격 (블록 높이에 맞춰 조절)
    public int blocksPerLine = 8;     // 한 줄당 블록 개수
    public float startX = -6.9f;      // 맨 왼쪽 블록 X 좌표 (화면에 맞춰 조절)
    public float startY = 2.9f;       // 새 줄이 생길 고정 Y 좌표

    private void Start()
    {
        UpdateScoreUI();    // 시작할 때 점수판 초기화

        // 블록 한 줄 추가 루틴 시작
        StartCoroutine(AddBlockLineRoutine());
    }

    public void GameStart()
    {
        if (StartInfo != null)
        {
            StartInfo.SetActive(false);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (Score != null)
            Score.text = currentScore.ToString("D3");
    }

    IEnumerator AddBlockLineRoutine()
    {
        while (!isGameOver)
        {
            // 6초 대기
            yield return new WaitForSeconds(6f);

            // 1. 기존 모든 블록을 아래로 이동
            // blockParent(Blocks) 안의 모든 자식들을 한 칸씩 내립니다.
            foreach (Transform child in blockParent)
            {
                if (child != null) // 자식이 이미 파괴되었을 수 있으므로 체크
                {
                    child.position += Vector3.down * lineSpacing;
                    if (child.position.y < -3.0f) GameOverUI();
                }
            }

            // 2. 맨 윗줄에 새 블록 한 줄 생성
            for (int i = 0; i < blocksPerLine; i++)
            {
                // 간격 1.5f는 블록 가로 길이에 맞춰 조절하세요
                Vector3 spawnPos = new Vector3(startX + (i * 2f), startY, 0);
                GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity, blockParent);

                // 생성된 블록을 강제로 활성화
                if (newBlock != null)
                {
                    newBlock.SetActive(true);    
                }
            }
        }
    }

    public void GameOverUI()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        Time.timeScale = 0;
        GameOver.SetActive(true);

        if (GOS != null)
            GOS.text = currentScore.ToString();

        RestartBT.onClick.AddListener(Restart);
        EndBT.onClick.AddListener(End);
    }

    private void End()
    {
        Time.timeScale = 1; // 씬 전환 전 시간 흐름 초기화
        SceneManager.LoadScene((int)SceneList.TitleScene);
    }

    private void Restart()
    {
        // 씬 재부팅
        Time.timeScale = 1;
        SceneManager.LoadScene((int)SceneList.BlockCrash);
    }
}
