using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// 식물형 크래쳐가 발사하는 총알 스크립트 입니다.
/// 처음 발사한 방향으로 일직선으로 이동하게 됩니다.
/// 충돌시 범위 피해를 입히게 되며, 자신이 왔던 방향의 절반 만큼 되돌아 가게 됩니다.
/// 모두 되돌아 가게 되었다면 다시 플레이어를 향해 날라가게 됩니다.
/// 두번째 충돌에서 오브젝트는 제거된다.
/// </summary>
public class EnemyBall : MonoBehaviour
{
    public bool boom = false;

    private float moveSpeed;
    private int damage;
    private int index = 0;

    public int go = 1; //while문 반복문 on/off 용
                //ReturnMovePosition메서드에서 while문 사용용이다.
                //처음은 on 으로 되어있다.

    private Vector3[] indexPosition;
    private Vector3 startPosition;
    private Vector3 lastPosition;
    Vector3 secondDirection;    //현재 위치 중간값

    Rigidbody rd;
    private Transform PlayerPosition;
    public LayerMask enmeyBal;
    //PlayerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    //식물형 크랩쳐에게서 속도를 받아옴
    public void ValueUpdate(float speed,int setDamage)
    {
        damage = setDamage;
        moveSpeed = speed;
    }
    
    private void Start()
    {

        rd = gameObject.GetComponent<Rigidbody>();

        PlayerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        indexPosition = new Vector3[5000];
        startPosition = transform.position;
        StartCoroutine(ReturnPosition(gameObject));

        Invoke("LifeTime", 8f);
    }
    private void Update()
    {
        Collider[] collider = Physics.OverlapSphere(secondDirection, 1, enmeyBal);

        foreach (Collider collider2 in collider)
        {
            if(collider2)
            {
                StartCoroutine(ReturnToPlayer(rd));
            }

        }
        if (go > 2)
        {
            Destroy(gameObject);
        }
        else if(go == 2)
        {
            Invoke("LifeTime", 8f);
        }
    }
    void LifeTime()
    {
        if(go < 2)
        {
            go++;
            StopCoroutine(ReturnPosition(gameObject));
            ReturnMovePosition(gameObject);
        }
    }

    void ReturnMovePosition(GameObject enagleBall)
    {
        if (enagleBall != null)
        {
            //처음 지점과 마지막 지점 위치의 중간값을 받음
            Vector3 midPos = (startPosition + lastPosition) / 2;

            //배열의 크기를 구함
            //코드상 배열의 크기를 미리 100이라 임의로 저장한 상태이기에 index형 변수를 받는게 더 올바름
            //1. index를 받아옴
            int A = index;

            //저장된 값의 가운데 값을 저장
            A /= 2;
            //여기부터 수정하면 됨.
            //위치값이 이상하게 받아지는 경우가 많음 ㅇㅇ
            //Vector3 moveVector = indexPosition[A];

            //중간 지점 기준으로 터진 시점이 더 멀다면 발생
            //초기 위치 / 마지막 위치중 어디가 더 가까운지 확인.
            if (Vector3.Distance(midPos, indexPosition[A]) < Vector3.Distance(midPos, indexPosition[A-1]))
            {
                secondDirection = indexPosition[A];
                secondDirection.y = indexPosition[A].y;
            }
            else if(Vector3.Distance(midPos, indexPosition[A]) > Vector3.Distance(midPos, indexPosition[A - 1]))
            {
                secondDirection = indexPosition[A - 1];
                secondDirection.y = indexPosition[A - 1].y;
            }
            else
            {
                secondDirection = startPosition;
                secondDirection.y = startPosition.y;
            }
            //되돌아 가는 코드
            //Debug.Log(secondDirection);
            Vector3 e = startPosition - lastPosition;
            rd.velocity = Vector3.zero;
            rd.velocity = e * moveSpeed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagebal damagebal = other.GetComponent<IDamagebal>();
        go++;
        lastPosition = transform.position;
        if (other != null)
        {
            StopCoroutine(ReturnPosition(gameObject));
            ReturnMovePosition(gameObject);
            if (other.gameObject.tag == "Player")
            {
                damagebal.Damaged(damage);
            }
        }
    }

    /// <summary>
    ///시작되자 마자 호출하게 되는 코루틴
    ///지속적으로 위치를 업데이트
    /// </summary>
    /// <param name="enagleBall"></param>
    /// <returns></returns>
    IEnumerator ReturnPosition(GameObject enagleBall)
    {
        Vector3 pos = enagleBall.transform.position;
        startPosition = pos;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            indexPosition[index] = transform.position;
            index++;
        }
    }

    /// <summary>
    ///중간 지점에 도착하였다면 발생하는 메서드다.
    ///플레이어를 향해 날라간다.
    /// </summary>
    /// <param name="rd"></param>
    /// <param name="rotaitionQuation"></param>
    /// <returns></returns>

    IEnumerator ReturnToPlayer(Rigidbody rd)
    {
        Invoke("LifeTime", 5f);
        yield return new WaitForSeconds(0.5f); // 잠시 대기 후 다시 발사

        Vector3 directionToPlayer = (PlayerPosition.position - transform.position).normalized;

        rd.velocity = directionToPlayer * moveSpeed;
        yield return null;
    }

}