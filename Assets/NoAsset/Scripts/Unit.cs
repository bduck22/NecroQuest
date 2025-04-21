using System.Collections;
using UnityEngine;

public enum UnitClass
{
    Tank,
    DPS,
    Mage
}
public enum UnitName
{

}
public class Unit : MonoBehaviour
{
    public Vector2 TargetWid;
    public bool Move;

    public float Speed;
    public float AttackSpeed;
    public float Hp;
    public float Damage;
    public float Intersection;
    public float Moral;

    public UnitClass UnitClass;
    public UnitTargetType UnitTargetType;
    
    public bool Invin;
    public float InvinTime;

    public Transform TargetUnit;

    [SerializeField]private float AttackTime;
    void Start()
    {
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
                transform.position = Vector2.MoveTowards(transform.position, TargetWid, Speed * Time.deltaTime);
            }
            else Move = false;
        }
    }
    void Attack()
    {
        Debug.Log("°ø°ÝÇÔ");
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
