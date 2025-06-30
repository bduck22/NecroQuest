using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraDark : MonoBehaviour
{
    PostProcessVolume volume;
    Vignette vignette;
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);
    }

    bool o=true;
    void Update()
    {
        if(vignette.intensity <= 0.5f&&o)
        {
            vignette.intensity.value += 0.5f * Time.deltaTime;
        }
        else if(vignette.intensity >=0.25f)
        {
            vignette.intensity.value -= 0.5f * Time.deltaTime;
            o = false;
        }
        else
        {
            o = true;
        }
    }
}
