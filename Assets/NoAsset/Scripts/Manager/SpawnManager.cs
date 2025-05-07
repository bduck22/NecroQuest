using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] mobPrefabs;

    public MobBase[] mobs;
    private Transform spawnPoints;
    bool waving;

    int spawnnum;

    public float SpawnDelay;
    void Start()
    {
        spawnnum = 0;
        spawnPoints = transform.GetChild(0);
    }
    void Update()
    {
        if (waving)
        {

        }
    }
    public void WaveStart()
    {
        spawnnum = 0;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        foreach (Wave_Info info in GameManager.instance.Waves[GameManager.instance.Wave].MobInfo)
        {
            for (int i = 0; i < info.Count; i++) {
                GameObject Mob = Instantiate(mobPrefabs[info.Type]);
                Mob.transform.position = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)).position;
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}