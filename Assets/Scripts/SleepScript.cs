using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SleepScript : MonoBehaviour
{
    public ClockControl clock;
    public FarmingSoil farmingSoil;
    public EnergyBarManager energyBarManager;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            resetDay();
        }
    }

    public void resetDay()
    {
        clock.incrementDay();
        farmingSoil.dailySoilCrops();
        energyBarManager.resetEnergy();
    }

}
