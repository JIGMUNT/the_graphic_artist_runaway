using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeScript : MonoBehaviour
{
    private TextMeshProUGUI valueText;

    private void Start()
    {
        valueText = GetComponent<TextMeshProUGUI>();
    }

    public void valueUpdate(float value)
    {
        valueText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
