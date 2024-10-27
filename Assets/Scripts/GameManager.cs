using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AvaterManager _avaterManager;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    AudioClip _audioClip;
    FileSystemWatcher[] fileWatcher = new FileSystemWatcher[2];
    private ConcurrentQueue<string> audioFileQueue = new ConcurrentQueue<string>();

    [SerializeField] TextManager texttext;


    void Start()
    {

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                fileWatcher[i] = new FileSystemWatcher(Application.dataPath + "/Audio");
            }
            else
            {
                fileWatcher[i] = new FileSystemWatcher(Application.dataPath + "/Text");
            }

            // FileSystemWatcherの設定
            fileWatcher[i].NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            if (i == 0)
            {
                fileWatcher[i].Filter = "*.wav"; // 監視対象ファイルのフィルタ（例：.wavファイルのみ）
                
                // イベントハンドラの登録
                fileWatcher[i].Changed += OnAudioFileChanged;
                fileWatcher[i].Created += OnAudioFileChanged;
            }
            else
            {
                fileWatcher[i].Filter = "*.txt"; // 監視対象ファイルのフィルタ（例：.txtファイルのみ）

                // イベントハンドラの登録
                fileWatcher[i].Changed += OnTextFileChanged;
                fileWatcher[i].Created += OnTextFileChanged;
            }

            // 監視を開始
            fileWatcher[i].EnableRaisingEvents = true;
        }
    }

    // ファイルが変更されたときに実行される関数

    //private void OnFileChanged(object sender, FileSystemEventArgs e)
    //{
    //    Debug.Log($"File {e.ChangeType}: {e.FullPath}");

    //    //System.Threading.Thread.Sleep(100);

    //    Debug.Log(e.FullPath);
    //    // ここで特定の処理を実行
    //    StartCoroutine(LoadAudioClip(e.FullPath));
    //}

    private void OnAudioFileChanged(object sender, FileSystemEventArgs e)
    {
        // ファイルパスをキューに追加
        audioFileQueue.Enqueue(e.FullPath);
    }

    private void OnTextFileChanged(object sender, FileSystemEventArgs e)
    {
        // テキストを読み込み
        string fileContent = File.ReadAllText(e.FullPath);
        Debug.Log("File contents:\n" + fileContent);

        texttext.flag = true;
        texttext.content = fileContent;
    }

    void Update()
    {
        // キューにファイルパスがあれば取得して処理
        if (audioFileQueue.TryDequeue(out string audioFilePath))
        {
            StartCoroutine(LoadAudioClip(audioFilePath));
        }
    }

    private IEnumerator LoadAudioClip(string filePath)
    {
        string url = "file:///" + filePath;
        // ファイル名に応じて表情を変化させるため、ファイル名取得
        string fileName = Path.GetFileName(filePath);

        using (WWW www = new WWW(url))
        {
            yield return www;
            _audioClip = www.GetAudioClip(true, true);

            // アニメーション再生
            _avaterManager.GetAudioText(_audioClip, fileName);

            Debug.Log("New audio file played: " + filePath);
        }
    }

    void OnDestroy()
    {
        for(int i = 0; i < 2; i++)
        {
            // 監視を停止
            fileWatcher[i].EnableRaisingEvents = false;
            fileWatcher[i].Dispose();
        }
        Debug.Log("File watcher stopped.");
    }
}