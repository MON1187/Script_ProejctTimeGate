using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerContolloer;

/// <summary>
/// ���� ������Ʈ�� ������ ��ũ��Ʈ �Դϴ�.
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
