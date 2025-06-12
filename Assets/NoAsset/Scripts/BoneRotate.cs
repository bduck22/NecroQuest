using UnityEngine;

public class BoneRotate : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(0, 0, 180f*Time.deltaTime);
    }
}
