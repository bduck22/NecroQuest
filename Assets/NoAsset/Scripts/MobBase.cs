using System.Collections;
using System.Collections.Generic;
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

    public float SpawnSpeed;

    public float AttackWeight;
    [Header("Type")]
    public MobType Type;
    public UnitTargetType MobTargetType;
    public Attack_Type AttackType;

    [Header("etc")]
    public bool Lock;

    public float LodingTime;

    public Unit Target;

    private float time;

    public Transform AttackOb;

    //[SerializeField] private bool moving;

    public SpriteRenderer[] HitImage;

    bool hit = true;
    //private NavMeshAgent agent;

    Transform Arm;

    float AttackTime = 0;

    Rigidbody2D rigidbody;

    public Vector3 targetP;

    [SerializeField] bool attack;

    public SpawnManager spawnManager;

    Animator ani;

    public bool Ghosted;

    [SerializeField] private Transform AttackPostion;

    public bool spawnlock;

    public void MobInit()
    {
        Lock = false;
        spawnlock = false;
        spawntime = 0;
        goaled = false;
        attack = false;
        hit = true;
        foreach (SpriteRenderer image in HitImage)
        {
            image.color = Color.white;
        }
        MobStat stat = Data.MobData[Type];
        MaxHp = stat.Hp;
        Speed = stat.Speed;
        Damage = stat.Damage;
        AttackSpeed = stat.AttackSpeed;
        Intersection = stat.Intersection;
        MaxHp += GameManager.instance.Diffi * 0.5f;
        Damage += GameManager.instance.Diffi * 0.5f;
        if (spawnManager.Boss)
        {
            if (spawnManager.Boss.Type == MobType.Necro && Type != MobType.Necro)
            {
                Damage *= 1.5f;
                MaxHp *= 1.5f;
            }
        }
        Hp = MaxHp * 20;
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
    }
    [SerializeField] bool goaled;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime && Type != MobType.Ghost && !Lock)
        {
            time = 0;
            TargetLoad();
        }
        if (Target && !Lock)
        {
            if (Target.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (Arm && Type == MobType.Zombie)
                {
                    switch (Type)
                    {
                        case MobType.Zombie:
                            Arm.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                        case MobType.Ghoul:
                            Arm.GetChild(0).localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z + 30);
                            Arm.GetChild(1).localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                    }

                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (Arm)
                {
                    switch (Type)
                    {
                        case MobType.Zombie:
                            Arm.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                        case MobType.Ghoul:
                            Arm.GetChild(0).localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z + 30);
                            Arm.GetChild(1).localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.down, Target.transform.position - transform.position).eulerAngles.z);
                            break;
                    }
                }
            }
            if (AttackType == Attack_Type.ShotRange )
            {
                if (Ghosted)
                {

                    if (goaled)
                    {
                        goaled = true;
                        transform.position += targetP * Speed * 1.5f *Time.deltaTime;
                    }
                    else
                    {
                        transform.position += (Target.transform.position - transform.position).normalized * Speed * 1.5f * Time.deltaTime;
                    }
                    if (Type == MobType.Ghost && Vector2.Distance(transform.position, Target.transform.position) > Intersection && goaled)
                    {
                        goaled = false;
                        TargetLoad();
                    }
                }
                else rigidbody.linearVelocity = (targetP - transform.position).normalized * 1.5f * Speed;
            }
            else if (AttackType == Attack_Type.longRange )
            {
                if (Vector2.Distance(transform.position, Target.transform.position) > Intersection + 2)
                {
                    if (Type == MobType.Dullahan)
                    {
                        ani.SetBool("Walk", true);
                    }
                    if (Ghosted)
                    {
                        transform.position += (Target.transform.position - transform.position).normalized * Speed * 1.5f * Time.deltaTime;
                    }
                    else
                    {
                        rigidbody.linearVelocity = (targetP - transform.position).normalized * 1.5f * Speed;
                    }
                    attack = false;
                }
                else
                {
                    if (Type == MobType.Dullahan)
                    {
                        ani.SetBool("Walk", false);
                    }
                    rigidbody.linearVelocity = Vector2.zero;
                    if (Type == MobType.Necro)
                    {
                        if (spawnlock)
                        {
                            attack = true;
                        }
                    }
                    else
                    {
                        attack = true;
                    }
                }
            }
            if (AttackType == Attack_Type.longRange && AttackTime == 1 && attack&&!Lock)
            {
                GameObject Attack=null;
                AttackEffect AE=null;
                if (AttackOb)
                {
                    Attack = Instantiate(AttackOb.gameObject);
                    Attack.transform.position = AttackPostion.position;
                    AE = Attack.GetComponentInChildren<AttackEffect>();
                }

                switch (Type)
                {
                    case MobType.Skull:
                        Attack.GetComponent<TargetMove>().Target = Target.transform;
                        Attack.GetComponent<TargetMove>().Speed = 8f;
                        break;
                    case MobType.Ghoul:
                        AE.Range = true;
                        break;
                    case MobType.Necro:
                        AE = AttackPostion.GetComponent<AttackEffect>();
                        AE.Damage = MaxHp * 20/30;
                        break;
                    case MobType.Dullahan:
                        AE = AttackPostion.GetComponent<AttackEffect>();
                        break;
                }
                AE.Mob = this;
                AE.Damage += Damage;
                AE.Weight = AttackWeight;
                AttackTime = 0;
                ani.SetTrigger("Attack");
            }
        }
        if (spawntime == SpawnSpeed && !spawnlock)
        {
            spawntime = 0;
            switch (Type)
            {
                case MobType.Dullahan:
                    spawnManager.Spawn(2, 5);
                    break;
                case MobType.Necro:
                    spawnManager.Spawn(Random.Range(0, 5), 3);
                    break;
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
        if (Type == MobType.Dullahan || Type == MobType.Necro && !Lock)
        {
            if (spawntime < SpawnSpeed)
            {
                if (!spawnlock)
                {
                    spawntime += Time.deltaTime;
                }
            }
            else
            {
                spawntime = SpawnSpeed;
            }
        }
    }
    public float spawntime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Lock)
        {
            if (collision.CompareTag("Attack"))
            {
                //collision.enabled = false;
                AttackEffect AE = collision.GetComponent<AttackEffect>();
                AE.Unit.SetDamages += AE.Damage * AE.Weight;
                if (PlayerManager.instance.QuestType == QuestType.Attack)
                {
                    PlayerManager.instance.QuestValue-= AE.Damage * AE.Weight;
                }
                if (AE.Unit.UnitClass == UnitClass.DragonN)
                {
                    AE.Unit.HpChange(-(AE.Damage * AE.Weight)*0.3f);
                }
                if (AE.Unit.UnitClass == UnitClass.GuardN)
                {
                    if (AE.Skill)
                    {
                        Buff.Add(new Buff(Buff_Type.Provo, AE.Unit.transform, 3));
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
                if (Type == MobType.Ghost) goaled = true;
                if (Type == MobType.Ghost && !collision.transform.parent.GetComponent<Unit>().Invin)
                {
                    collision.transform.parent.GetComponent<Unit>().HpChange(Damage * AttackWeight);
                    collision.GetComponent<UnitHit>().Hit();
                }
            }
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("HitBox") || collision.CompareTag("Mob"))
    //    {
    //        Debug.Log(collision.name);
    //        if (collision.CompareTag("HitBox"))
    //        {
    //            if (collision.GetComponent<Unit>().Hp <= 0) transform.parent.GetComponent<MobBase>().HpCh(10);
    //        }
    //        if (collision.CompareTag("Mob"))
    //        {
    //            if (collision.GetComponent<MobBase>().Hp <= 0) transform.parent.GetComponent<MobBase>().HpCh(10);
    //        }
    //    }
    //}
    IEnumerator HitAni()
    {
        if (!hit) yield return null;
        else hit = false;
        foreach (SpriteRenderer image in HitImage)
        {
            image.color = Color.red;
        }
        yield return new WaitForSeconds(1.5f / 3f);
        foreach (SpriteRenderer image in HitImage)
        {
            image.color = Color.white;
        }
        yield return new WaitForSeconds(0.75f / 3f);
        hit = true;
    }
    public void DullahanHeal(Transform position)
    {
        if (Vector2.Distance(transform.position, position.position) < 10)
        {
            HpCh(50);
        }
    }
    public void HpCh(float damage)
    {
        if (gameObject.activeSelf && !Lock)
        {
            Hp += damage;
            if (Hp > MaxHp * 20) Hp = MaxHp * 20;
            if (damage < 0)
            {
                if (gameObject.activeSelf) StartCoroutine(HitAni());

                PlayerManager.instance.Deal(transform, -damage);
            }
            else
            {
                PlayerManager.instance.Heal(transform, damage);
            }
            if (Hp <= 0)
            {
                if (Type == MobType.Necro && !spawnlock)
                {
                    Lock = true;
                    int C = 0;
                    spawnlock = true;
                    spawnManager.StopAllCoroutines();
                    spawnManager.waving = false;
                    foreach (MobBase mob in spawnManager.Mobs)
                    {
                        if (mob != this)
                        {
                            if (mob.gameObject.activeSelf)
                            {
                                mob.HpCh(-mob.Hp);
                                C++;
                            }
                        }
                    }
                    MaxHp += C / 5f;
                    PlayerManager.instance.Heal(transform, MaxHp * 20);
                    Hp = MaxHp * 20;
                    ani.SetTrigger("P2");
                }
                else
                {
                    PlayerManager.instance.killcount++;
                    if (PlayerManager.instance.QuestType == QuestType.Monster)
                    {
                        PlayerManager.instance.QuestValue--;
                    }
                    spawnManager.MobCount--;
                    if (spawnManager.Boss)
                    {
                        if (spawnManager.Boss.Type == MobType.Dullahan)
                        {
                            spawnManager.Boss.DullahanHeal(transform);
                        }
                    }
                    PlayerManager.instance.UnitsMoral(5);
                    PlayerManager.instance.CreateGold(100, transform.position);
                    if (Type == MobType.Ghost)
                    {
                        transform.GetComponentInChildren<TrailRenderer>().enabled = false;
                    }
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void TargetLoad()
    {
        foreach (Buff b in Buff)
        {
            if (b.Type == Buff_Type.Provo)
            {
                targetP = Target.transform.position;
                if (Ghosted)
                {
                    targetP = (targetP - transform.position).normalized;
                }
                return;
            }
        }
        Target = null;
        foreach (Unit u in PlayerManager.instance.Units)
        {
            if (u!=null&&u.gameObject.activeSelf)
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
    public void LockF()
    {
        Lock = false;
    }
}
