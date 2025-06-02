using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetMove : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    Vector3 Direction;
    void Start()
    {
        Direction = Target.position-transform.position;
    }
    void Update()
    {
        transform.Translate(Direction * Speed*Time.deltaTime);
    }
}
