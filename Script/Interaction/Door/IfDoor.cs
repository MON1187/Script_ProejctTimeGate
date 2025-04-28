using System.Collections;
using UnityEngine;

/// <summary>
/// 조건이 모두 활성화 된다면 한번만 실행하며, 그 이후론 실행이 안된다.
/// </summary>
public class IfDoor : B_TextUpdate
{
    [SerializeField] private GameObject[] lever;
    [SerializeField] private Animator animator;
    
    public void CheackLever()
    {
        int a = 0;
        int b = lever.Length;
        for (int i = 0; i < lever.Length; i++)
        {
            bool isOpen = lever[i].GetComponent<Lever_01>().condition;

            if (isOpen)
            {
                a++;
            }
            else
            {
                Debug.Log("Not Open");
            }
        }
        if (a == b)
        {
            animator.SetBool("open", true);
            for(int i = 0;i < lever.Length;i++)
            {
                lever[i].layer = 0;
            }
        }
    }
}