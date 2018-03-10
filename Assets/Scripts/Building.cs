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
        if(recruitement)
        {
            VillageManager.instance.buy(woodCost, copperCost);
        }
    }
}
