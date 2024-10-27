using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaterManager : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;

    [SerializeField] AudioClip _audioClip;

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
    }
}
