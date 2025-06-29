using UnityEngine;

public class CraditCamera : MonoBehaviour
{
    public Transform target;
    public float Speed;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
    }
}
