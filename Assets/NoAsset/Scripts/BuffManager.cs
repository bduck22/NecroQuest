using UnityEngine;

public class BuffManager : MonoBehaviour
{
    Unit Unit;
    MobBase Mob;
    public bool IsUnit;
    public float LodingTime;
    private float time;
    void Start()
    {
        if (IsUnit)
        {
            Unit = GetComponent<Unit>();
        }
        else
        {
            Mob = GetComponent<MobBase>();
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= LodingTime)
        {
            time = 0;
            BuffLoad();
        }
    }
    void BuffLoad()
    {
        if (!IsUnit) {
            foreach (Buff buff in Mob.Mob.Buff)
            {
                switch (buff.Type)
                {
                }
            }
        }
        else
        {

        }
    }
}
