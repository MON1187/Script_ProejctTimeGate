using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


//�÷��̾� ���� �޴���
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float curSpeed;          //������ ������ �����Ͽ� Sped �ʱ�ȭ�� ���
    public float speed = 3;
    public float maxSpeed = 5;      
    public float runSpeed = 1.5f;   //�ش� ���� ������
    //public int JumpForce = 8;

    public int hp = 20;
    public int haxHp = 20;

    public int Damage = 1;
    public int defance = 0;
    #region Input Key
    public KeyCode K_Jump = KeyCode.Space;      //���� Ű
    public KeyCode K_Run = KeyCode.LeftShift;   //�޸���
    public KeyCode K_Esc = KeyCode.Escape;      //���� ȭ���� Ȯ���ϱ� ���� Ű
    public KeyCode K_Shot = KeyCode.Mouse0;     //�Ѿ� �߻� Ű
    public KeyCode K_Zoom = KeyCode.Mouse1;     
    public KeyCode K_Reload = KeyCode.R;        //������ Ű
    public KeyCode K_Interaction = KeyCode.F;   //��ȣ�ۿ� Ű
    public KeyCode K_Tab = KeyCode.Q;         //UIȮ�� �ϱ� ���� Ű
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
namespace PlayerContolloer //�÷��̾� ��Ȳ üũ��
{
    public static class PlayerSituationCheack
    {
        //�÷��̾� �ൿ üũ
        public static bool IsRun = false;   //�޸��� �ִ��� üũ
        public static bool isInteraction = false; //���� ��ȣ�ۿ� ������ üũ

        //�ش� ���⸦ ���ϰ� �ִ��� Ȯ�ο�
        public static bool getHandGun = false;
        public static bool getAKM = false;

        public static bool getFlashlight = false;

        //�رݵ� �ý���
        public static bool openInteraction = false;    //��ȣ�ۿ� �������� ���� �Ǵ�
    }

    public enum ChangeWeapone
    {
        Base,       //�⺻ ����Դϴ�.
        Charging,   //��¡�� ��� �Դϴ�.
        spacetime   //�ð��� ���� ��� �Դϴ�.
    }
}
