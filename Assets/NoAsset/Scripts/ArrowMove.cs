using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public Transform Target;   
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, 1f);
    }
}
