using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraDark : MonoBehaviour
{
    Volume volume;
    //PostProcessVolume volume;
    Vignette vignette;
    //Volume
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
    }

    bool o=true;
    void Update()
    {
        if(vignette.intensity.value <= 0.5f&&o)
        {
            vignette.intensity.value += 0.05f * Time.deltaTime;
        }
        else if(vignette.intensity.value >= 0.25f)
        {
            vignette.intensity.value -= 0.05f * Time.deltaTime;
            o = false;
        }
        else
        {
            o = true;
        }
    }
}
