using System.Linq.Expressions;
using UnityEngine;

public class NormalDoor : B_TextUpdate
{
    public Animator animator;
    int index = 0;
    //처음 상호작용을 받을땐 1이 된다.
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
