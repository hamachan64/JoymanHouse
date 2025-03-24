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

    string serverIp = "127.0.0.1"; // サーバーのIPアドレス
    int port = 12345; // サーバーのポート番号

    //実行したいfileを指定
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

    // ChatGPTから音声テキストを受信
    public void GetAudioText(AudioClip auido, string name)
    {
        _audioClip = auido;
        PlayAnimation(name);
    }

    // リクエストを受け取ったら, 音声とアニメーションの再生
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
        // 音源が再生終了するまで待機
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        // 音源が終了したらこの関数が呼ばれる
        _animator.SetBool(pram, false);

        RunPythonScript("Hello, Server!");
    }

    private IEnumerator WaitNormalAudioEnd()
    {
        // 音源が再生終了するまで待機
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        RunPythonScript("Hello, Server!");
    }

    public void RunPythonScript(string sendMessage)
    {
        try
        {
            // ソケットの作成
            TcpClient client = new TcpClient(serverIp, port);
            Console.WriteLine("サーバーに接続しました");

            // ネットワークストリームを取得
            NetworkStream stream = client.GetStream();

            // サーバーに送信するメッセージ
            string message = sendMessage;
            byte[] data = Encoding.UTF8.GetBytes(message);

            // メッセージをサーバーに送信
            stream.Write(data, 0, data.Length);
            Console.WriteLine("サーバーにメッセージを送信しました: " + message);

            // サーバーからの応答を受信
            byte[] responseData = new byte[256];
            int bytes = stream.Read(responseData, 0, responseData.Length);
            string response = Encoding.UTF8.GetString(responseData, 0, bytes);
            Console.WriteLine("サーバーからの応答: " + response);
            // ストリームとクライアントをクローズ
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("エラーが発生しました: " + e.Message);
        }
    }
}
