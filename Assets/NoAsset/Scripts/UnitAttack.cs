using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    Unit Unit;
    void Start()
    {
        Unit = transform.parent.GetComponent<Unit>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (Unit.UnitTargetType)
        {
            case UnitTargetType.Close: //가장 가까운 적
                if (collision.CompareTag("Mob"))
                {
                    if (!Unit.TargetUnit)
                    {
                        Unit.TargetUnit = collision.transform;
                    }
                    else
                    {
                        if(Vector2.Distance(transform.position, Unit.TargetUnit.position) > Vector2.Distance(transform.position, collision.transform.position))
                        {
                            Unit.TargetUnit = collision.transform;
                        }
                    }
                }
                break;
            case UnitTargetType.Far:
                break;
            case UnitTargetType.LowHp: //가장 체력이 낮은 아군
                if (collision.CompareTag("Unit"))
                {
                    if (!Unit.TargetUnit)
                    {
                        Unit.TargetUnit = collision.transform;
                    }
                    else
                    {
                        if(Unit.TargetUnit.GetComponent<Unit>().Hp > collision.GetComponent<Unit>().Hp)
                        {
                            Unit.TargetUnit = collision.transform;
                        }
                    }
                }
                break;
            case UnitTargetType.Provo: //도발
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == Unit.TargetUnit)
        {
            Unit.TargetUnit = null;
        }
    }
}
