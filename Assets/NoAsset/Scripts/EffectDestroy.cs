using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    private ParticleSystem PS;
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!PS.IsAlive(true))
        {
            Destroy(gameObject);
        }
    }
}
