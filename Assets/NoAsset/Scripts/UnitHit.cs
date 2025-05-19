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
    IEnumerator Invining()
    {
        Unit.Invin = true;
        HitImage.color = Color.red;
        yield return new WaitForSeconds(Unit.InvinTime / 3);
        HitImage.color = Color.white;
        yield return new WaitForSeconds(Unit.InvinTime/3*2);
        Unit.Invin = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mob"))
        {
            if (!Unit.Invin)
            {
                float Damage = collision.transform.GetComponent<MobBase>().Damage;
                Unit.HpChange(Damage);
                StartCoroutine(Invining());
            }
        }
    }
}
