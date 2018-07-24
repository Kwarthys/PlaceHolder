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

    [SerializeField]
    public List<RecruitmentOrder> recruitment = new List<RecruitmentOrder>();

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
    }

    private void Update()
    {
        recruitment.RemoveAll(numberIs0);
    }

    private static bool numberIs0(RecruitmentOrder o)
    {
        return o.number == 0;
    }

    public void buyUnit(string unit)
    {

        ArmyType type = ArmyType.cutter;
        //galley, frigate, destroyer, A1, A2, A3, artillery, bomber
        if (unit.Equals("frigate"))
        {
            type = ArmyType.frigate;
        }
        else if (unit.Equals("galley"))
        {

            type = ArmyType.cutter;
        }
        else if (unit.Equals("A1"))
        {

            type = ArmyType.A1;
        }
        else if (unit.Equals("A2"))
        {

            type = ArmyType.A2;
        }
        else if (unit.Equals("artillery"))
        {

            type = ArmyType.artillery;
        }
        else if (unit.Equals("bomber"))
        {

            type = ArmyType.bomber;
        }

        if (VillageManager.instance.buy(type))
        {
            recruitment.Add(new RecruitmentOrder(type, 1));
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
