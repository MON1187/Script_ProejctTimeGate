using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerContolloer;

/// <summary>
/// 선반 오브젝트가 실행할 스크립트 입니다.
/// </summary>
public class Shelf : B_TextUpdate
{
    public Animator animator;
    
    protected override void instats()
    {
        Debug.Log("ok");
        PlayerSituationCheack.isInteraction = true;
        animator.SetBool("Play",true);

        this.gameObject.layer = 0;
    }
}
