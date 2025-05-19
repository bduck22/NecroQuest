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
}
