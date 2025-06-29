using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    TMP_Text Text;
    void Start()
    {
        Text = GetComponent<TMP_Text>();    
    }
    bool c=true;
    void Update()
    {
        if(Text.color.a >0&&c)
        {
            Text.color -= Color.black * 2*Time.deltaTime;
        }
        else if(Text.color.a < 1)
        {
            c = false;
            Text.color += Color.black * 2*Time.deltaTime;
        }
        else
        {
            c = true;
        }
    }
}
