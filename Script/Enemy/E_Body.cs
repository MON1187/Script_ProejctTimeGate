using UnityEngine;

public class E_TestBody : MonoBehaviour, IDamagebal
{
    //적 캐릭터의 코어 역할
    //스텟 관리
    //?

    public int Hp;
    public int MaxHp = 10;
    public int defense = 1;

    private void Start()
    {
        Hp = MaxHp;    
    }
    private void Update()
    {
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damaged(float Damage)
    {
        if (Damage <= defense)//공격력이 방어력 보다 낮을경우 고정 1을 준다.
        {
            if(Damage < defense - (Damage + 1))
            {
                Debug.Log("허접 쉑");
            }
            else Hp -= 1;
        }
        else//공격력이 방어력 보다 높을경우 실행 
            Hp += defense - (int)Damage;

    }
}
