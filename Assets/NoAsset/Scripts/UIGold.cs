using DamageNumbersPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGold : MonoBehaviour
{
    Text text;
    public RectTransform SpawnPoint;
    public DamageNumber preFab;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = Data.Gold.ToString("#,##0");
    }
    public void Spawn(int value)
    {
        preFab.SpawnGUI(SpawnPoint, Vector3.zero, value);
    }
}
