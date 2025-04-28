using UnityEngine;
using PlayerContolloer;
public class GetHandGun : B_TextUpdate
{
    public GameObject thisobj;
    protected override void instats()
    {
        PlayerSituationCheack.getHandGun = true;
        thisobj.SetActive(false);
    }
}
