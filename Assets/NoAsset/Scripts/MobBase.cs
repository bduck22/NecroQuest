using System.Collections.Generic;
using UnityEngine;


public class MobBase : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<int> Buffs;

    void Start()
    {
        MaxHp = 5;
        Hp = MaxHp;
        Speed = 5;
        Damage = 5;
    }
    void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            collision.enabled = false;
            AttackEffect AE = collision.GetComponent<AttackEffect>();
            HpCh(-(AE.Damage * AE.Weight));
        }
    }
    void HpCh(float damage)
    {
        Hp+=damage;
        if(Hp > MaxHp) Hp = MaxHp;
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
