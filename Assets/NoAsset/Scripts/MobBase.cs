using UnityEngine;

public enum UnitTargetType
{
    Close,
    Far,
    LowHp
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
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}
}
