using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ProductionType
{
    copper, wood, coal, food
};

public class VillageManager : MonoBehaviour {

    public static VillageManager instance { get; internal set; }

    [Header("Resources")]

    [SerializeField]
    float maxCapacity = 200;
    public float MaxCapacity { get { return maxCapacity; }}

    [SerializeField]
    float copper = 100;
    public float Copper { get { return copper; } internal set { copper = Mathf.Clamp(value, 0, MaxCapacity); } }

    [SerializeField]
    float wood = 100;
    public float Wood { get { return wood; } internal set { wood = Mathf.Clamp(value, 0, MaxCapacity); } }

    [SerializeField]
    float coal = 100;
    public float Coal { get { return coal; } }

    [SerializeField]
    float food = 100;
    public float Food { get { return food; } }

    [Header("Buildings")]

    [SerializeField]
    List<Building> buildings;


    [Header("UI")]

    [SerializeField]
    Image copperBar;
    [SerializeField]
    Text copperText;

    [SerializeField]
    Image woodBar;
    [SerializeField]
    Text woodText;

    [SerializeField]
    Text coalText;
    [SerializeField]
    Text foodText;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        food = 0;
        coal = 0;

        foreach (Building b in buildings)
        {
            //Prod
            switch(b.ProductionType)
            {
                case ProductionType.copper:
                    Copper += b.GetProd(Time.deltaTime);
                    break;
                case ProductionType.wood:
                    Wood += b.GetProd(Time.deltaTime);
                    break;

                case ProductionType.food:
                    food += b.GetProd(1);
                    break;

                case ProductionType.coal:
                    coal += b.GetProd(1);
                    break;
            }

            //Costs
            food -= b.GetFoodComsuption();
            coal -= b.GetCoalConsumption();

        }

        updateBar(copperBar, copperText, Copper);
        updateBar(woodBar, woodText, Wood);

        foodText.text = food.ToString("N0");
        coalText.text = coal.ToString("N0");
	}


    void updateBar(Image bar, Text text, float amount)
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, amount / maxCapacity, Time.deltaTime);
        StringBuilder builder = new StringBuilder();
        text.text = builder.Append(amount.ToString("N0")).Append(" / ").Append(MaxCapacity.ToString("N0")).ToString();
    }



    public bool buy(ProductionType resource, int amount)
    {
        switch (resource)
        {
            case ProductionType.copper:
                if (amount <= Copper)
                {
                    Copper -= amount;
                    return true;
                }
                return false;

            default:
                return false;
        }
    }
}

