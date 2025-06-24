using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] mobPrefabs;

    private Transform spawnPoints;

    [SerializeField] private List<MobBase> Mobs = new List<MobBase>();

    public Transform BossUI;
    public string BossName;

    public int MobCount;

    public float SpawnDelay;
    bool waving = false;
    void Start()
    {
        spawnPoints = transform.GetChild(0);
    }
    void Update()
    {
        if(GameManager.instance.GameStatus == GameStatus.Waving)
        {
            if (MobCount <= 0 && !waving)
            {
                if (PlayerManager.instance.Checklock())
                {
                    Debug.Log("웨이브 끝");
                    GameManager.instance.GameStatus = GameStatus.WaveEnd;
                }
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
        waving = true;
        foreach (Wave_Info info in GameManager.instance.Waves[GameManager.instance.Wave].MobInfo)
        {
            int Wid = Random.Range(2, spawnPoints.childCount-2);
            for (int i = 0; i < info.Count; i++) {
                MobBase mob = null;
                MobCount++;
                foreach (MobBase o in Mobs)
                {
                    if(o.Type == mobPrefabs[info.Type].GetComponent<MobBase>().Type)
                    {
                        if (!o.gameObject.activeSelf)
                        {
                            mob = o;
                            mob.gameObject.SetActive(true);
                            mob.MobInit();
                            break;
                        }
                    }
                }
                if (!mob)
                {
                    mob = Instantiate(mobPrefabs[info.Type]).GetComponent<MobBase>();
                    mob.GetComponent<MobBase>().spawnManager = this;
                    mob.GetComponent<MobBase>().MobInit();
                    Mobs.Add(mob.GetComponent<MobBase>());
                }
                mob.transform.position = spawnPoints.GetChild(Wid+Random.Range(-2, 3)).position;
                yield return new WaitForSeconds(0.15f);
            }
            if (info.middle)
            {
                MobBase mob = Instantiate(mobPrefabs[5]).GetComponent<MobBase>();
                mob.spawnManager = this;
                mob.MobInit();
                Mobs.Add(mob);
            }
            else if(info.final)
            {

            }
            yield return new WaitForSeconds(SpawnDelay);
        }
        waving = false;
    }
}