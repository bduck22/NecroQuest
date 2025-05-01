using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<int> Buffs;

    public float LodingTime;

    public UnitTargetType MobTargetType;
    public Unit Target;


    private float time;
    void Start()
    {
        MaxHp = 5;
        Hp = MaxHp;
        Speed = 5;
        Damage = 5;
        TargetLoad();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime)
        {
            time = 0;
            TargetLoad();
        }
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * 0.15f * Time.deltaTime);
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
        Hp += damage;
        if (Hp > MaxHp) Hp = MaxHp;
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TargetLoad()
    {
        foreach (Unit u in PlayerManager.instance.Units)
        {
            if (u.enabled)
            {
                if (!Target)
                {
                    Target = u;
                }
                else
                {
                    switch (MobTargetType)
                    {
                        case UnitTargetType.LowHp:
                            if (Target.Hp > u.Hp)
                            {
                                Target = u;
                            }
                            break;
                        case UnitTargetType.Far:

                            if (Vector2.Distance(transform.position, Target.transform.position) < Vector2.Distance(transform.position, u.transform.position))
                            {
                                Target = u;
                            }
                            break;
                        case UnitTargetType.Close:

                            if (Vector2.Distance(transform.position, Target.transform.position) > Vector2.Distance(transform.position, u.transform.position))
                            {
                                Target = u;
                            }
                            break;
                    }
                }
            }
        }
    }
}
