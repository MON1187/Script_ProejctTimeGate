using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ������ ��ġ�� ������ �����ϴ� ���� �̴�.
/// �ٸ������� �������� ������, ���濡 �ִ� ������ �Դ´�.(?)
/// </summary>
public class MoveAIEnemy : MonoBehaviour
{
    public Transform[] indexPosition;
    public LayerMask indexLayer;

    public float moveSpeed = 3;

    public int qkswlfma;        //������//���߿� ����
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
