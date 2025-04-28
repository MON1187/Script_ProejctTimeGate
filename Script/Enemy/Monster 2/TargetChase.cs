using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ��ƼƼ�� �÷��̾� ������ �ü�ó�� ��ũ��Ʈ��.
/// �� �ʸӷ� �÷��̾ ������ ������ �ȵȴ�.
/// ���� ���� ū �Ҹ��� ���� �� �������� ���ų� ���Ѵ�.
/// �÷��̾ �߽߰� �߰��ϸ�, �߰� ����� ������ �ڸ����� ����ϰų� �������� ��ҷ� �����δ�.
/// </summary>
public class TargetChase : MonoBehaviour
{
    //public float moveSpeed;

    [Range(4f, 500f)]
    public float radius = 4;    //������
    public float attakeKeepYourDistance = 2;    //�Ÿ����� ����
    //���� ��� �Դϴ�.
    //0�� �⺻ ���� ���� �Դϴ�.
    //1�� ���� ���� �Դϴ�.
    public int attackType = 0;

    public bool isTarGetting = false;  //�߰������� �ƴ��� üũ��
    public bool randomMoveing = false;

    private NavMeshAgent agent;

    //public GameObject target;   //Ÿ�� ������Ʈ
    public LayerMask targetLayer;

    private Vector3 holdPosition;
    private Transform isTargetPosition;
    private void Start()
    {
        holdPosition = transform.position;
        isTargetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        MoveingPosition();
        RandomMoveing();
    }
    /// <summary>
    /// �÷��̾ �߰��ҽ� �÷��̾��� ��ġ�� ������Ʈ �ϸ�, �ش� ��ġ�� �̵� �Ҷ����� �����δ�.
    /// �÷��̾ ���� ������ ������� ���� ������Ʈ�� ��ұ��� �����δ�.
    /// </summary>
    private void MoveingPosition()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius, targetLayer);

        foreach (Collider other in collider)
        {
            isTargetPosition = other.transform;
            
            if(attackType == 0)
            {
                if (transform.position.x != isTargetPosition.position.x && transform.position.z != isTargetPosition.position.z)
                {
                    agent.SetDestination(isTargetPosition.position);
                    isTarGetting = true;
                }
            }
            ////�������� �Ÿ��� ��ä ���Ѻ��� �����մϴ�.
            //else if(attackType == 1)
            //{
            //    Vector3 KeepYourDistance = transform.position - isTargetPosition.position;

            //    if(Vector3.Distance(transform.position,isTargetPosition.position) <= KeepYourDistance.magnitude)
            //    {
                    
            //    }
            //    else
            //    {
            //        agent.SetDestination(isTargetPosition.position);
            //    }

            //    //if () ;
            //}
        }
        // ǥ���� ��ġ�� �����Ǿ� ������ ����
        if (isTarGetting)
        {
            // �ڽ��� �ش� ��ǥ ��ġ�� �����ߴ��� Ȯ��
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isTarGetting = false; // ��ǥ ��ġ�� �����ϸ� isTarGetting�� false�� ����
                    randomMoveing = true;
                }
            }
        }
    }

    /// <summary>
    /// �÷��̾ �߰����� �ƴҶ� �浿�ϴ� �Լ��̴�.
    /// ������ġ���� ������ ��ġ�� �̵��ϴ� ����� ���߰� �ִ�.
    /// �̵� ���� �Լ��� �����Ѵٸ� �̰����� ���� �� ������Ʈ �ϸ� �ȴ�.
    /// </summary>
    void RandomMoveing()
    {
        if(!isTarGetting)
        {
            randomMoveing = true;

            if (randomMoveing)
            {

                randomMoveing = false;
            }
        }
    }
}
