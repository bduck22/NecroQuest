using UnityEngine;
using UnityEngine.UI;

public class UnitPreView : MonoBehaviour
{
    public UnitClass Type;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Set(int uclass)
    {
        Type = (UnitClass)uclass;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =  Resources.Load<Sprite>((Type).ToString() + "Head");
        transform.GetChild(1).GetComponentInChildren<Text>().text = Data.UnitData[Type].Name;
        transform.GetChild(2).GetComponentInChildren<Text>().text = Data.UnitData[Type].Description;
    }
    public void Agree()
    {
        Data.LocalData.StartingUnit = Type;
        LobbyManager.Instance.UnitAdd((int)Type);
        Time.timeScale = 1;
    }
}
