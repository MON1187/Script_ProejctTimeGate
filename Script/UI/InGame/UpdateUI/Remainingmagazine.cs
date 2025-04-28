using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayerContolloer;
public class Remainingmagazine : MonoBehaviour
{
    //참조할 스크립트 받아오기
    [SerializeField] private Player_Controller player_Controller;

    //하단 항목들의 부모 오브젝트을 껏다 키기 위한용
    public bool isTap = false;
    public GameObject[] BaseObj;
    //남은 탄약 뛰울 UI
    [SerializeField] private TextMeshProUGUI T_Remainingmagazine;

    //현재 소지하고 있는 탄약 뛰울 UI
    [SerializeField] private TextMeshProUGUI T_Haveingagazine;
    //TMPro로 고쳐야 됨
    [SerializeField] private Slider S_RemaininHp;
    private void Start()
    {
        for(int i = 0; i <  BaseObj.Length; i++)
        {
            BaseObj[i].SetActive(false);
        }
        S_RemaininHp.maxValue = PlayerStats.Instance.maxSpeed;
        S_RemaininHp.value = PlayerStats.Instance.hp;
    }
    private void InputKey()
    {
        if (Input.GetKeyDown(PlayerStats.Instance.K_Tab))
        {
            for (int i = 0; i < BaseObj.Length; i++)
            {
                BaseObj[i].SetActive(true);
            }
        }
        else if(Input.GetKeyUp(PlayerStats.Instance.K_Tab))
        {
            for (int i = 0; i < BaseObj.Length; i++)
            {
                BaseObj[i].SetActive(false);
            }
        }
    }
    public void Update()
    {
        InputKey();
        

        T_Update();
        S_Update();
    }

    void T_Update()
    {
        T_Remainingmagazine.text = player_Controller.handGun_CurAmount.ToString() + " / " + player_Controller.handGun_MaxAmount.ToString();

        T_Haveingagazine.text = player_Controller.handGun_HaveAmount.ToString();
    }
    void S_Update()
    {
        S_RemaininHp.value = PlayerStats.Instance.hp;
    }
}
