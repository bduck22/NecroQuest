using System.Collections;
using UnityEngine;

public class UnitHit : MonoBehaviour
{
    Unit Unit;
    void Start()
    {
        Unit = transform.parent.GetComponent<Unit>();
    }
    IEnumerator Invining()
    {
        Unit.Invin = true;
        yield return new WaitForSeconds(Unit.InvinTime);
        Unit.Invin = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Mob"))
        {
            if (!Unit.Invin)
            {
                Unit.UB.Hp--;
                StartCoroutine(Invining());
            }
        }
    }
}
