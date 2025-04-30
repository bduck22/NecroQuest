using System.Collections;
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
    public float Damage;
    public float Intersection;
    public float Moral;

    [Header("Type")]
    public UnitClass UnitClass;
    public UnitTargetType UnitTargetType;

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
        Interaction.radius = Intersection+2f;
    }
    void Update()
    {
        if(AttackTime < 1)
        {
            AttackTime += AttackSpeed * Time.deltaTime;
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

        GameObject Effect = Instantiate(AttackEffect);
        AttackEffect.GetComponent<AttackEffect>().Damage = Damage;
        Effect.transform.rotation = AttackAnimation.transform.localRotation;
        switch (UnitClass)
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
                AttackEffect.transform.position = TargetUnit.transform.position;
                break;
            case UnitClass.SpiritM:
                break;
            case UnitClass.HolyM:
                break;
        }

        AttackAnimation.SetTrigger("Attack");
    }
    IEnumerator Invining()
    {
        Invin = true;
        yield return new WaitForSeconds(InvinTime);
        Invin = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mob"))
        {
            if (!Invin)
            {
                Hp--;
                StartCoroutine(Invining());
            }
        }
    }
}
