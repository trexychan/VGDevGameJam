using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    public DragonManager dragon;
    public Slider fillSlider;

    void Update()
    {
        fillSlider.value = dragon.health / 100;
    }
}
