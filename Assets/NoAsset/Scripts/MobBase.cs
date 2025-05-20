using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBase : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<Buff> Buff;

    [Header("Type")]
    public MobType Type;
    public UnitTargetType MobTargetType;

    [Header("etc")]
    public float LodingTime;

    public Unit Target;

    private float time;

    [SerializeField] private bool moving;

    [SerializeField] private DamageNumberMesh HitPrefab;
    [SerializeField] private DamageNumberMesh HealPrefab;

    private SpriteRenderer HitImage;

    bool hit=true;
    //private NavMeshAgent agent;
    public void MobInit()
    {
        moving = true;
        MaxHp = 5;
        Hp = MaxHp*5;
        Speed = 5;
        Damage = 1;
        Target = null;
        Buff.Clear();
        TargetLoad();
    }

    void Start()
    {
        HitImage = GetComponent<SpriteRenderer>();
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
            if (Target.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * 0.15f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            //collision.enabled = false;
            AttackEffect AE = collision.GetComponent<AttackEffect>();
            if (AE.Unit.UnitClass == UnitClass.DragonN)
            {

            }
            if (AE.Unit.UnitClass == UnitClass.GuardN)
            {
                if (AE.Skill)
                {
                    Buff.Add(new Buff(Buff_Type.Provo, AE.Unit.transform, 5));
                }
            }
            HpCh(-(AE.Damage * AE.Weight));
        }
    }
    IEnumerator HitAni()
    {
        if (!hit) yield return null;
        else hit = false;
        HitImage.color = Color.red;
        yield return new WaitForSeconds(2 / 3f);
        HitImage.color = Color.white;
        yield return new WaitForSeconds(1 / 3f);
        hit = true;
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
            StartCoroutine(HitAni());
            HitPrefab.Spawn(transform.position, -damage);
        }
        else
        {
            HealPrefab.Spawn(transform.position, damage);
        }
        Hp += damage;
        if (Hp > MaxHp * 5) Hp = MaxHp * 5;
        if (Hp <= 0)
        {
            SpawnManager.MobCount--;
            gameObject.SetActive(false);
        }
    }

    public void TargetLoad()
    {
        foreach (Buff b in Buff)
        {
            if (b.Type == Buff_Type.Provo)
            {
                return;
            }
        }
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
