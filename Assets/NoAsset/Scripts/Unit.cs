using DamageNumbersPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 TargetWid;
    public bool Move;

    public Unit_Base UB;

    [Header("Invin")]
    public bool Invin;
    public float InvinTime;

    [Header("Etc")]
    public Transform TargetUnit;

    [SerializeField] private float AttackTime;
    public Animator AttackAnimation;

    public GameObject AttackEffect;
    [SerializeField] CircleCollider2D Interaction;

    [SerializeField] private DamageNumberMesh HitPrefab;
    [SerializeField] private DamageNumberMesh HealPrefab;

    [Header("Skill")]
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
        UB.Hp = UB.MaxHp;
        AttackTime = 0;
        SkillTime = SkillCoolTime;
        Interaction.radius = UB.Intersection * 2f;
        Interaction.radius = UB.Intersection + 2f;
        TargetUnit = null;
        Invin =false;
        Move = false;

        switch (UB.UnitClass)
        {
            case UnitClass.GuardN:
                break;
            case UnitClass.DragonN:
                break;
            case UnitClass.Fighter:
                break;
            case UnitClass.ArchM:
                break;
        }
    }
    void Update()
    {
        if(UB.Hp <= 0)
        {
            gameObject.SetActive(false);
        }

        if(AttackTime < 1)
        {
            AttackTime += UB.AttackSpeed * Time.deltaTime;
        }
        else
        {
            AttackTime = 1;
        }
        
        if(SkillTime < SkillCoolTime)
        {
            SkillTime += 1 * Time.deltaTime;
        }
        else if(!skill)
        {
            skill = true;
            SkillTime = SkillCoolTime;
        }

        if (TargetUnit && AttackTime == 1)
        {
            AttackTime = 0;
            Attack();
        }

        if (Move)
        {
            if ((Vector2)transform.position != TargetWid)
            {
                if(AttackTime == 1) {
                    AttackAnimation.transform.localRotation = Quaternion.identity;
                    if (TargetWid.x >= transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                transform.position = Vector2.MoveTowards(transform.position, TargetWid, UB.Speed * Time.deltaTime);
            }
            else Move = false;
        }
    }
    void Attack()
    {
        AttackAnimation.SetFloat("AttackSpeed", UB.AttackSpeed);
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
        switch (UB.UnitClass)
        {
            case UnitClass.GuardN:
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
                Effect = Instantiate(AttackEffect, (AttackAnimation.transform.position + TargetUnit.position) / 2, AttackAnimation.transform.rotation);
                Effect.transform.localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f, 1.25f, 0.5f);
                Effect.transform.GetChild(0).localScale = new Vector3(Vector2.Distance(TargetUnit.position, AttackAnimation.transform.position) / 5f ,1.25f,0.5f);
                Effect = Effect.transform.GetChild(1).gameObject;
                break;
            case UnitClass.ArchM:
                Effect = Instantiate(AttackEffect, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
            case UnitClass.SpiritM:
                break;
            case UnitClass.HolyM:
                Effect = Instantiate(AttackEffect, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
        }
        if (UB.UnitClass != UnitClass.HolyM)
        {
            Effect.GetComponent<AttackEffect>().Unit = this;
            Effect.GetComponent<AttackEffect>().Damage = UB.Damage + UB.PlusDamage;
        }
        else
        {
            TargetUnit.GetComponent<Unit>().HpChange(-UB.Damage);
        }

        AttackAnimation.SetTrigger("Attack");
    }

    public void Skill()
    {
        switch (UB.UnitClass)
        {
            case UnitClass.GuardN:
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
                break;
            case UnitClass.ArchM:
                break;
            case UnitClass.SpiritM:
                break;
            case UnitClass.HolyM:
                break;
        }
    }

    public void HpChange(float Damage)
    {
        if(Damage > 0)
        {
            UB.Hp -= Damage;
            HitPrefab.Spawn(transform.position, Damage);
        }
        else
        {
            UB.Hp -= Damage;
            HealPrefab.Spawn(transform.position, -Damage);
        }
    }
}
