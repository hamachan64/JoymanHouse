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

            // FileSystemWatcher�̐ݒ�
            fileWatcher[i].NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            if (i == 0)
            {
                fileWatcher[i].Filter = "*.wav"; // �Ď��Ώۃt�@�C���̃t�B���^�i��F.wav�t�@�C���̂݁j
                
                // �C�x���g�n���h���̓o�^
                fileWatcher[i].Changed += OnAudioFileChanged;
                fileWatcher[i].Created += OnAudioFileChanged;
            }
            else
            {
                fileWatcher[i].Filter = "*.txt"; // �Ď��Ώۃt�@�C���̃t�B���^�i��F.txt�t�@�C���̂݁j

                // �C�x���g�n���h���̓o�^
                fileWatcher[i].Changed += OnTextFileChanged;
                fileWatcher[i].Created += OnTextFileChanged;
            }

            // �Ď����J�n
            fileWatcher[i].EnableRaisingEvents = true;
        }
    }

    // �t�@�C�����ύX���ꂽ�Ƃ��Ɏ��s�����֐�

    //private void OnFileChanged(object sender, FileSystemEventArgs e)
    //{
    //    Debug.Log($"File {e.ChangeType}: {e.FullPath}");

    //    //System.Threading.Thread.Sleep(100);

    //    Debug.Log(e.FullPath);
    //    // �����œ���̏��������s
    //    StartCoroutine(LoadAudioClip(e.FullPath));
    //}

    private void OnAudioFileChanged(object sender, FileSystemEventArgs e)
    {
        // �t�@�C���p�X���L���[�ɒǉ�
        audioFileQueue.Enqueue(e.FullPath);
    }

    private void OnTextFileChanged(object sender, FileSystemEventArgs e)
    {
        // �e�L�X�g��ǂݍ���
        string fileContent = File.ReadAllText(e.FullPath);
        Debug.Log("File contents:\n" + fileContent);

        texttext.flag = true;
        texttext.content = fileContent;
    }

    void Update()
    {
        // �L���[�Ƀt�@�C���p�X������Ύ擾���ď���
        if (audioFileQueue.TryDequeue(out string audioFilePath))
        {
            StartCoroutine(LoadAudioClip(audioFilePath));
        }
    }

    private IEnumerator LoadAudioClip(string filePath)
    {
        string url = "file:///" + filePath;
        // �t�@�C�����ɉ����ĕ\���ω������邽�߁A�t�@�C�����擾
        string fileName = Path.GetFileName(filePath);

        using (WWW www = new WWW(url))
        {
            yield return www;
            _audioClip = www.GetAudioClip(true, true);

            // �A�j���[�V�����Đ�
            _avaterManager.GetAudioText(_audioClip, fileName);

            Debug.Log("New audio file played: " + filePath);
        }
    }

    void OnDestroy()
    {
        for(int i = 0; i < 2; i++)
        {
            // �Ď����~
            fileWatcher[i].EnableRaisingEvents = false;
            fileWatcher[i].Dispose();
        }
        Debug.Log("File watcher stopped.");
    }
}