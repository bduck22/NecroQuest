using UnityEngine;

public class SkillAniEnd : MonoBehaviour
{
    Unit Unit;
    private void Start()
    {
        Unit = transform.parent.GetComponent<Unit>();
    }
    public void AniEnd()
    {
        Unit.locked = false;
    }
    public void ReMove()
    {
        Unit.Hlocked = false;
    }
    public void DrangonDash()
    {
        Unit.transform.position = Unit.AttackAnimation.transform.GetChild(1).position;
    }
}
