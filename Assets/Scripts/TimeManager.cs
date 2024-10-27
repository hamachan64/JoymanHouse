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

    void Update()
    {
        //���Ԃ��擾
        TodayNow = DateTime.Now;

        //�e�L�X�gUI�ɔN�E���E���E�b��\��������
        _text.text = TodayNow.Year.ToString() + "�N " + TodayNow.Month.ToString() + "��" + TodayNow.Day.ToString() + "��" + DateTime.Now.ToLongTimeString();
    }

}
