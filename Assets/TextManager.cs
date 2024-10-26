using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    public bool flag;
    public string content;

    void Update()
    {
        if (flag)
        {
            Debug.Log(content);
            this.GetComponent<TextMeshProUGUI>().text = content;
            flag = false;
        }

    }
}
