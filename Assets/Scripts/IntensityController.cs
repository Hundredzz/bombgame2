using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class IntensityController : MonoBehaviour
{
    public VolumeProfile volumeProfile;

    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    private LensDistortion lensDistortion;
    private Vignette vignette;

    private float targetIntensity;
    private float transitionDuration;
    private float transitionStartTime;

    private void Start()
    {
        if (!volumeProfile)
        {
            Debug.LogError("Please assign a Post-Processing Volume Profile.");
            return;
        }

        // Try to get the post-processing effects from the volume profile.
        volumeProfile.TryGet(out chromaticAberration);
        volumeProfile.TryGet(out bloom);
        volumeProfile.TryGet(out lensDistortion);
        volumeProfile.TryGet(out vignette);
        // Example: Change chromatic aberration intensity to 1 over 3 seconds.
    }

    public void ChangeIntensityOverTime(string property, float intensity, float time)
    {
        targetIntensity = intensity;
        transitionDuration = time;
        transitionStartTime = Time.time;

        StartCoroutine(ChangeIntensityCoroutine(property));
    }

    private IEnumerator ChangeIntensityCoroutine(string property)
    {
        float elapsedTime = 0f;
        float startIntensity = 0f;

        // Determine which post-processing effect to use based on the property name.
        switch (property)
        {
            case "chromaticAberration":
                startIntensity = chromaticAberration != null ? chromaticAberration.intensity.value : startIntensity;
                break;
            case "bloom":
                startIntensity = bloom != null ? bloom.intensity.value : startIntensity;
                break;
            case "lensDistortion":
                startIntensity = lensDistortion != null ? lensDistortion.intensity.value : startIntensity;
                break;
            case "vignette":
                startIntensity = vignette != null ? vignette.intensity.value : startIntensity;
                break;
            default:
                Debug.LogError(property+ "not recognized.");
                yield break;
        }

        while (elapsedTime < transitionDuration)
        {
            elapsedTime = Time.time - transitionStartTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            // Set the intensity based on the property name.
            switch (property)
            {
                case "chromaticAberration":
                    if (chromaticAberration != null)
                    {
                        chromaticAberration.intensity.Override(Mathf.Lerp(startIntensity, targetIntensity, t));
                    }
                    break;
                case "bloom":
                    if (bloom != null)
                    {
                        bloom.intensity.Override(Mathf.Lerp(startIntensity, targetIntensity, t));
                    }
                    break;
                case "lensDistortion":
                    if (lensDistortion != null)
                    {
                        lensDistortion.intensity.Override(Mathf.Lerp(startIntensity, targetIntensity, t));
                    }
                    break;
                case "vignette":
                    if (vignette != null)
                    {
                        vignette.intensity.Override(Mathf.Lerp(startIntensity, targetIntensity, t));
                    }
                    break;
            }

            yield return null;
        }

        // Ensure the target intensity is set exactly.
        switch (property)
        {
            case "chromaticAberration":
                if (chromaticAberration != null)
                {
                    chromaticAberration.intensity.Override(targetIntensity);
                }
                break;
            case "bloom":
                if (bloom != null)
                {
                    bloom.intensity.Override(targetIntensity);
                }
                break;
            case "lensDistortion":
                if (lensDistortion != null)
                {
                    lensDistortion.intensity.Override(targetIntensity);
                }
                break;
            case "vignette":
                if (vignette != null)
                {
                    vignette.intensity.Override(targetIntensity);
                }
                break;
        }
    }
}