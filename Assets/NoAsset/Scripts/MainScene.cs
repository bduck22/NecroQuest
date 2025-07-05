using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Transform Fade;
    [SerializeField] AudioMixer AudioMixer;
    void Start()
    {
        if (File.Exists(Data.path))
        {
            Data.Load();
            AudioMixer.SetFloat("Master", Mathf.Log10(Data.LocalData.Master) * 20);
            AudioMixer.SetFloat("SFX", Mathf.Log10(Data.LocalData.SFX) * 20);
            AudioMixer.SetFloat("BGM", Mathf.Log10(Data.LocalData.BGM) * 20);
        }
        else
        {
            Data.LocalData = new LocalData();
            Data.LocalData.Gold = 5000;
            Data.LocalData.diffi = 0;
            Data.LocalData.SelectPreSet = 0;
            Data.Stats = new UnitStats();
            Data.Units = new List<int>();
            Data.LocalData.Presets = new List<int[]>();
            Data.LocalData.GetUnits = new Dictionary<UnitClass, LocalUnit>();
            Data.LocalData.Blessing = new Dictionary<BlessingType, int>()
            {   {BlessingType.Attack, 0 },
                {BlessingType.Defence, 0 },
                { BlessingType.Skill, 0 },
                {BlessingType.Moral, 0 }
            };
            Data.LocalData.Master = 1;
            Data.LocalData.SFX = 1;
            Data.LocalData.BGM = 1;
            for (int i = 0; i < 3; i++)
            {
                Data.LocalData.Presets.Add(new int[4] { -1, -1, -1, -1 });
            }
        }
    }

    void Update()
    {

    }
    public void GameStart()
    {
        StartCoroutine(G());
    }
    public void Credit()
    {
        StartCoroutine(C());
    }
    public void Init()
    {
        Data.Delete();
        Application.Quit();
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator G()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
    IEnumerator C()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }
}
