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
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void ValueChange()
    {
        audioMixer.SetFloat(Type.ToString(), Mathf.Log10(slider.value)*20);
    }
}
