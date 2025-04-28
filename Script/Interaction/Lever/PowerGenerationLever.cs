using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerationLever : B_TextUpdate
{
    //�ڱ� �ڽ��� ���� �ִϸ��̼��� �����Ű�� ���� �뵵
    public Animator animator = default;

    //���ɸ� �ڵ����� �����ϱ� ���� �뵵
    [SerializeField]
    private AutoDoor[] autoDoors = default;
    
    //�ʿ� �����ϴ� ����Ʈ���� Ű�� �ϱ� ���� �뵵
    [SerializeField]
    private GameObject[] lightObj = default;

    private void Start()
    {
        for(int i = 0; i < lightObj.Length; i++)
        {
            lightObj[i].SetActive(false);
        }
    }

    protected override void instats()
    {
        animator.SetBool("isOpen", true);
        for (int i = 0; i < autoDoors.Length; i++)
        {
            autoDoors[i].index = 0;
        }
        StartCoroutine(LookingTheLight());
        this.gameObject.layer = 0;
    }
    IEnumerator LookingTheLight()
    {
        yield return null;

        for (int i = 0; i < lightObj.Length; i++)
        {
            lightObj[i].SetActive(true);
        }
        yield break;
    }
}
