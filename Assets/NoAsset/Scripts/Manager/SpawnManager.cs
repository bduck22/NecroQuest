using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] mobPrefabs;

    public MobBase[] mobs;
    private Transform spawnPoints;

    [SerializeField] private List<MobBase> Mobs = new List<MobBase>();

    public static int MobCount;

    public float SpawnDelay;
    void Start()
    {
        spawnPoints = transform.GetChild(0);
    }
    void Update()
    {
        Debug.Log(MobCount+"ㅁㄴㅇㄹ");
        if(GameManager.instance.GameStatus == GameStatus.Waving)
        {
            if(MobCount <= 0)
            {
                Debug.Log("웨이브 끝");
                GameManager.instance.GameStatus = GameStatus.WaveEnd;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (MobBase mob in Mobs)
            {
                mob.gameObject.SetActive(false);
            }
        }
    }
    public void WaveStart()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        foreach (Wave_Info info in GameManager.instance.Waves[GameManager.instance.Wave].MobInfo)
        {
            for (int i = 0; i < info.Count; i++) {
                GameObject mob = null;
                MobCount++;
                foreach (MobBase o in Mobs)
                {
                    if(o.Mob.Type == mobPrefabs[info.Type].GetComponent<MobBase>().Mob.Type)
                    {
                        if (!o.gameObject.activeSelf)
                        {
                            mob = o.gameObject;
                            mob.SetActive(true);
                            mob.GetComponent<MobBase>().MobInit();
                            break;
                        }
                    }
                }
                if (!mob)
                {
                    mob = Instantiate(mobPrefabs[info.Type]);
                    Mobs.Add(mob.GetComponent<MobBase>());
                }
                mob.transform.position = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)).position;
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}