using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator animator;

    //Transform[] returnTransform;

    //0은 조건문 없는 , 1은 조건문이 있는 문이다.
    [Range(0,1)]
    public int index = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        //returnTransform = new Transform[transform.childCount];

        //// 각 자식의 Transform을 배열에 할당
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
