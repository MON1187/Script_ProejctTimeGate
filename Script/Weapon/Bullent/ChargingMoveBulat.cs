using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingMoveBulat : MonoBehaviour
{
    public int lifeTime = 5;

    public int Damageed;
    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagebal damagebal = other.GetComponent<IDamagebal>();
        if(other != null)
        {
            if(other.gameObject.tag == "Enemy")
            {
                damagebal.Damaged(Damageed);
            }
        }
        Destroy(gameObject);
    }
}
