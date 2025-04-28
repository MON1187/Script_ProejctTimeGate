using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerationLever : B_TextUpdate
{
    //자기 자신의 실행 애니메이션을 실행시키기 위한 용도
    public Animator animator = default;

    //락걸린 자동문들 해제하기 위한 용도
    [SerializeField]
    private AutoDoor[] autoDoors = default;
    
    //맵에 존재하는 라이트들을 키게 하기 위한 용도
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
