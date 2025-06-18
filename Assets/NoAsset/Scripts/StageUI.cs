using TMPro;
using UnityEngine;
public class StageUI : MonoBehaviour
{
    public bool gold;
    public bool wave;
    public bool count;
    TMP_Text text;
    public SpawnManager spawnManager;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (gold)
        {
            text.text = GameManager.instance.gold.ToString("#,##0");
        }else if (wave)
        {
            text.text = GameManager.instance.Wave.ToString("#,##0") + " / "+GameManager.instance.Waves.Length.ToString("#,##0");
        }
        else if (count)
        {
            text.text = spawnManager.MobCount.ToString("#,##0");
        }
    }
}
