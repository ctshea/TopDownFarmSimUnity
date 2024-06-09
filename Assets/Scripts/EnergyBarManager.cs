using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarManager : MonoBehaviour
{

    public Slider energySlider;
    public int maxEnergy;

    public void SetMaxEnergy()
    {
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;
    }

    public void setEnergy(int energy)
    {
        energySlider.value = energy;
    }

    public void subtractEnergy(int value)
    {
        energySlider.value -= value;
    }

    public void resetEnergy()
    {
        energySlider.value = maxEnergy;
    }
   
}
