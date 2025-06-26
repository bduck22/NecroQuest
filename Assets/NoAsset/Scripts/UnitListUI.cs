using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : MonoBehaviour
{
    private void OnEnable()
    {
        Load();
    }
    public void Load()
    {
        for(int i = 0; i < 7; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        int sitnum = 0;
        foreach(int u in Data.Units)
        {
            transform.GetChild(sitnum).gameObject.SetActive(true);
            transform.GetChild(sitnum++).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(((UnitClass)u).ToString() + "Head");
        }
    }
}
