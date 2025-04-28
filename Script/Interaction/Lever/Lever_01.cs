using UnityEngine;

/// <summary>
/// 주로 문과 관련된 것이 상호작용 할때 사용됩니다.
/// 대상이 하나일 때만 사용됩니다.
/// </summary>
public class Lever_01 : B_TextUpdate
{
    public bool condition = false;
    public Animator animator = default;

    //해당 문의 조건문을 체크 하게 실행하기 위한 변수
    public IfDoor door;
    // Update is called once per frame

    protected override void instats()
    {
        condition = true;
        animator.SetBool("isOpen",condition);
        door.CheackLever();
    }
}
