using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelMeter : MonoBehaviour
{
    public FireBreathing fireBreathing;
    public Slider fillSlider;

    void Update()
    {
        fillSlider.value = ((float) fireBreathing.fuel) / fireBreathing.maxFuel;
        print(((float) fireBreathing.fuel) / fireBreathing.maxFuel);
    }
}
