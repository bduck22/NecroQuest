using DamageNumbersPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBase : MonoBehaviour
{
    public Mob Mob;

    public float LodingTime;

    public Unit Target;

    private float time;

    [SerializeField] private bool moving;

    [SerializeField] private DamageNumberMesh HitPrefab;
    [SerializeField] private DamageNumberMesh HealPrefab;

    //private NavMeshAgent agent;
    public void MobInit()
    {
        moving = true;
        Mob.MaxHp = 5;
        Mob.Hp = Mob.MaxHp;
        Mob.Speed = 5;
        Mob.Damage = 1;
        Target = null;
        TargetLoad();
    }

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        MobInit();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime)
        {
            time = 0;
            TargetLoad();
        }
        if (moving&&Target)
        {
            //agent.SetDestination(Target.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Mob.Speed * 0.15f * Time.deltaTime);
        }
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Unit"))
        {
            if (collision.transform.GetComponent<Unit>() == Target)
            {
                moving = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Unit"))
        {
            if (collision.transform.GetComponent<Unit>() == Target)
            {
                moving = true;
            }
        }
    }
    void HpCh(float damage)
    {
        if(damage < 0)
        {
            HitPrefab.Spawn(transform.position, -damage);
        }
        else
        {
            HealPrefab.Spawn(transform.position, damage);
        }
        Mob.Hp += damage;
        if (Mob.Hp > Mob.MaxHp) Mob.Hp = Mob.MaxHp;
        if (Mob.Hp <= 0)
        {
            SpawnManager.MobCount--;
            gameObject.SetActive(false);
        }
    }

    public void TargetLoad()
    {
        Target = null;
        foreach (Unit u in PlayerManager.instance.Units)
        {
            if (u.gameObject.activeSelf)
            {
                if (!Target)
                {
                    Target = u;
                }
                else
                {
                    switch (Mob.MobTargetType)
                    {
                        case UnitTargetType.LowHp:
                            if (Target.UB.Hp > u.UB.Hp)
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
