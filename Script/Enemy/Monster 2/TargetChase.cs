using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 앤티티가 플레이어 감지및 시선처리 스크립트다.
/// 벽 너머로 플레이어가 있을시 감지가 안된다.
/// 범위 내에 큰 소리가 날시 그 방향으로 보거나 향한다.
/// 플레이어를 발견시 추격하며, 추격 대상이 없을시 자리에서 대기하거나 숨을만한 장소로 움직인다.
/// </summary>
public class TargetChase : MonoBehaviour
{
    //public float moveSpeed;

    [Range(4f, 500f)]
    public float radius = 4;    //반지름
    public float attakeKeepYourDistance = 2;    //거리유지 길이
    //공격 방식 입니다.
    //0은 기본 근접 공격 입니다.
    //1은 돌진 공격 입니다.
    public int attackType = 0;

    public bool isTarGetting = false;  //추격중인지 아닌지 체크용
    public bool randomMoveing = false;

    private NavMeshAgent agent;

    //public GameObject target;   //타겟 오브젝트
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
    /// 플레이어를 발견할시 플레이어의 위치를 업데이트 하며, 해당 위치로 이동 할때까지 움직인다.
    /// 플레이어가 범위 내에서 사라져도 기존 업데이트된 장소까지 움직인다.
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
            ////일정량의 거리를 둔채 지켜보다 돌진합니다.
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
        // 표적의 위치가 설정되어 있을때 실행
        if (isTarGetting)
        {
            // 자신이 해당 목표 위치에 도착했는지 확인
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isTarGetting = false; // 목표 위치에 도착하면 isTarGetting을 false로 설정
                    randomMoveing = true;
                }
            }
        }
    }

    /// <summary>
    /// 플레이어를 추격중이 아닐때 방동하는 함수이다.
    /// 현재위치에서 랜덤한 위치로 이동하는 기능을 갖추고 있다.
    /// 이동 관련 함수를 제작한다면 이곳에서 관리 및 업데이트 하면 된다.
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
