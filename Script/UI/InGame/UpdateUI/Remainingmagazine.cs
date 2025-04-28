using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayerContolloer;
public class Remainingmagazine : MonoBehaviour
{
    //������ ��ũ��Ʈ �޾ƿ���
    [SerializeField] private Player_Controller player_Controller;

    //�ϴ� �׸���� �θ� ������Ʈ�� ���� Ű�� ���ѿ�
    public bool isTap = false;
    public GameObject[] BaseObj;
    //���� ź�� �ٿ� UI
    [SerializeField] private TextMeshProUGUI T_Remainingmagazine;

    //���� �����ϰ� �ִ� ź�� �ٿ� UI
    [SerializeField] private TextMeshProUGUI T_Haveingagazine;
    //TMPro�� ���ľ� ��
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
