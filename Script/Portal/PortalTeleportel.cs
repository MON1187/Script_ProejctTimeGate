using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalTeleportel : MonoBehaviour
{
    public Transform PlayerObj;
    public Transform recieverObj;

    private bool PlayerIsOverlapping = false;


    private void Update()
    {
        if(PlayerIsOverlapping)
        {
            Vector3 portalToPlayer = PlayerObj.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if(dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, recieverObj.rotation);
                rotationDiff += 180;
                PlayerObj.Rotate(Vector3.up, rotationDiff);

                Vector3 postitionOffset = Quaternion.Euler(0f,rotationDiff,0f) * portalToPlayer;
                PlayerObj.position = recieverObj.position + postitionOffset;

                PlayerIsOverlapping = false;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerIsOverlapping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerIsOverlapping=false;
        }
    }
}
