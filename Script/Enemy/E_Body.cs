using UnityEngine;

public class E_TestBody : MonoBehaviour, IDamagebal
{
    //�� ĳ������ �ھ� ����
    //���� ����
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
        if (Damage <= defense)//���ݷ��� ���� ���� ������� ���� 1�� �ش�.
        {
            if(Damage < defense - (Damage + 1))
            {
                Debug.Log("���� ��");
            }
            else Hp -= 1;
        }
        else//���ݷ��� ���� ���� ������� ���� 
            Hp += defense - (int)Damage;

    }
}
