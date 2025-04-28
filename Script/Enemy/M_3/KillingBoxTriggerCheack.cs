using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingBoxTriggerCheack : MonoBehaviour
{
    public LayerMask killingMask;
    private void OnTriggerEnter(Collider other)
    {
        IDamagebal damagebal = other.GetComponent<IDamagebal>();
        if(other != null)
        {
            if(other.gameObject.tag == "Player")
            {
                damagebal.Damaged(1000000f);
            }
        }
    }
}
