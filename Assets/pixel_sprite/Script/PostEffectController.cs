using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostEffectController : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private DepthOfField depthOfField;
    private bool Is_On_Corutine = false;

    [SerializeField] private float effect_speed;
    // Start is called before the first frame update

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);//
        volume.profile.TryGet(out depthOfField);//
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && Is_On_Corutine == false)
        {
            StartCoroutine(Hit_Effect());
        }
    }

    IEnumerator Hit_Effect()
    {
        vignette.active = true;
        Is_On_Corutine = true;
        depthOfField.active = true;
        vignette.intensity.value = 0f;
        depthOfField.focalLength.value = 0;

        for(float i = 0; vignette.intensity.value <= 0.4f; i++)
        {
            vignette.intensity.value += effect_speed * Time.smoothDeltaTime;
            depthOfField.focalLength.value += 100 * effect_speed * Time.smoothDeltaTime;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.05f);
        for (float i = 0; vignette.intensity.value >= 0.01f; i++)
        {
            vignette.intensity.value -= effect_speed * Time.smoothDeltaTime;
            depthOfField.focalLength.value -= 100 * effect_speed * Time.smoothDeltaTime;
            yield return new WaitForSeconds(0.02f);
        }
        vignette.active = false;
        Is_On_Corutine = false;
        depthOfField.active = false;
        yield return null;
    }

    
}
