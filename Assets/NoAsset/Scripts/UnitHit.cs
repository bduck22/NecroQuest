using DamageNumbersPro;
using System.Collections;
using UnityEngine;

public class UnitHit : MonoBehaviour
{
    Unit Unit;
    private SpriteRenderer HitImage;
    void Start()
    {
        HitImage = transform.parent.GetChild(4).GetComponent<SpriteRenderer>();
        Unit = transform.parent.GetComponent<Unit>();
    }
    public void Hit()
    {
        StartCoroutine(Invining());
    }
    IEnumerator Invining()
    {
        if (Unit.Invin) yield return null;
        else Unit.Invin = true;
        HitImage.color = Color.red;
        yield return new WaitForSeconds((Unit.InvinTime+Unit.PlusStats.InvinTime) / 3f*2);
        HitImage.color = Color.white;
        yield return new WaitForSeconds((Unit.InvinTime + Unit.PlusStats.InvinTime) / 3f);
        Unit.Invin = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mob"))
        {
            if (!Unit.Invin)
            {
                float Damage = collision.transform.GetComponent<MobBase>().Damage * collision.transform.GetComponent<MobBase>().AttackWeight;
                Unit.HpChange(Damage);
                StartCoroutine(Invining());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            float Damage = other.GetComponentInChildren<AttackEffect>().Damage;
            Unit.HpChange(Damage * other.GetComponentInChildren<AttackEffect>().Weight);
            Destroy(other.gameObject);
            StartCoroutine(Invining());
        }
    }
}
