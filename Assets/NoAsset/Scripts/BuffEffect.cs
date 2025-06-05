using UnityEngine;

public class BuffEffect : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
