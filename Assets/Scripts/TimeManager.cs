using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //�e�L�X�gUI���h���b�O&�h���b�v
    [SerializeField] TextMeshProUGUI _text;

    //DateTime���g�����ߕϐ���ݒ�
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
        //���Ԃ��擾
        TodayNow = DateTime.Now;

        //�e�L�X�gUI�ɔN�E���E���E�b��\��������
        _text.text = TodayNow.Year.ToString() + "�N " + TodayNow.Month.ToString() + "��" + TodayNow.Day.ToString() + "��" + DateTime.Now.ToLongTimeString();
    }

}
