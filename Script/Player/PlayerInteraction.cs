using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerContolloer;
/// <summary>
/// �÷��̾ �ٶ󺸴� ������ ��ȣ�ۿ� ��ü�� �ִ��� Ȯ���մϴ�.
/// ��ȣ�ۿ��� �ִ� ��ü�� �ִٸ� �ؽ�Ʈ�� �ٿ��, ��ȣ�ۿ�� �ش� ��ü�� ���ڰ��� �޾� �ɴϴ�.
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
            //���� ��ü��ŭ �������� �Ÿ��� �پ��.
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

    //�ٸ� ��ü�� �������� �浹�Ҷ� �ش� ��ü�� B_TextUpdate�� ���� �ִٸ� �ش� ��ü�� ���ڰ� string�� ���� �޾� �� �� ȭ��� �ؽ�Ʈ ��¿� ��˵˴ϴ�.
    public void T_UpdateInteraction(string textMessge)
    {
        text.text = textMessge;
    }
}
/// <summary>
/// �ٸ� ��ȣ�ۿ� ��ü ��ũ��Ʈ �ۼ��� �ش� Ŭ������ �����Ͽ� ȭ�鿡 �ؽ�Ʈ ��¿� ���� �ؽ�Ʈ�� ������� �����ϰ� �˴ϴ�.
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
