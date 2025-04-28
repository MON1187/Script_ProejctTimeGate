using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 정해진 위치로 앞으로 직진하는 몬스터 이다.
/// 다른곳으로 움직이지 않으며, 전방에 있는 모든것을 먹는다.(?)
/// </summary>
public class MoveAIEnemy : MonoBehaviour
{
    public Transform[] indexPosition;
    public LayerMask indexLayer;

    public float moveSpeed = 3;

    public int qkswlfma;        //반지름//나중에 개명
    private int index = 0;
    private int maxIndex;
    private NavMeshAgent cc;
    // Start is called before the first frame update
    void Start()
    {
        maxIndex = indexPosition.Length;
        Debug.Log(maxIndex);
        cc = GetComponent<NavMeshAgent>();
        cc.SetDestination(indexPosition[index].position);
    }

    // Update is called once per frame
    void Update()
    {
        cc.speed = moveSpeed;
        NextIndexMove();

    }
    void NextIndexMove()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, qkswlfma, indexLayer);

        foreach (Collider other in colliders) 
        {
            if(other)
            {
                if (transform.position.x == indexPosition[index].position.x && transform.position.z == indexPosition[index].position.z)
                {
                    index++;
                    if (index == maxIndex)
                    {
                        index = 0;
                    }
                    cc.SetDestination(indexPosition[index].position);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,qkswlfma);
        Gizmos.color = Color.yellow;
    }
}
