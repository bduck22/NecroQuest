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

    [SerializeField] private DamageNumberMesh HitPrefab;
    [SerializeField] private DamageNumberMesh HealPrefab;

    public bool locked;

    public float AttackWeight;

    [Header("Skill")]
    public Transform SkillEffect;
    public float SkillCoolTime;
    [SerializeField] private float SkillTime;
    public bool skill;
    public float SkillWeight;

    private UnitManager UM;
    void Start()
    {
        UM = PlayerManager.instance.UnitManager;
        UnitInit();
    }
    public void UnitInit()
    {
        Hp = MaxHp * 5;
        AttackTime = 0;
        SkillTime = SkillCoolTime;
        Interaction.radius = Intersection * 2f;
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
                transform.position = Vector2.MoveTowards(transform.position, TargetWid, Speed * Time.deltaTime);
            }
            else Move = false;
        }
    }
    void Attack()
    {
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
                    Effect.GetComponentInChildren<AttackEffect>().Weight = AttackWeight;
                }
                break;
            case UnitClass.HolyM:
                Effect = Instantiate(AttackEffect.gameObject, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
        }
        if (UnitClass != UnitClass.HolyM)
        {
            Effect.GetComponentInChildren<AttackEffect>().Unit = this;
            Effect.GetComponentInChildren<AttackEffect>().Damage = Damage;
            Effect.GetComponentInChildren<AttackEffect>().Weight = AttackWeight;
        }
        else
        {
            TargetUnit.GetComponent<Unit>().HpChange(-Damage);
        }

        AttackAnimation.SetTrigger("Attack");
    }
    public void Skill()
    {
        if (skill)
        {
            skill = false;
            SkillTime = 0;
            GameObject Effect = null;
            switch (UnitClass)
            {
                case UnitClass.GuardN:
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
                    locked = true;
                    AttackAnimation.SetTrigger("Skill");
                    Effect = Instantiate(SkillEffect.gameObject, AttackAnimation.transform.position, AttackAnimation.transform.rotation);
                    break;
                case UnitClass.ArchM:
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
                    //Effect.transform.GetComponentInChildren<AttackEffect>().Damage = Damage + (Buff[0].Value * 0.05f);
                    break;
                case UnitClass.SpiritM:
                    break;
                case UnitClass.HolyM:
                    break;
            }
            Effect.transform.GetComponentInChildren<AttackEffect>().Unit = this;
            Effect.transform.GetComponentInChildren<AttackEffect>().Damage += Damage;
            Effect.transform.GetComponentInChildren<AttackEffect>().Weight = SkillWeight;
            Effect.transform.GetComponentInChildren<AttackEffect>().Skill = true;
        }
        else
        {
            Debug.Log("스킬 쿨타임중");
        }
    }

    public void HpChange(float Damage)
    {
        if (Hp > MaxHp * 5) Hp = MaxHp * 5;
        if (Damage > 0)
        {
            if (UnitClass == UnitClass.GuardN)
            {
                Damage /= 2f;
            }
            HitPrefab.Spawn(transform.position, Damage);
        }
        else
        {
            HealPrefab.Spawn(transform.position, -Damage);
        }
        Hp -= Damage;
    }
}
