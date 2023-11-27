using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScriprt : MonoBehaviour
{
    public Slider theSlider;
    public Slider followSlider;
    public TextMeshProUGUI valueText;
    public bool Isfollow;
    public bool IsUp;
    public bool useText;
    void Start()
    {
        
    }

    public void SetmaxValue(float max)
    {
        theSlider.maxValue = max;
        theSlider.value = theSlider.maxValue;
        if(Isfollow == true)
        {
            followSlider.maxValue = max;
            followSlider.value = followSlider.maxValue;
        }
    }

    public void SetValue(float value)
    {
        if(IsUp == false)
        {
            theSlider.value = theSlider.maxValue - value;
        }
        else
        {
            theSlider.value = value;
        }

        if(Isfollow == true && followSlider.value > theSlider.value)
        {
            StartCoroutine(follow());
        }
        else if(Isfollow == true && followSlider.value <= theSlider.value)
        {
            followSlider.value = theSlider.value;
        }

        if(useText == true)
        {
            valueText.text = (theSlider.value - (theSlider.value%1)).ToString();
        }
    }

    IEnumerator follow()
    {
        yield return new WaitForSeconds(0.2f);
        while(followSlider.value < theSlider.value-0.02 || followSlider.value > theSlider.value+0.02)
        {
            if(followSlider.value > theSlider.value)
            {
                followSlider.value -= followSlider.maxValue/200;
                yield return new WaitForSeconds(0.01f);
            }
            else if(followSlider.value < theSlider.value)
            {
                followSlider.value += followSlider.maxValue/200;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
