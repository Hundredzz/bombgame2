using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    void Start() 
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera> ();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
    }


    public IEnumerator ProcessShake(float shakeIntensity, float shakeTiming)
        {
            Shake(shakeIntensity,10);
            yield return new WaitForSeconds(shakeTiming);
            Shake(0, 0);
        }

    public void Shake(float amplitudeGain, float frequencyGain) 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;    
    }

    public void Shaking(float shakeIntensity, float shakeTiming)
    {
        StartCoroutine(ProcessShake(shakeIntensity,shakeTiming));
    }
}
