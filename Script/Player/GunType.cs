
namespace PlayerContolloer
{
    using UnityEngine;

    /// <summary>
    /// 플레이어의 무기 타입을 관리한다.
    /// 플레이어의 무기 개방 조건문을 관리 한다.
    /// </summary>
    //public enum CoreWeaponType
    //{
    //    NomalMode,
    //    ChargeMode,         //충전식 무기로 에너지탄을 발사할수 있다.
    //    CoreBluntMode       //궁극기 방식으로 시간차 공격
    //}
    public enum WeaponType
    {
        Null = 0,
        Handgun,
        AKM,
    }
    public class WeaponTypeClass : MonoBehaviour
    {
        //모듈 관리하는 코드 작성
    }
}
