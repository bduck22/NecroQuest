using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingLoad : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).GetComponentInChildren<Slider>().value = Data.LocalData.Master;
        LobbyManager.Instance.AudioMixer.SetFloat("Master", Mathf.Log10(Data.LocalData.Master));
        transform.GetChild(1).GetComponentInChildren<Slider>().value = Data.LocalData.SFX;
        LobbyManager.Instance.AudioMixer.SetFloat("SFX", Mathf.Log10(Data.LocalData.SFX));
        transform.GetChild(2).GetComponentInChildren<Slider>().value = Data.LocalData.BGM;
        LobbyManager.Instance.AudioMixer.SetFloat("BGM", Mathf.Log10(Data.LocalData.BGM));
    }
    private void OnDisable()
    {
        if(SceneManager.GetActiveScene().buildIndex != 4)
        {
            Data.Save();
        }
    }
}
