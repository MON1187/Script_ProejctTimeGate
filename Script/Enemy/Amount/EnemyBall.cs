using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// �Ĺ��� ũ���İ� �߻��ϴ� �Ѿ� ��ũ��Ʈ �Դϴ�.
/// ó�� �߻��� �������� ���������� �̵��ϰ� �˴ϴ�.
/// �浹�� ���� ���ظ� ������ �Ǹ�, �ڽ��� �Դ� ������ ���� ��ŭ �ǵ��� ���� �˴ϴ�.
/// ��� �ǵ��� ���� �Ǿ��ٸ� �ٽ� �÷��̾ ���� ���󰡰� �˴ϴ�.
/// �ι�° �浹���� ������Ʈ�� ���ŵȴ�.
/// </summary>
public class EnemyBall : MonoBehaviour
{
    public bool boom = false;

    private float moveSpeed;
    private int damage;
    private int index = 0;

    public int go = 1; //while�� �ݺ��� on/off ��
                //ReturnMovePosition�޼��忡�� while�� �����̴�.
                //ó���� on ���� �Ǿ��ִ�.

    private Vector3[] indexPosition;
    private Vector3 startPosition;
    private Vector3 lastPosition;
    Vector3 secondDirection;    //���� ��ġ �߰���

    Rigidbody rd;
    private Transform PlayerPosition;
    public LayerMask enmeyBal;
    //PlayerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    //�Ĺ��� ũ���Ŀ��Լ� �ӵ��� �޾ƿ�
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
            //ó�� ������ ������ ���� ��ġ�� �߰����� ����
            Vector3 midPos = (startPosition + lastPosition) / 2;

            //�迭�� ũ�⸦ ����
            //�ڵ�� �迭�� ũ�⸦ �̸� 100�̶� ���Ƿ� ������ �����̱⿡ index�� ������ �޴°� �� �ùٸ�
            //1. index�� �޾ƿ�
            int A = index;

            //����� ���� ��� ���� ����
            A /= 2;
            //������� �����ϸ� ��.
            //��ġ���� �̻��ϰ� �޾����� ��찡 ���� ����
            //Vector3 moveVector = indexPosition[A];

            //�߰� ���� �������� ���� ������ �� �ִٸ� �߻�
            //�ʱ� ��ġ / ������ ��ġ�� ��� �� ������� Ȯ��.
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
            //�ǵ��� ���� �ڵ�
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
    ///���۵��� ���� ȣ���ϰ� �Ǵ� �ڷ�ƾ
    ///���������� ��ġ�� ������Ʈ
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
    ///�߰� ������ �����Ͽ��ٸ� �߻��ϴ� �޼����.
    ///�÷��̾ ���� ���󰣴�.
    /// </summary>
    /// <param name="rd"></param>
    /// <param name="rotaitionQuation"></param>
    /// <returns></returns>

    IEnumerator ReturnToPlayer(Rigidbody rd)
    {
        Invoke("LifeTime", 5f);
        yield return new WaitForSeconds(0.5f); // ��� ��� �� �ٽ� �߻�

        Vector3 directionToPlayer = (PlayerPosition.position - transform.position).normalized;

        rd.velocity = directionToPlayer * moveSpeed;
        yield return null;
    }

}