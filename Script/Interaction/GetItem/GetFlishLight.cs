using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerContolloer;

public class GetFlishLight :B_TextUpdate
{
    public GameObject flishLight;
    protected override void instats()
    {
        flishLight.SetActive(false);
        PlayerSituationCheack.getFlashlight = true;
    }
}
