using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 TargetWid;
    public bool Move;

    [Header("Stats")]
    public float Speed;
    public float AttackSpeed;
    public float Hp;
    public float MaxHp;
    public float Damage;
    public float Intersection;
    public float Moral;
    public List<Buff> Buff;

    [Header("Type")]
    public UnitClass UnitClass;
    public UnitTargetType UnitTargetType;

    [Header("Invin")]
    public bool Invin;
    public float InvinTime;

    [Header("Etc")]
    public Transform TargetUnit;

    [SerializeField] private float AttackTime;
    public Animator AttackAnimation;

    public Transform AttackEffect;
    [SerializeField] CircleCollider2D Interaction;

    public bool locked;

    public float AttackWeight;

    private Rigidbody2D rigidbody;

    [Header("Skill")]
    public Transform SkillEffect;
    public float SkillCoolTime;
    public float SkillTime;
    public bool skill;
    public float SkillWeight;

    private UnitManager UM;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        UM = PlayerManager.instance.UnitManager;
        UnitInit();
    }
    public void UnitInit()
    {
        Hp = MaxHp * 5;
        AttackTime = 0;
        SkillTime = SkillCoolTime;
        Interaction.radius = Intersection + 2f;
        TargetUnit = null;
        Invin = false;
        Move = false;
        switch (UnitClass)
        {
            case UnitClass.DragonN:
                break;
            case UnitClass.Fighter:
                break;
            case UnitClass.ArchM:
                foreach (Buff b in Buff)
                {
                    if (b.Type == Buff_Type.Charge)
                    {
                        return;
                    }
                }
                Buff.Add(new Buff(Buff_Type.Charge, 0, 0));
                break;
        }
    }
    public void maxHpUp(float value)
    {
        Hp += value*5;
        MaxHp += value;
    }
    public void InteractionUp(float value)
    {
        Intersection += value;
        Interaction.radius = Intersection + 2f;
    }
    public void AllStatUp(float value)
    {
        maxHpUp(value);
        Damage += value;
        Speed += value;
        AttackSpeed += value;
    }
    void Update()
    {

        if (TargetUnit)
        {
            if (!TargetUnit.gameObject.activeSelf)
            {
                TargetUnit = null;
            }
        }

        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }

        if (AttackTime < 1)
        {
            if (!locked)
            {
                AttackTime += AttackSpeed * Time.deltaTime;
            }
        }
        else
        {
            AttackTime = 1;
        }

        if (SkillTime < SkillCoolTime)
        {
            if (!locked)
            {
                SkillTime += 1 * Time.deltaTime;
            }
        }
        else if (!skill)
        {
            skill = true;
            SkillTime = SkillCoolTime;
        }

        if (TargetUnit && AttackTime == 1 && !locked)
        {
            AttackTime = 0;
            Attack();
        }

        if (Move)
        {
            if ((Vector2)transform.position != TargetWid)
            {
                if (AttackTime == 1)
                {
                    AttackAnimation.transform.localRotation = Quaternion.identity;
                    if (TargetWid.x >= transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                rigidbody.linearVelocity = ((Vector3)TargetWid - transform.position).normalized * Speed;
                //transform.position = Vector2.MoveTowards(transform.position, TargetWid, Speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, TargetWid) <0.2f)
            {
                Move = false;
            }
        }
        else
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
    }
    void Attack()
    {
        float attackweight = AttackWeight;
        if (Moral <= 50)
        {
            attackweight *= 0.7f;
        }
        else if (Moral > 200)
        {
            attackweight *= 1.3f;
        }
        AttackAnimation.SetFloat("AttackSpeed", AttackSpeed);
        if (TargetUnit.transform.position.x >= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.right, TargetUnit.transform.position - transform.position).eulerAngles.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.left, TargetUnit.transform.position - transform.position).eulerAngles.z);
        }

        GameObject Effect = null;
        switch (UnitClass)
        {
            case UnitClass.GuardN:
                Effect = Instantiate(AttackEffect.gameObject, AttackAnimation.transform);
                break;
            case UnitClass.DragonN:
                break;
            case UnitClass.HolyN:
                break;
            case UnitClass.Fighter:
                break;
            case UnitClass.Berserker:
                break;
            case UnitClass.Archer:
                Effect = Instantiate(AttackEffect.gameObject, (AttackAnimation.transform.position + TargetUnit.position) / 2, AttackAnimation.transform.rotation);
                Effect.transform.localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f, 1.25f, 0.5f);
                Effect.transform.GetChild(0).localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f, 1.25f, 0.5f);
                break;
            case UnitClass.ArchM:
                Buff[0].Value++;
                Effect = Instantiate(AttackEffect.gameObject, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
            case UnitClass.SpiritM:
                for (int i = 0; i < 3; i++)
                {
                    Effect = Instantiate(AttackEffect.gameObject, AttackAnimation.transform.position, AttackAnimation.transform.localRotation);
                    Effect.GetComponentInChildren<SpiritMove>().Target = TargetUnit.transform;
                    Effect.GetComponentInChildren<AttackEffect>().Unit = this;
                    Effect.GetComponentInChildren<AttackEffect>().Damage = Damage;
                    Effect.GetComponentInChildren<AttackEffect>().Weight = attackweight;
                }
                break;
            case UnitClass.HolyM:
                TargetUnit.GetComponent<Unit>().HpChange(-Damage);
                break;
        }
        if (UnitClass != UnitClass.HolyM)
        {
            Effect.GetComponentInChildren<AttackEffect>().Unit = this;
            Effect.GetComponentInChildren<AttackEffect>().Damage = Damage;
            Effect.GetComponentInChildren<AttackEffect>().Weight = attackweight;
        }

        AttackAnimation.SetTrigger("Attack");
    }
    public void Skill()
    {
        if (skill)
        {
            float skillweight = SkillWeight;
            if (Moral <= 50)
            {
                skillweight *= 0.7f;
            }
            else if(Moral > 200)
            {
                skillweight *= 1.3f;
            }
            bool IsDamaged = false;
            skill = false;
            SkillTime = 0;
            GameObject Effect = null;
            switch (UnitClass)
            {
                case UnitClass.GuardN:
                    IsDamaged = true;
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, transform.position, transform.rotation);
                    break;
                case UnitClass.DragonN:
                    break;
                case UnitClass.HolyN:
                    break;
                case UnitClass.Fighter:
                    break;
                case UnitClass.Berserker:
                    break;
                case UnitClass.Archer:
                    IsDamaged = true;
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, AttackAnimation.transform.position, AttackAnimation.transform.rotation);
                    break;
                case UnitClass.ArchM:
                    IsDamaged = true;
                    locked = true;
                    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector2.right, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).eulerAngles.z);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        AttackAnimation.transform.localRotation = Quaternion.Euler(0, 0, -Quaternion.FromToRotation(Vector2.left, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).eulerAngles.z);
                    }
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y, 0);
                    Effect.transform.localScale = new Vector3((Buff[0].Value * 0.02f) + 1f, (Buff[0].Value * 0.02f) + 1f, 1);
                    Effect = Effect.transform.GetChild(0).gameObject;
                    Effect.transform.GetComponentInChildren<AttackEffect>().Damage = (Buff[0].Value * 0.05f);
                    break;
                case UnitClass.SpiritM:
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Unit Skill_Target=null;
                    foreach(Unit t in PlayerManager.instance.Units)
                    {
                        if (t.gameObject.activeSelf)
                        {
                            if (!Skill_Target)
                            {
                                Skill_Target = t;
                            }
                            else
                            {
                                if (Skill_Target.Hp > t.Hp)
                                {
                                    Skill_Target = t;
                                }
                            }
                        }
                    }
                    AttackAnimation.SetTrigger("Skill");
                    Skill_Target.HpChange(skillweight * -Damage);
                    Skill_Target.Buff.Add(new Buff(Buff_Type.Spirit, 1, 5));
                    break;
                case UnitClass.HolyM:
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y, 0);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 2.8f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.CompareTag("HitBox"))
                        {
                            col.transform.parent.GetComponent<Unit>().HpChange(-Damage*skillweight);
                        }
                    }
                    break;
            }
            if (IsDamaged)
            {
                Effect.transform.GetComponentInChildren<AttackEffect>().Unit = this;
                Effect.transform.GetComponentInChildren<AttackEffect>().Damage += Damage;
                Effect.transform.GetComponentInChildren<AttackEffect>().Weight = skillweight;
                Effect.transform.GetComponentInChildren<AttackEffect>().Skill = true;
            }
        }
        else
        {
            Debug.Log("스킬 쿨타임중");
        }
    }

    public void HpChange(float Damage)
    {
        if (Damage > 0)
        {
            if (UnitClass == UnitClass.GuardN)
            {
                Damage /= 2f;
            }
            if (Moral <= 50)
            {
                Damage *= 1.3f;
            }
            else if (Moral > 200)
            {
                Damage *= 0.7f;
            }
            PlayerManager.instance.Deal(transform, Damage);
        }
        else
        {
            if (Moral <= 50)
            {
                Damage *= 0.7f;
            }
            else if (Moral > 200)
            {
                Damage *= 1.3f;
            }
            PlayerManager.instance.Heal(transform, -Damage);
        }
        Hp -= Damage;
        if (Hp > MaxHp * 5) Hp = MaxHp * 5;
    }
}
