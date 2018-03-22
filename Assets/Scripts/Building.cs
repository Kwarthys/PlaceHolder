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
        //galley, frigate, destroyer, A1, A2, A3, artillery, bomber
        if (unit.Equals("frigate"))
        {
            if(VillageManager.instance.buy(50, 40, ArmyType.frigate))
            {
                //Debug.Log("Bought a Fregate 50,40");
            }
        }
        else if (unit.Equals("galley"))
        {
            if (VillageManager.instance.buy(40, 10, ArmyType.galley))
            {
                //Debug.Log("Bought a Fregate 50,40");
            }
        }
        else if (unit.Equals("A1"))
        {
            if (VillageManager.instance.buy(100, 100, ArmyType.A1))
            {
                //Debug.Log("Bought a Fregate 50,40");
            }
        }
        else if (unit.Equals("A2"))
        {
            if (VillageManager.instance.buy(110, 110, ArmyType.A2))
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
