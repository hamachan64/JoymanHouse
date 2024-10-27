using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AvaterManager : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;

    [SerializeField] AudioClip _audioClip;

    string serverIp = "127.0.0.1"; // �T�[�o�[��IP�A�h���X
    int port = 12345; // �T�[�o�[�̃|�[�g�ԍ�

    //���s������file���w��
    [SerializeField] string pythonFilePath = "path/to/your_script.py";

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
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


        // �ȂȂȕ��͂Ȃ�
        if (filename == "bando.wav")
        {
            _animator.SetBool("Nanana", true);
            StartCoroutine(WaitForAudioEnd("Nanana"));
        }

        // �X�C�}�������͂Ȃ�
        if (filename == "suimamen.wav")
        {
            _animator.SetBool("Suimamen", true);
            StartCoroutine(WaitForAudioEnd("Suimamen"));
        }
    }

    private IEnumerator WaitForAudioEnd(string pram)
    {
        // �������Đ��I������܂őҋ@
        yield return new WaitUntil(() => !_audioSource.isPlaying);

        // �������I�������炱�̊֐����Ă΂��
        _animator.SetBool(pram, false);

        RunPythonScript(pythonFilePath);
    }

    void RunPythonScript(string filePath)
    {
        // Python�̎��s�t�@�C���̃p�X���w��
        string pythonPath = @"C:\Python39\python.exe";

        // �v���Z�X����ݒ�
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{filePath}\"",  // �����Ƃ���Python�t�@�C���̃p�X���w��
            RedirectStandardOutput = true,  // �W���o�͂����_�C���N�g
            UseShellExecute = false,        // �V�F�����g��Ȃ�
            CreateNoWindow = true           // �V�����E�B���h�E���쐬���Ȃ�
        };

        // �v���Z�X�����s
        using (Process process = Process.Start(psi))
        {
            // �W���o�͂�ǂݎ��
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();  // Python�X�N���v�g�̎��s���I���܂őҋ@
            UnityEngine.Debug.Log("Python script output: " + output);  // ���ʂ�Unity�̃R���\�[���ɕ\��
        }
    }
}
