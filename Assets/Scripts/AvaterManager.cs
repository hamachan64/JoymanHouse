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
    }
}
