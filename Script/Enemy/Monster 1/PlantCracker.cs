using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

//�÷��̾ �����ϰ� �����ϴ� ��ƼƼ
public class A : MonoBehaviour
{
    public GameObject objEnergyBall;

    public Transform head;
    public Transform FirePosition;
    public Transform PlayerPosition;

    public float AttakeLongTime;        //�Ѿ� �߻� ����
    public float returnAttakeLongTime;  //�ð� �߻� ���� �ʱ�ȭ �뵵
    public float amountMoveSpeed;       //�Ѿ� �ӵ�
    public int Damage = 10;
    public LayerMask targetLayerMask;
    bool isAttake = false;

    public Transform cnrpoison;//Ž�� ��ġ ��   /���߿� �� �缳��
    public float qkswlfma;  //������ /���߿� �� �缳��

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
    /// �÷��̾ �����ҽ� ���� �ϴ� �Լ� �Դϴ�.
    /// �浹�� ���ο� �Ѿ��� �����Ͽ� �ڽ��� �Դ� ���⿡ �߰��������� �ǵ��� ����, �÷��̾ ���� �ٽ� ���󰡰� �˴ϴ�,
    /// </summary>
    void Attake()
    {
        AttakeLongTime = returnAttakeLongTime;
        //���� �κ�
        Vector3 flyingPosition = FirePosition.forward;
        GameObject enagleBall = Instantiate(objEnergyBall,FirePosition.position,Quaternion.identity); 
        Rigidbody rd = enagleBall.GetComponent<Rigidbody>();
        //�Ѿ� ������ ����
        rd.velocity = flyingPosition * amountMoveSpeed;

        enagleBall.GetComponent<EnemyBall>().ValueUpdate(amountMoveSpeed,Damage);
    }
    void SetLookingPlayer()
    {
        Vector3 direction = PlayerPosition.position - head.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        float �Ÿ� = direction.magnitude;
        if(�Ÿ� <= qkswlfma)
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

