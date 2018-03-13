using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Production")]

    [SerializeField]
    ProductionType productionType;
    public ProductionType ProductionType { get { return productionType; } }
    [SerializeField]
    float prodPerSecond = 2;

    [Header("Consumption")]

    [SerializeField]
    float coalConsumption = 2;
    [SerializeField]
    float foodConsumption = 2;

    [Header("Purchase")]

    [SerializeField]
    bool recruitement = false;
    [SerializeField]
    int woodCost;
    [SerializeField]
    int copperCost;


    [Header("UI")]
    [SerializeField]
    GameObject canvas;

    public float GetProd(float time)
    {
        return time * prodPerSecond;
    }

    public float GetFoodComsuption()
    {
        return foodConsumption;
    }

    internal float GetCoalConsumption()
    {
        return coalConsumption;
    }

    private void OnMouseDown()
    {
        if(canvas != null && !VillageManager.instance.canvasOpened)
        {
            canvas.SetActive(true);
            VillageManager.instance.canvasOpened = true;
        }

        /*
        if(recruitement)
        {
            VillageManager.instance.buy(woodCost, copperCost);
        }
        */
    }

    public void buyUnit(string unit)
    {
        if(unit.Equals("fregate"))
        {
            if(VillageManager.instance.buy(50, 40, ArmyType.frigate))
            {
                //Debug.Log("Bought a Fregate 50,40");
            }
        }
    }
    
    public void closeCanvas()
    {
        if(canvas != null)
        {
            canvas.SetActive(false);
            VillageManager.instance.canvasOpened = false;
        }
    }
}
