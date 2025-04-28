using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerCamera¿¡°Ô ÄÄÆÄÀÏ·µ ½ÃÅ´.
public class InPortalCam : MonoBehaviour
{
    public Transform PlayerCamera;
    public Transform Potral;
    public Transform otherPotral;

    private void Update()
    {
        MovePotralCamera();
    }
    public void MovePotralCamera()
    {
        Vector3 PlayerOffSetFormPortal = PlayerCamera.position - otherPotral.position;
        transform.position = Potral.position + PlayerOffSetFormPortal;

        float anuglePortalRotation = Quaternion.Angle(Potral.rotation,otherPotral.rotation);

        Quaternion portalRotation = Quaternion.AngleAxis(anuglePortalRotation, Vector3.up);
        Vector3 newPlayerCamera = portalRotation * PlayerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newPlayerCamera , Vector3.up);
    }
}
