using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] mobPrefabs;

    private Transform spawnPoints;

    public List<MobBase> Mobs = new List<MobBase>();

    public MobBase Boss;
    public bool IsBoss;
    public Slider BossUI;

    public int MobCount;

    public float SpawnDelay;
    public bool waving = false;
    void Start()
    {
        spawnPoints = transform.GetChild(0);
    }
    void Update()
    {
        if (IsBoss && !BossUI.transform.parent.gameObject.activeSelf)
        {
            BossUI.transform.parent.gameObject.SetActive(true);
            BossUI.transform.parent.GetChild(0).GetComponentInChildren<TMP_Text>().text = (Boss.Type == MobType.Dullahan ? "듀라한" : "네크로맨서");
        }
        else if (IsBoss)
        {
            BossUI.value = Boss.Hp / (Boss.MaxHp * 20f);
            if (Boss.Hp <= 0||!Boss.gameObject.activeSelf)
            {
                BossUI.transform.parent.gameObject.SetActive(false);
                IsBoss = false;
                Boss = null;
            }
        }
        if (GameManager.instance.GameStatus == GameStatus.Waving)
        {
            if (MobCount <= 0 && !waving)
            {
                if (PlayerManager.instance.Checklock())
                {
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
            yield return new WaitForSeconds(SpawnDelay);
            int Wid = Random.Range(2, spawnPoints.childCount - 2);
            StartCoroutine(Spawn(info.Type, info.Count * (1+Data.LocalData.diffi*0.1f)));
            if (info.middle || info.final)
            {
                MobCount++;
                if (info.middle)
                {
                    Boss = Instantiate(mobPrefabs[5]).GetComponent<MobBase>();
                }
                else if (info.final)
                {
                    Boss = Instantiate(mobPrefabs[6]).GetComponent<MobBase>();
                }
                Boss.transform.position = spawnPoints.GetChild(Wid + Random.Range(-2, 3)).position;
                IsBoss = true;
                Boss.spawnManager = this;
                Boss.MobInit();
                Mobs.Add(Boss);
                GetComponent<AudioSource>().Play();
            }
        }
        waving = false;
    }
    public IEnumerator Spawn(int type, float count)
    {
        int Wid = Random.Range(2, spawnPoints.childCount - 2);
        for (int i = 0; i < count; i++)
        {
            if (i % 5 == 0)
            {
                Wid = Random.Range(2, spawnPoints.childCount - 2);
            }
            MobBase mob = null;
            MobCount++;
            foreach (MobBase o in Mobs)
            {
                if (o.Type == mobPrefabs[type].GetComponent<MobBase>().Type)
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
                mob = Instantiate(mobPrefabs[type]).GetComponent<MobBase>();
                mob.GetComponent<MobBase>().spawnManager = this;
                mob.GetComponent<MobBase>().MobInit();
                Mobs.Add(mob.GetComponent<MobBase>());
            }
            mob.transform.position = spawnPoints.GetChild(Wid + Random.Range(-2, 3)).position;
            yield return new WaitForSeconds(0.1f);
        }
    }
}