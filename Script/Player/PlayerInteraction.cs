using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerContolloer;
/// <summary>
/// 플레이어가 바라보는 방향의 상호작용 물체가 있는지 확인합니다.
/// 상호작용이 있는 물체가 있다면 텍스트를 뛰우며, 상호작용시 해당 물체의 인자값을 받아 옵니다.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject mainCam;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float low = 5;

    public LayerMask hitLayerMask;
    void Update()
    {
        if(PlayerSituationCheack.openInteraction == true)
        RayHitUpdatte();
    }
    void RayHitUpdatte()
    {
        //low = oringLow;
        T_UpdateInteraction(string.Empty);

        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hitDate;
        Debug.DrawRay(ray.origin, ray.direction * low);
        if (Physics.Raycast(ray, out hitDate, low, hitLayerMask))
        {
            //닿은 물체만큼 레이저의 거리가 줄어듬.
            //low = Vector3.Distance(ray.origin,hitDate.transform.position);
            if (hitDate.collider.GetComponent<B_TextUpdate>() != null)
            {
                B_TextUpdate instats = hitDate.collider.GetComponent<B_TextUpdate>();
                T_UpdateInteraction(instats.text);
                if (Input.GetKeyDown(PlayerStats.Instance.K_Interaction))
                {
                    instats.BaseInteract();
                    //Destroy(hitDate.transform.gameObject);
                }
            }
        }
    }

    //다른 객체와 레이저가 충돌할때 해당 객체가 B_TextUpdate를 갖고 있다면 해당 객체의 인자값 string형 값을 받아 온 후 화면상에 텍스트 출력에 사옹됩니다.
    public void T_UpdateInteraction(string textMessge)
    {
        text.text = textMessge;
    }
}
/// <summary>
/// 다른 상호작용 물체 스크립트 작성시 해당 클래스를 참조하여 화면에 텍스트 출력에 무슨 텍스트를 출력할지 결정하게 됩니다.
/// </summary>
/// 
public abstract class B_TextUpdate : MonoBehaviour
{
    public string text;

    public void BaseInteract()
    {
        instats();
    }
    protected virtual void instats()
    {

    }
}
