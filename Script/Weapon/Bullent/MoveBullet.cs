using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어 총알 스크립트 기본형 입니다.
/// 충돌시 데미지를 넣는 이벤트와 삭제되는 이벤트만 존재합니다.
/// </summary>
public class MoveBullet : MonoBehaviour
{
    public float lifetime = 2f; // 총알 수명 (초)
    private void Start()
    {
        // 일정 시간이 지나면 총알을 제거합니다.
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
