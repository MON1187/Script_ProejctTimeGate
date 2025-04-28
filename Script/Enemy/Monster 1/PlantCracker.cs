using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

//플레이어를 감지하고 공격하는 엔티티
public class A : MonoBehaviour
{
    public GameObject objEnergyBall;

    public Transform head;
    public Transform FirePosition;
    public Transform PlayerPosition;

    public float AttakeLongTime;        //총알 발사 간격
    public float returnAttakeLongTime;  //시간 발사 간격 초기화 용도
    public float amountMoveSpeed;       //총알 속도
    public int Damage = 10;
    public LayerMask targetLayerMask;
    bool isAttake = false;

    public Transform cnrpoison;//탐색 위치 축   /나중에 명 재설정
    public float qkswlfma;  //반지름 /나중에 명 재설정

    void Start()
    {
        returnAttakeLongTime = AttakeLongTime;
    }
    public void Update()
    {
        SetLookingPlayer();


        if (AttakeLongTime <= 0 && isAttake)
            Attake();
        else AttakeLongTime -= Time.deltaTime;
        isAttake = false;


    }
    /// <summary>
    /// 플레이어를 감지할시 공격 하는 함수 입니다.
    /// 충돌시 새로운 총알을 생성하여 자신이 왔던 방향에 중간지점까지 되돌아 간후, 플레이어를 향해 다시 날라가게 됩니다,
    /// </summary>
    void Attake()
    {
        AttakeLongTime = returnAttakeLongTime;
        //정의 부분
        Vector3 flyingPosition = FirePosition.forward;
        GameObject enagleBall = Instantiate(objEnergyBall,FirePosition.position,Quaternion.identity); 
        Rigidbody rd = enagleBall.GetComponent<Rigidbody>();
        //총알 움직임 관리
        rd.velocity = flyingPosition * amountMoveSpeed;

        enagleBall.GetComponent<EnemyBall>().ValueUpdate(amountMoveSpeed,Damage);
    }
    void SetLookingPlayer()
    {
        Vector3 direction = PlayerPosition.position - head.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        float 거리 = direction.magnitude;
        if(거리 <= qkswlfma)
        {
            rotation *= Quaternion.Euler(0, -90, 0);

            head.rotation = rotation;
            isAttake = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(cnrpoison.position,qkswlfma);
        Gizmos.color = Color.red;
        
    }
}

