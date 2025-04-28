using System.Collections;
using UnityEngine;

/// <summary>
/// ������ ��� Ȱ��ȭ �ȴٸ� �ѹ��� �����ϸ�, �� ���ķ� ������ �ȵȴ�.
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