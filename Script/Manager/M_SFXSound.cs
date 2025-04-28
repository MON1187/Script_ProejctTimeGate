using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class M_SFXSound : MonoBehaviour
{
    public static M_SFXSound instance;

    [SerializeField] private AudioSource soundSFXObjecy;

    [SerializeField] private AudioSource soundSFXHandGunObj;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFireAudioClip(AudioClip clip,Transform spawnTtransform, float volume)
    {
        //����� ����
        AudioSource audioSource = Instantiate(soundSFXObjecy, spawnTtransform.position, Quaternion.identity);

        //����� Ŭ�� ����
        audioSource.clip = clip;

        //���� ����
        audioSource.volume = volume;

        //����� ���
        audioSource.Play();

        //����� �ð� ����
        float playTime = audioSource.clip.length;

        //����� �ı�
        Destroy(audioSource);
    }

    public void PlayRelaodAudioClip(AudioClip clip, float volume)
    {
        //GameObject obj = reloadObj.gameObject;

        AudioSource reloadObj = soundSFXHandGunObj;

        reloadObj.clip = clip;

        reloadObj.volume = volume;

        reloadObj.Play();

        float playTime = reloadObj.clip.length;

        //Invoke();
    }
    public void PlayNullAmountFireClip(AudioClip clip, float volume)
    {
        AudioSource audiosource = soundSFXHandGunObj;

        audiosource.clip = clip;

        audiosource.volume = volume;

        audiosource.Play();
    }
}
