using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI_SettingManager;

/// <summary>
/// 타이틀 관리 스크립트 입니다.
/// </summary>
public class StartWindowScript : MonoBehaviour
{
    [Header("Start Window")]
    public GameObject StartPanel;
    public GameObject SettingPanel;
    public GameObject CreditPanel;
    public GameObject CreditButton;

    [Header("Setting Window")]
    public GameObject gamePlay;
    public GameObject Music;
    public GameObject Contorller;
    private void Start()
    {
        StartPanel.SetActive(true);
        SettingPanel.SetActive(false);
    }
    public void B_Start()
    {
        SceneManager.LoadScene("InScenes");
        //SceneManager.LoadScene("TestScenes");
    }

    public void B_SettingOn()
    {
        StartPanel.SetActive(false);
    }
    public void B_StartCanversOn()
    {
        StartPanel.SetActive(true);
    }
    public void B_CreidtON()
    {
        CreditPanel.SetActive(true);
        StartPanel.SetActive(false);
    }
    public void B_CreidtOFF()
    {
        CreditPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
    public void B_Exit()
    {
        Application.Quit();
    }
}
