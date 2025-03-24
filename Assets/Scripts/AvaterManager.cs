using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class AvaterManager : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;

    [SerializeField] AudioClip _audioClip;

    public string avaterRequest;

    string serverIp = "127.0.0.1"; // �T�[�o�[��IP�A�h���X
    int port = 12345; // �T�[�o�[�̃|�[�g�ԍ�

    //���s������file���w��
    [SerializeField] string pythonFilePath = "path/to/your_script.py";

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        if(avaterRequest == null)
        {
            avaterRequest = "Zundamon";
        }
    }

    // ChatGPT���特���e�L�X�g����M
    public void GetAudioText(AudioClip auido, string name)
    {
        _audioClip = auido;
        PlayAnimation(name);
    }

    // ���N�G�X�g���󂯎������, �����ƃA�j���[�V�����̍Đ�
    public void PlayAnimation(string filename)
    {
        _audioSource.PlayOneShot(_audioClip);


        if (filename == "bando.wav")
        {
            _animator.SetBool("Nanana", true);
            StartCoroutine(WaitSpecialAudioEnd("Nanana"));
        }else if (filename == "suimamen.wav")
        {
            _animator.SetBool("Suimamen", true);
            StartCoroutine(WaitSpecialAudioEnd("Suimamen"));
        }
        else if (filename == "angry.wav")
        {
            _animator.SetBool("Angry", true);
            StartCoroutine(WaitSpecialAudioEnd("Angry"));
        }
        else if (filename == "Happy.wav")
        {
            _animator.SetBool("Happy", true);
            StartCoroutine(WaitSpecialAudioEnd("Happy"));
        }
        else
        {
            StartCoroutine(WaitNormalAudioEnd());
        }
    }

    private IEnumerator WaitSpecialAudioEnd(string pram)
    {
        // �������Đ��I������܂őҋ@
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        // �������I�������炱�̊֐����Ă΂��
        _animator.SetBool(pram, false);

        RunPythonScript("Hello, Server!");
    }

    private IEnumerator WaitNormalAudioEnd()
    {
        // �������Đ��I������܂őҋ@
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        RunPythonScript("Hello, Server!");
    }

    public void RunPythonScript(string sendMessage)
    {
        try
        {
            // �\�P�b�g�̍쐬
            TcpClient client = new TcpClient(serverIp, port);
            Console.WriteLine("�T�[�o�[�ɐڑ����܂���");

            // �l�b�g���[�N�X�g���[�����擾
            NetworkStream stream = client.GetStream();

            // �T�[�o�[�ɑ��M���郁�b�Z�[�W
            string message = sendMessage;
            byte[] data = Encoding.UTF8.GetBytes(message);

            // ���b�Z�[�W���T�[�o�[�ɑ��M
            stream.Write(data, 0, data.Length);
            Console.WriteLine("�T�[�o�[�Ƀ��b�Z�[�W�𑗐M���܂���: " + message);

            // �T�[�o�[����̉�������M
            byte[] responseData = new byte[256];
            int bytes = stream.Read(responseData, 0, responseData.Length);
            string response = Encoding.UTF8.GetString(responseData, 0, bytes);
            Console.WriteLine("�T�[�o�[����̉���: " + response);
            // �X�g���[���ƃN���C�A���g���N���[�Y
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("�G���[���������܂���: " + e.Message);
        }
    }
}
