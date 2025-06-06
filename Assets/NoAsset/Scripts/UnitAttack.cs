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
            case UnitTargetType.Far: //가장 먼 캐릭터
                if (collision.CompareTag("Mob")&&collision.gameObject.activeSelf)
                {
                    if (!Unit.TargetUnit)
                    {
                        Unit.TargetUnit = collision.transform;
                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, Unit.TargetUnit.position) < Vector2.Distance(transform.position, collision.transform.position))
                        {
                            Unit.TargetUnit = collision.transform;
                        }
                    }
                }
                break;
            case UnitTargetType.LowHp: //가장 체력이 낮은 아군
                if (collision.CompareTag("HitBox"))
                {
                    if (!Unit.TargetUnit)
                    {
                        Unit.TargetUnit = collision.transform.parent;
                    }
                    else
                    {
                        if(Unit.TargetUnit.GetComponent<Unit>().Hp > collision.transform.parent.GetComponent<Unit>().Hp)
                        {
                            Unit.TargetUnit = collision.transform.parent;
                        }
                    }
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Unit.TargetUnit)
        {
            if (Unit.TargetUnit.CompareTag("Unit"))
            {
                Unit.TargetUnit = null;
            }
            else if (collision.transform == Unit.TargetUnit)
            {
                Unit.TargetUnit = null;
            }
        }
    }
}
