using System.Linq.Expressions;
using UnityEngine;

public class NormalDoor : B_TextUpdate
{
    public Animator animator;
    int index = 0;
    //ó�� ��ȣ�ۿ��� ������ 1�� �ȴ�.
    protected override void instats()
    {
        index++;
        if (index == 1)
        {
            animator.SetBool("isOpen",true);
        }
        else
        {
            animator.SetBool("isOpen", false);
            index = 0;
        }
    }
}
