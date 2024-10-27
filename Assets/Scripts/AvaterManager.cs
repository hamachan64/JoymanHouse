using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AvaterManager : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;

    [SerializeField] AudioClip _audioClip;

    string serverIp = "127.0.0.1"; // サーバーのIPアドレス
    int port = 12345; // サーバーのポート番号

    //実行したいfileを指定
    [SerializeField] string pythonFilePath = "path/to/your_script.py";

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
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


        // ななな文章なら
        if (filename == "bando.wav")
        {
            _animator.SetBool("Nanana", true);
            StartCoroutine(WaitForAudioEnd("Nanana"));
        }

        // スイマメン文章なら
        if (filename == "suimamen.wav")
        {
            _animator.SetBool("Suimamen", true);
            StartCoroutine(WaitForAudioEnd("Suimamen"));
        }
    }

    private IEnumerator WaitForAudioEnd(string pram)
    {
        // 音源が再生終了するまで待機
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        // 音源が終了したらこの関数が呼ばれる
        _animator.SetBool(pram, false);

        RunPythonScript(pythonFilePath);
    }

    void RunPythonScript(string filePath)
    {
        // Pythonの実行ファイルのパスを指定
        string pythonPath = @"C:\Python39\python.exe";

        // プロセス情報を設定
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{filePath}\"",  // 引数としてPythonファイルのパスを指定
            RedirectStandardOutput = true,  // 標準出力をリダイレクト
            UseShellExecute = false,        // シェルを使わない
            CreateNoWindow = true           // 新しいウィンドウを作成しない
        };

        // プロセスを実行
        using (Process process = Process.Start(psi))
        {
            // 標準出力を読み取り
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();  // Pythonスクリプトの実行が終わるまで待機
            UnityEngine.Debug.Log("Python script output: " + output);  // 結果をUnityのコンソールに表示
        }
    }
}
