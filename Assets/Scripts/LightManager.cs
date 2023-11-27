using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    
    public Light mapLight;
    public Light characterLight;
    public Light globalLight;
    private Color originalTint;

    void Start()
    {
        characterLight.intensity = 0;
        if (RenderSettings.skybox.HasProperty("_Tint"))
           originalTint = RenderSettings.skybox.GetColor("_Tint");
        else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            originalTint = RenderSettings.skybox.GetColor("_SkyTint");
    }

    public void lightSnap(float time)
    {
        characterLight.intensity = 3000;
        globalLight.intensity = 0;
        StopAllCoroutines();
        StartCoroutine(lightResetTimer(time));
    }

    IEnumerator lightResetTimer(float timer)
    {
        float time = timer;
        if (RenderSettings.skybox.HasProperty("_Tint"))
                RenderSettings.skybox.SetColor("_Tint",new Vector4(0.3f,0.3f,0.35f,0.3f));
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
                RenderSettings.skybox.SetColor("_SkyTint",new Vector4(0.3f,0.3f,0.35f,0.3f));
        yield return new WaitForSeconds(1f);
        while(timer > 0)
        {
            characterLight.intensity -= Time.deltaTime*3000;
            globalLight.intensity += Time.deltaTime/time;

            Color currentTint = RenderSettings.skybox.GetColor("_Tint");
            Color newTint = Vector4.Lerp(currentTint,originalTint,0.08f/time);
            if (RenderSettings.skybox.HasProperty("_Tint"))
                RenderSettings.skybox.SetColor("_Tint",newTint);
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
                RenderSettings.skybox.SetColor("_SkyTint",newTint);

            timer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
