using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator animator;

    //Transform[] returnTransform;

    //0�� ���ǹ� ���� , 1�� ���ǹ��� �ִ� ���̴�.
    [Range(0,1)]
    public int index = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        //returnTransform = new Transform[transform.childCount];

        //// �� �ڽ��� Transform�� �迭�� �Ҵ�
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    returnTransform[i] = transform.GetChild(i);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (index == 0)
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
                {
                    animator.SetBool("open", true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other != null)
        {
            if (index == 0)
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
                {
                    animator.SetBool("open", false);
                }
            }
        }
    }
}
