using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //テキストUIをドラッグ&ドロップ
    [SerializeField] TextMeshProUGUI _text;

    //DateTimeを使うため変数を設定
    DateTime TodayNow;

    public bool flag;
    public string content;

    void Update()
    {
        if (flag)
        {
            _text.text = content;
            flag = false;
        }
        //時間を取得
        TodayNow = DateTime.Now;

        //テキストUIに年・月・日・秒を表示させる
        _text.text = TodayNow.Year.ToString() + "年 " + TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日" + DateTime.Now.ToLongTimeString();
    }

}
