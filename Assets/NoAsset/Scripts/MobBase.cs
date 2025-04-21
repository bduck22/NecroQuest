using UnityEngine;

public enum UnitTargetType
{
    Close,
    Far,
    LowHp,
    Provo
}
public class MobBase : MonoBehaviour
{
    public float Hp;
    public float Speed;
    public float Damage;

    void Start()
    {
        
    }
    void Update()
    {
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
