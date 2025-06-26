using UnityEngine;
using UnityEngine.UI;

public class Preset : MonoBehaviour
{
    public int SitNumber = -1;
    public int PresetNumber = -1;

    public Transform Chalist;
    public Transform ChaInfo;

    public int[][] list = new int[3][] { new int[4] { -1, -1, -1, -1 }, new int[4] { -1, -1, -1, -1 }, new int[4] { -1, -1, -1, -1 } };
    public void SetSit(int n)
    {
        if (SitNumber != n)
        {
            SitNumber = n;
            Chalist.gameObject.SetActive(true);
            ChaInfo.gameObject.SetActive(false);
        }
        else
        {
            Chalist.gameObject.SetActive(!Chalist.gameObject.activeSelf);
            ChaInfo.gameObject.SetActive(false);
        }
    }
    public void SetPre(int n)
    {
        if (n != PresetNumber)
        {
            PresetNumber = n;
            Chalist.gameObject.SetActive(!Chalist.gameObject.activeSelf);
            ChaInfo.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        Chalist = transform.parent.GetChild(1);
        ChaInfo = transform.parent.GetChild(3);
    }
    private void OnEnable()
    {
        Init();
        AllLoad();
    }
    public void Load()
    {
        if (list[PresetNumber][SitNumber] != -1)
        {
            transform.GetChild(PresetNumber).GetChild(SitNumber).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(PresetNumber).GetChild(SitNumber).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(((UnitClass)list[PresetNumber][SitNumber]).ToString() + "Head");
        }
        else transform.GetChild(PresetNumber).GetChild(SitNumber).GetChild(0).gameObject.SetActive(false);
    }
    public void AllLoad()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (list[i][j] != -1)
                {
                    transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(((UnitClass)list[i][j]).ToString() + "Head");
                }
                else transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void Delete()
    {
        list[PresetNumber][SitNumber] = -1;
        Load();
    }
    public void Set(int n)
    {
        for(int i = 0; i < 4; i++)
        {
            if (list[PresetNumber][i] == Data.Units[n])
            {
                list[PresetNumber][i] = -1;
            }
        }
        list[PresetNumber][SitNumber] = Data.Units[n];
        AllLoad();
    }
    public void Save()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                Data.LocalData.Presets[j][i] = list[j][i];
            }
        }
        LobbyManager.Instance.Wanning(Wannings.Saved);
    }
    public void Init()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                list[j][i] = Data.LocalData.Presets[j][i];
            }
        }
        AllLoad();
    }
    public void AllClear()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                list[j][i] = -1;
            }
        }
        AllLoad();
    }
}
