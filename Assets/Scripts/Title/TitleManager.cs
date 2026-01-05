using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SceneList
{
    TitleScene,
    BlockCrash
}

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button startBT;
    [SerializeField] private Button ExitBT;
    [SerializeField] private Button settingBT;
    [SerializeField] private GameObject Volume;
    [SerializeField] private Slider slider;

    private bool isOn;

    private void Start()
    {
        startBT.onClick.AddListener(GameStartBT);
        settingBT.onClick.AddListener(SoundSettingBT);
        ExitBT .onClick.AddListener(GameExit);
    }

    private void GameStartBT()
    {
        SceneManager.LoadScene((int)SceneList.BlockCrash);
    }

    private void SoundSettingBT()
    {
        isOn = !isOn;
        Volume.SetActive(isOn);
    }
       

    private void GameExit()
    {
        Application.Quit();
    }
}
