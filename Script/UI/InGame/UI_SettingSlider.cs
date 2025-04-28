using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UI_boolManager;
using UI_SettingManager;
using static UnityEngine.PlayerPrefs;
/// <summary>
/// UI의 B_ , S_ 등을 관리 합니다.
/// </summary>

public class UI_SettingSlider : MonoBehaviour
{

    //플레이어 화면 감도설정 슬라이더
    [SerializeField] private Slider S_ScreenSensitivity;

    //음악 저장 변수
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    [SerializeField] private GameObject settingUICanvers;
    [SerializeField] private GameObject gmaePlayUi;
    [SerializeField] private GameObject musicUi;
    [SerializeField] private GameObject ControllerUi;

    int settingUICanversCheackValue = 0;

    private void Start()
    {
        LoadDate();
    }

    void LoadDate()
    {
        m_MusicMasterSlider.value = GetInt("Master Music Value");
        m_MusicBGMSlider.value = GetInt("Bgm Music Value");
        m_MusicSFXSlider.value = GetInt("Sfx Music Value");
    }

    private void Update()
    {
        if (Input.GetKeyDown(PlayerStats.Instance.K_Esc))
        {
            if (settingUICanversCheackValue == 1)
            {
                B_SetSettingMenu_Off();
            }
            else
            {
                B_SetSettingMenu_On();
            }
        }
    }

    #region Music function
    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        SetInt("Master Music Value", (int)volume);
    }

    public void SetBGMVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        SetInt("Bgm Music Value", (int)volume);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        SetInt("Sfx Music Value", (int)volume);
    }

    #endregion

    #region Button function
    //셋팅창 ON/OFF
    public void B_SetSettingMenu_On()
    {
        settingUICanvers.SetActive(true);
        B_GamePlay();
        ValueManager.setMenu = true;
        settingUICanversCheackValue = 1;
    }
    public void B_SetSettingMenu_Off()
    {
        settingUICanvers.SetActive(false);
        ValueManager.setMenu = false;
        settingUICanversCheackValue = 0;
    }
    public void B_GamePlay()
    {
        gmaePlayUi.SetActive(true);
        musicUi.SetActive(false);
        ControllerUi.SetActive(false);
    }
    public void B_Music()
    {
        gmaePlayUi.SetActive(false);
        musicUi.SetActive(true);
        ControllerUi.SetActive(false);
    }
    public void B_Controller()
    {
        gmaePlayUi.SetActive(false);
        musicUi.SetActive(false);
        ControllerUi.SetActive(true);
    }
    #endregion
}
