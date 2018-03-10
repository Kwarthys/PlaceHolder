﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    ProductionType productionType;
    public ProductionType ProductionType { get { return productionType; } }

    [SerializeField]
    float coalConsumption = 2;

    [SerializeField]
    float foodConsumption = 2;

    [SerializeField]
    float prodPerSecond = 2;

    [SerializeField]
    bool recruitement = false;

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
            VillageManager.instance.buy(ProductionType.copper, 20);
        }
    }
}
