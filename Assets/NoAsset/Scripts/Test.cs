using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    List<ParticleSystem.Particle> Inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();

    }
    void Update()
    {
        
    }


    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("asdf");
        Debug.Log(other.name);
    }

    //{
    //    int numInside = ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, Inside);
    //    //Debug.Log(numInside);
    //    //int numEnter = ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    //int numExit = ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    //    Debug.Log(Inside[0]);
    //}
}
