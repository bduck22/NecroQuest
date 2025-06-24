using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (!GetComponentInChildren<AttackEffect>()|| !GetComponentInChildren<AttackEffect>().enabled|| transform.childCount==0)
        {
            Destroy(gameObject);
        }
    }
}
