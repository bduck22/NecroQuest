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
        switch (Unit.UB.UnitTargetType)
        {
            case UnitTargetType.Close: //���� ����� ��
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
            case UnitTargetType.Far: //���� �� ĳ����
                if (collision.CompareTag("Mob"))
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
            case UnitTargetType.LowHp: //���� ü���� ���� �Ʊ�
                if (collision.CompareTag("HitBox"))
                {
                    if (!Unit.TargetUnit)
                    {
                        Unit.TargetUnit = collision.transform.parent;
                    }
                    else
                    {
                        if(Unit.TargetUnit.GetComponent<Unit>().UB.Hp > collision.transform.parent.GetComponent<Unit>().UB.Hp)
                        {
                            Unit.TargetUnit = collision.transform;
                        }
                    }
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform == Unit.TargetUnit)
        {
            Unit.TargetUnit = null;
        }
    }
}
