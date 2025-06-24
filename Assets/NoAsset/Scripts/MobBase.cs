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

    [SerializeField] private Transform AttackPostion;
    public void MobInit()
    {
        goaled = false;
        attack = false;
        hit = true;
        HitImage.color = Color.white;
        MobStat stat = Data.MobData[Type];
        MaxHp = stat.Hp;
        Hp = MaxHp * 20;
        Speed = stat.Speed;
        Damage = stat.Damage;
        AttackSpeed = stat.AttackSpeed;
        Intersection = stat.Intersection;
        AttackWeight = 1;
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
        if (!Ghosted)
        {
            Arm = transform.GetChild(1);
        }
        HitImage = GetComponent<SpriteRenderer>();
    }
    [SerializeField] bool goaled;
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
            if (Target.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (Arm&&Type==MobType.Zombie)
                {
                    switch (Type)
                    {
                        case MobType.Zombie:
                            Arm.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                        case MobType.Ghoul:
                            Arm.GetChild(0).localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z+30);
                            Arm.GetChild(1).localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                    }
                    
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (Arm )
                {
                    switch (Type)
                    {
                        case MobType.Zombie:
                            Arm.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                        case MobType.Ghoul:
                            Arm.GetChild(0).localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z+30);
                            Arm.GetChild(1).localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                    }
                }
            }
            if (AttackType == Attack_Type.ShotRange)
            {
                if (Ghosted)
                {
                    
                    if(goaled)
                    {
                        goaled = true;
                        transform.position += targetP * Speed * Time.deltaTime;
                    }
                    else
                    {
                        transform.position += (Target.transform.position - transform.position).normalized * Speed * Time.deltaTime;
                    }
                    if (Type == MobType.Ghost && Vector2.Distance(transform.position, Target.transform.position) > Intersection && goaled)
                    {
                        goaled = false;
                        TargetLoad();
                    }
                }
                else rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
            }
            else if (AttackType == Attack_Type.longRange)
            {
                if (Vector2.Distance(transform.position, Target.transform.position) > Intersection + 2)
                {
                    if (Ghosted)
                    {
                        transform.position += (Target.transform.position - transform.position).normalized * Speed * Time.deltaTime;
                    }
                    else
                    {
                        rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
                    }
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
                ani.SetTrigger("Attack");
                GameObject Attack = Instantiate(AttackOb.gameObject);
                Attack.transform.position = AttackPostion.position;
                AttackEffect AE = Attack.GetComponentInChildren<AttackEffect>();
                switch (Type)
                {
                    case MobType.Skull:
                        Attack.GetComponent<TargetMove>().Target = Target.transform;
                        Attack.GetComponent<TargetMove>().Speed = 8f;
                        break;
                    case MobType.Ghoul:
                        AE.Range = true;
                        break;
                }
                AE.Mob = this;
                AE.Damage = Damage;
                AE.Weight = AttackWeight;
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
                AE.Unit.HpChange(-(AE.Damage * AE.Weight));
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
        if (collision.CompareTag("HitBox"))
        {
            if (Type == MobType.Shade)
            {
                HpCh(-Hp);
                AttackEffect Ob = Instantiate(AttackOb, transform.position, Quaternion.identity).GetComponent<AttackEffect>();
                Ob.Mob = this;
                Ob.Damage = Damage;
                Ob.Weight = AttackWeight;
                Ob.Range = true;
            }
            if(Type==MobType.Ghost) goaled = true;
            if (Type == MobType.Ghost&&!collision.transform.parent.GetComponent<Unit>().Invin)
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
        yield return new WaitForSeconds(1.5f / 3f);
        HitImage.color = Color.white;
        yield return new WaitForSeconds(0.75f / 3f);
        hit = true;
    }
    public void HpCh(float damage)
    {
        if (gameObject.activeSelf)
        {
            Hp += damage;
            if (Hp > MaxHp * 20) Hp = MaxHp * 20;
            if (Hp <= 0)
            {
                spawnManager.MobCount--;
                PlayerManager.instance.UnitsMoral(5);
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
        if (Target)
        {
            targetP = Target.transform.position;
            if (Ghosted)
            {
                targetP = (targetP - transform.position).normalized;
            }
        }
    }
}
