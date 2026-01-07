using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeScript : MonoBehaviour
{
    public Slider bgmSlider;
    public TMP_Text soundVolume;

    void Start()
    {
        // 시작할 때 슬라이더 값을 현재 BGM 볼륨에 맞춤 (기본값 1)
        if (BgmManager.instance != null)
        {
            float currentActualVol = BgmManager.instance.GetComponent<AudioSource>().volume;
            bgmSlider.value = currentActualVol / 0.7f;
            UpdateText(bgmSlider.value);
        }

        // 슬라이더 값이 변경될 때마다 함수 실행 등록
        bgmSlider.onValueChanged.AddListener(delegate { ValueUpdate(); });
    }

    public void ValueUpdate()
    {
        if (BgmManager.instance != null)
        {
            float volume = bgmSlider.value;
            BgmManager.instance.SetVolume(volume);
            UpdateText(volume);
        }
    }

    private void UpdateText(float volume)
    {
        if (soundVolume != null)
        {
            int percent = Mathf.RoundToInt(volume * 100);
            soundVolume.text = percent.ToString() + "%";
        }
    }
}
