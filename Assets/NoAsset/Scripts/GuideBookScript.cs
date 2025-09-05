using UnityEngine;
using UnityEngine.UI;

public class GuideBookScript : MonoBehaviour
{
    public ScrollRect Scroll;
    public int ScrollNumber;

    public Transform[] Buttons;
    public void SetNumber()
    {
        if ((int)(ScrollNumber / 6f) != Mathf.FloorToInt((1 - Scroll.verticalNormalizedPosition) * 6) || Mathf.FloorToInt((1 - Scroll.verticalNormalizedPosition) * 6) == 0)
        {
            if (Mathf.FloorToInt((1 - Scroll.verticalNormalizedPosition) * 6) < 6&& Mathf.FloorToInt((1 - Scroll.verticalNormalizedPosition) * 6)>0)
            {
                ScrollNumber = Mathf.FloorToInt((1 - Scroll.verticalNormalizedPosition) * 6);
                foreach (Transform t in Buttons)
                {
                    t.gameObject.SetActive(false);
                }
                Buttons[ScrollNumber].gameObject.SetActive(true);
            }
        }
        switch (ScrollNumber)
        {
            case 0:
                HoriBlock(0.01f);
                break;
            case 1:
                HoriBlock(0.01f);
                break;
            case 2:
                HoriBlock(0.43f);
                break;
            case 3:
                HoriBlock(0.18f);
                break;
            case 4:
                HoriBlock(0.18f);
                break;
            case 5:
                HoriBlock(1.1f);
                break;
        }
    }

    public void SetButton(int num)
    {
        ScrollNumber = num;
        foreach (Transform t in Buttons)
        {
            t.gameObject.SetActive(false);
        }
        Buttons[ScrollNumber].gameObject.SetActive(true);
        Scroll.verticalNormalizedPosition = 1 - (ScrollNumber / 5f);
    }

    void HoriBlock(float block)
    {
        if (Scroll.horizontalNormalizedPosition > block)
        {
            Scroll.horizontalNormalizedPosition = block;
        }
    }
}
