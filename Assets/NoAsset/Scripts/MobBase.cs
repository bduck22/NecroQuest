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

    public float AttackSpeed;
    public float Intersection;

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

    [SerializeField] private DamageNumberMesh HitPrefab;
    [SerializeField] private DamageNumberMesh HealPrefab;

    private SpriteRenderer HitImage;

    bool hit=true;
    //private NavMeshAgent agent;

    Transform Arm;

    float AttackTime=0;

    Rigidbody2D rigidbody;

    Vector3 targetP;
    public void MobInit()
    {
        hit = true;
        HitImage.color = Color.white;
        //moving = true;
        MaxHp = 5;
        Hp = MaxHp*5;
        Speed = 1;
        Damage = 1;
        Target = null;
        AttackTime = 0;
        Buff.Clear();
        TargetLoad();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Arm = transform.GetChild(1);
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
        if (Target)//moving&&
        {
            //agent.SetDestination(Target.transform.position);
            if (Target.transform.position.x < transform.position.x)
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
                rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
                //transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            }else if(AttackType == Attack_Type.longRange)
            {
                if(Vector2.Distance(transform.position, Target.transform.position) > Intersection+2){
                    rigidbody.linearVelocity = (targetP - transform.position).normalized * Speed;
                }
            }
            if (AttackType == Attack_Type.longRange && AttackTime == 1)
            {
                AttackTime = 0;
                GameObject Bone = Instantiate(AttackOb.gameObject);
                Bone.transform.position = transform.position;
                Bone.GetComponent<TargetMove>().Target = Target.transform;
                Bone.GetComponent<TargetMove>().Speed = 3f;
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
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Unit"))
    //    {
    //        if (collision.transform.GetComponent<Unit>() == Target)
    //        {
    //            moving = false;
    //        }
    //    }
    //    if (collision.transform.CompareTag("Mob") && moving)
    //    {
    //        if (!collision.transform.GetComponent<MobBase>().moving)
    //        {
    //            moving = false;
    //        }
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Unit"))
    //    {
    //        if (collision.transform.GetComponent<Unit>() == Target)
    //        {
    //            moving = true;
    //        }
    //    }
    //    if (collision.transform.CompareTag("Mob"))
    //    {
    //        if (!collision.transform.GetComponent<MobBase>().moving)
    //        {
    //            moving = true;
    //        }
    //    }
    //}
    void HpCh(float damage)
    {
        if (gameObject.activeSelf)
        {
            Hp += damage;
            if (Hp > MaxHp * 5) Hp = MaxHp * 5;
            if (Hp <= 0)
            {
                SpawnManager.MobCount--;
                gameObject.SetActive(false);
            }
            if (damage < 0)
            {
                if (gameObject.activeSelf) StartCoroutine(HitAni());
                HitPrefab.Spawn(transform.position, -damage);
            }
            else
            {
                HealPrefab.Spawn(transform.position, damage);
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
        }
    }
}
