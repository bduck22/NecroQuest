using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingLoad : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private void OnEnable()
    {
        transform.GetChild(0).GetComponentInChildren<Slider>().value = Data.LocalData.Master;
        audioMixer.SetFloat("Master", Mathf.Log10(Data.LocalData.Master)* 20);
        transform.GetChild(1).GetComponentInChildren<Slider>().value = Data.LocalData.SFX;
        audioMixer.SetFloat("SFX", Mathf.Log10(Data.LocalData.SFX)* 20);
        transform.GetChild(2).GetComponentInChildren<Slider>().value = Data.LocalData.BGM;
        audioMixer.SetFloat("BGM", Mathf.Log10(Data.LocalData.BGM)* 20);
    }
    private void OnDisable()
    {
        if (File.Exists(Data.path))
        {
            Data.Save();
        }
    }
}
