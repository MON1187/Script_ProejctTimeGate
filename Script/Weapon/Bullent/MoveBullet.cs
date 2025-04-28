using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �÷��̾� �Ѿ� ��ũ��Ʈ �⺻�� �Դϴ�.
/// �浹�� �������� �ִ� �̺�Ʈ�� �����Ǵ� �̺�Ʈ�� �����մϴ�.
/// </summary>
public class MoveBullet : MonoBehaviour
{
    public float lifetime = 2f; // �Ѿ� ���� (��)
    private void Start()
    {
        // ���� �ð��� ������ �Ѿ��� �����մϴ�.
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagebal damagebal = other.GetComponent<IDamagebal>();
        if(other.gameObject != null)
        {
            if(other.tag == "Enemy")
            {
                //other.gameObject.GetComponent<E_BaseCode>().Hp -= PlayerStats.Instance.Damage;
                damagebal.Damaged(PlayerStats.Instance.Damage);
            }
            Destroy(gameObject);
        }
    }
}
