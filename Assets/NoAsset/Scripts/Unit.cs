using System.Collections;
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

    private float AttackTime;
    public Animator AttackAnimation;

    public GameObject AttackEffect;
    [SerializeField] CircleCollider2D Interaction;
    void Start()
    {
        Interaction.radius = UB.Intersection+2f;
    }
    public void UnitInit()
    {
        UB.Hp = UB.MaxHp;
        AttackTime = 0;
        Interaction.radius = UB.Intersection * 2f;
        TargetUnit = null;
        Invin =false;
        Move = false;
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
                
                break;
            case UnitClass.ArchM:
                Effect = Instantiate(AttackEffect, TargetUnit.transform.position, AttackAnimation.transform.localRotation);
                break;
            case UnitClass.SpiritM:
                break;
            case UnitClass.HolyM:
                break;
        }
        Effect.GetComponent<AttackEffect>().Damage = UB.Damage;

        AttackAnimation.SetTrigger("Attack");
    }

}
