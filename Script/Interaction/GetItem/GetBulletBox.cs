using PlayerContolloer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulletBox : B_TextUpdate
{
    public Player_Controller player_Controller = default;

    [SerializeField] int addBullet = 7;
    protected override void instats()
    {
        player_Controller.handGun_HaveAmount += addBullet;
        gameObject.SetActive(false);
    }
}
