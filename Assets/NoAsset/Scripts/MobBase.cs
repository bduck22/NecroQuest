using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float Damage;
    public List<Buff> Buff;

    public float AttackSpeed;
    public float Intersection;

    public float AttackWeight;
    [Header("Type")]
    public MobType Type;
    public UnitTargetType MobTargetType;
    public Attack_Type AttackType;

    [Header("etc")]
    public float LodingTime;

    public Unit Target;

    private float time;

    public Transform AttackOb;

    //[SerializeField] private bool moving;

    private SpriteRenderer HitImage;

    bool hit = true;
    //private NavMeshAgent agent;

    Transform Arm;

    float AttackTime = 0;

    Rigidbody2D rigidbody;

    Vector3 targetP;

    [SerializeField] bool attack;

    public SpawnManager spawnManager;

    Animator ani;

    public bool Ghosted;
    public void MobInit()
    {
        attack = false;
        hit = true;
        HitImage.color = Color.white;
        MobStat stat = Data.MobData[Type];
        MaxHp = stat.Hp;
        Hp = MaxHp * 3;
        Speed = stat.Speed;
        Damage = stat.Damage;
        AttackSpeed = stat.AttackSpeed;
        Intersection = stat.Intersection;
        Target = null;
        AttackTime = 0;
        Buff.Clear();
        if (Type == MobType.Ghost)
        {
            transform.GetComponentInChildren<TrailRenderer>().enabled = true;
        }
        TargetLoad();
    }

    void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        Arm = transform.GetChild(1);
        HitImage = GetComponent<SpriteRenderer>();
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime && Type != MobType.Ghost)
        {
            time = 0;
            TargetLoad();
        }
        if (Target)
        {
            if (targetP.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Arm.transform.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Arm.transform.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
            }
            if (AttackType == Attack_Type.ShotRange)
            {
                if (Ghosted)
                {
                    transform.position += targetP * Speed * Time.deltaTime;
                    if (Type == MobType.Ghost && Vector2.Distance(transform.position, targetP) > Intersection)
                    {
                        TargetLoad();
                    }
                }
                else rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
            }
            else if (AttackType == Attack_Type.longRange)
            {
                if (Vector2.Distance(transform.position, targetP) > Intersection + 2)
                {
                    rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
                    attack = false;
                }
                else
                {
                    rigidbody.linearVelocity = Vector2.zero;
                    attack = true;
                }
            }
            if (AttackType == Attack_Type.longRange && AttackTime == 1 && attack)
            {
                GameObject Attack = null;
                switch (Type)
                {
                    case MobType.Skull:
                        ani.SetTrigger("Attack");
                        Attack = Instantiate(AttackOb.gameObject);
                        Attack.transform.position = transform.GetChild(1).GetChild(0).GetChild(1).position;
                        Attack.GetComponent<TargetMove>().Target = Target.transform;
                        Attack.GetComponent<TargetMove>().Speed = 8f;
                        break;
                }
                Attack.GetComponentInChildren<AttackEffect>().Mob = this;
                Attack.GetComponentInChildren<AttackEffect>().Damage = Damage;
                Attack.GetComponentInChildren<AttackEffect>().Weight = AttackWeight;
                AttackTime = 0;
            }
        }
        if (AttackTime < 1)
        {
            AttackTime += AttackSpeed * Time.deltaTime;
        }
        else
        {
            AttackTime = 1;
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
        if (Type == MobType.Ghost&&collision.CompareTag("HitBox"))
        {
            if (!collision.transform.parent.GetComponent<Unit>().Invin)
            {
                collision.transform.parent.GetComponent<Unit>().HpChange(Damage* AttackWeight);
                collision.GetComponent<UnitHit>().Hit();
            }
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
    void HpCh(float damage)
    {
        if (gameObject.activeSelf)
        {
            Hp += damage;
            if (Hp > MaxHp * 3) Hp = MaxHp * 3;
            if (Hp <= 0)
            {
                spawnManager.MobCount--;
                PlayerManager.instance.CreateGold(100, transform.position);
                if (Type == MobType.Ghost)
                {
                    transform.GetComponentInChildren<TrailRenderer>().enabled=false;
                }
                gameObject.SetActive(false);
            }
            if (damage < 0)
            {
                if (gameObject.activeSelf) StartCoroutine(HitAni());

                PlayerManager.instance.Deal(transform, -damage);
            }
            else
            {
                PlayerManager.instance.Heal(transform, damage);
            }
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

                            if (Vector2.Distance(transform.position, targetP) < Vector2.Distance(transform.position, u.transform.position))
                            {
                                Target = u;
                            }
                            break;
                        case UnitTargetType.Close:

                            if (Vector2.Distance(transform.position, targetP) > Vector2.Distance(transform.position, u.transform.position))
                            {
                                Target = u;
                            }
                            break;
                    }
                }
            }
        }
        if (Target)
        {
            targetP = Target.transform.position;
            if (Type == MobType.Ghost)
            {
                targetP = (targetP - transform.position).normalized;
            }
        }
    }
}
