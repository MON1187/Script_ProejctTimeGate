using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


//플레이어 스텟 메니저
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float curSpeed;          //현재의 움직임 저장하여 Sped 초기화에 사용
    public float speed = 3;
    public float maxSpeed = 5;      
    public float runSpeed = 1.5f;   //해당 값은 곱셉용
    //public int JumpForce = 8;

    public int hp = 20;
    public int haxHp = 20;

    public int Damage = 1;
    public int defance = 0;
    #region Input Key
    public KeyCode K_Jump = KeyCode.Space;      //점프 키
    public KeyCode K_Run = KeyCode.LeftShift;   //달리기
    public KeyCode K_Esc = KeyCode.Escape;      //설정 화면을 확인하기 위한 키
    public KeyCode K_Shot = KeyCode.Mouse0;     //총알 발사 키
    public KeyCode K_Zoom = KeyCode.Mouse1;     
    public KeyCode K_Reload = KeyCode.R;        //재장전 키
    public KeyCode K_Interaction = KeyCode.F;   //상호작용 키
    public KeyCode K_Tab = KeyCode.Q;         //UI확인 하기 위한 키
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(this);

        //MaxSpeed = Speed * 2 - ( Speed / 2);
    }
    private void Start()
    {
        curSpeed = speed;
    }
}
namespace PlayerContolloer //플레이어 상황 체크용
{
    public static class PlayerSituationCheack
    {
        //플레이어 행동 체크
        public static bool IsRun = false;   //달리고 있는지 체크
        public static bool isInteraction = false; //현재 상호작용 중인지 체크

        //해당 무기를 지니고 있는지 확인용
        public static bool getHandGun = false;
        public static bool getAKM = false;

        public static bool getFlashlight = false;

        //해금된 시스템
        public static bool openInteraction = false;    //상호작용 가능한지 여부 판단
    }

    public enum ChangeWeapone
    {
        Base,       //기본 모듈입니다.
        Charging,   //차징형 모듈 입니다.
        spacetime   //시간차 공격 모듈 입니다.
    }
}
