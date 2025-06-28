using UnityEngine;
using UnityEngine.UI;

public class GuardianLoad : MonoBehaviour
{
    public void Load()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            if (i < PlayerManager.instance.guardians.Count) {
                transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerManager.instance.guardians[i].GuardianType.ToString());
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        Load();
    }
}
