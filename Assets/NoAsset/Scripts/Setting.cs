using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SliderType
{
    Master,
    SFX,
    BGM
}
public class Ｓｅｔｔｉｎｇ : MonoBehaviour
{
    public SliderType Type;
    public AudioMixer audioMixer;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void ValueChange()
    {
        switch (Type)
        {
            case SliderType.Master:
                Data.LocalData.Master = slider.value;
                break;
            case SliderType.SFX:
                Data.LocalData.SFX = slider.value;
                break;
            case SliderType.BGM:
                Data.LocalData.BGM = slider.value;
                break;
        }
        audioMixer.SetFloat(Type.ToString(), Mathf.Log10(slider.value)*20);
    }
}
