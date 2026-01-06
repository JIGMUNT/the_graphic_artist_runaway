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

    private bool isGameOver;
    private int currentScore = 0;   // 점수 저장용 변수

    private void Start()
    {
        UpdateScoreUI();    // 시작할 때 점수판 초기화
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (Score != null)
            Score.text = currentScore.ToString("D5");
    }

    public void GameOverUI()
    {
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
