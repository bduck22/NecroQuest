using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 TargetWid;
    public bool Move;
    public float Speed;
    void Start()
    {
        
    }
    void Update()
    {
        if (Move)
        {
            if ((Vector2)transform.position != TargetWid)
            {
                transform.position = Vector2.MoveTowards(transform.position, TargetWid, Speed * Time.deltaTime);
            }
            else Move = false;
        }
    }
}
