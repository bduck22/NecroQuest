using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartEffectCorutine : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    [SerializeField] private bool Is_On_Corutine = false;

    [SerializeField] private float effect_speed;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorAdjustments);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Is_On_Corutine == false)
        {
            StartCoroutine(Start_Corutine());
        }
    }

    IEnumerator Start_Corutine()
    {
        Is_On_Corutine = true;

        vignette.active = true;
        colorAdjustments.active = true;


        vignette.color.value = Color.black;
        vignette.intensity.value = 1f;
        vignette.center.value = new Vector2(2.0f, 2.0f);

        colorAdjustments.saturation.value = -100;

        yield return new WaitForSeconds(0.05f);

        vignette.center.value = new Vector2(0.5f, 0.5f);

        for (float i = 0; vignette.intensity.value >= 0.05f; i++)
        {
            vignette.intensity.value -= effect_speed * Time.smoothDeltaTime;
            
            colorAdjustments.saturation.value += 100 * effect_speed * Time.smoothDeltaTime;

            yield return new WaitForSeconds(0.02f);
        }

        vignette.color.value = Color.red;

        vignette.active = false;
        colorAdjustments.active= false;

        Is_On_Corutine = false;

        yield return null;
    }


}
