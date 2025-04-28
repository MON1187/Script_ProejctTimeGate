using UnityEngine;

/// <summary>
/// �ַ� ���� ���õ� ���� ��ȣ�ۿ� �Ҷ� ���˴ϴ�.
/// ����� �ϳ��� ���� ���˴ϴ�.
/// </summary>
public class Lever_01 : B_TextUpdate
{
    public bool condition = false;
    public Animator animator = default;

    //�ش� ���� ���ǹ��� üũ �ϰ� �����ϱ� ���� ����
    public IfDoor door;
    // Update is called once per frame

    protected override void instats()
    {
        condition = true;
        animator.SetBool("isOpen",condition);
        door.CheackLever();
    }
}
