using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (!GetComponentInChildren<AttackEffect>()||transform.childCount==0)
        {
            Destroy(gameObject);
        }
    }
}
