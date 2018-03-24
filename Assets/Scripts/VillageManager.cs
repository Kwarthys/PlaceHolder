using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ProductionType
{
    copper, wood, coal, food, groundWorkforce, airWorkforce, siegeWorkforce
};


public enum ArmyType
{
    cutter, frigate, destroyer, A1, A2, A3, artillery, bomber
};

public class RecruitmentOrder
{
    public ArmyType type;
    public int number;
    public float jobDone = 0;

    public RecruitmentOrder(ArmyType type, int howMany)
    {
        this.type = type;
        this.number = howMany;
    }
}

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

    [SerializeField]
    Dictionary<ArmyType, int> army = new Dictionary<ArmyType, int>();

    [SerializeField]
    List<RecruitmentOrder> recruitment = new List<RecruitmentOrder>();

    [Header("Sprites")]

    [SerializeField]
    List<Sprite> unitLogos;

    [Header("Buildings")]

    [SerializeField]
    List<Building> buildings;

    public bool canvasOpened;


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


    [SerializeField]
    Image ArmyImageHolder;
    [SerializeField]
    GameObject UIUnitPrefab;
    [SerializeField]
    Dictionary<ArmyType, GameObject> uiInstanciated = new Dictionary<ArmyType, GameObject>();
    [SerializeField]
    int uiOffset = 0;
    [SerializeField]
    int uiOffsetStep = 70;

    // Use this for initialization
    void Start () {
        instance = this;
        //galley, frigate, destroyer, A1, A2, A3, artillery, bomber
        //Imposing the wanted order
        army[ArmyType.bomber] = 0;
        army[ArmyType.artillery] = 0;
        army[ArmyType.A3] = 0;
        army[ArmyType.A2] = 0;
        army[ArmyType.A1] = 0;
        army[ArmyType.destroyer] = 0;
        army[ArmyType.frigate] = 0;
        army[ArmyType.cutter] = 0;
    }
	
	// Update is called once per frame
	void Update () {

        refreshArmyUI();

        food = 0;
        coal = 0;

        float groundWorkforce = 0;
        float airWorkforce = 0;
        float siegeWorkforce = 0;

        foreach (Building b in buildings)
        {

            //PRODUCTION
            switch (b.ProductionType)
            {
                //Resources
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

                //Army
                case ProductionType.groundWorkforce:
                    groundWorkforce += b.GetProd(Time.deltaTime);
                    break;

                case ProductionType.airWorkforce:
                    airWorkforce += b.GetProd(Time.deltaTime);
                    break;

                case ProductionType.siegeWorkforce:
                    siegeWorkforce += b.GetProd(Time.deltaTime);
                    break;
            }

            //Costs
            food -= b.GetFoodComsuption();
            coal -= b.GetCoalConsumption();

        }

        List<RecruitmentOrder> toRemove = new List<RecruitmentOrder>();

        Debug.Log(recruitment.Count);

        foreach (RecruitmentOrder o in recruitment)
        {
            //ADD WORK FORCE TO ORDERS
            switch (UnitKnowledge.workforceType[o.type])
            {
                case ProductionType.groundWorkforce:
                    o.jobDone += groundWorkforce;
                    groundWorkforce = 0;
                    break;

                case ProductionType.airWorkforce:
                    o.jobDone += airWorkforce;
                    airWorkforce = 0;
                    break;

                case ProductionType.siegeWorkforce:
                    o.jobDone += siegeWorkforce;
                    siegeWorkforce = 0;
                    break;
            }

            while(o.jobDone - UnitKnowledge.resourcesCosts[o.type][2] >= 0)
            {
                o.jobDone -= UnitKnowledge.resourcesCosts[o.type][2];
                o.number--;

                addUnit(o.type, 1);

                if (o.number == 0)
                    break;
            }

            if (o.number == 0)
            {
                switch (UnitKnowledge.workforceType[o.type])
                {
                    case ProductionType.groundWorkforce:
                         groundWorkforce += o.jobDone;
                        break;

                    case ProductionType.airWorkforce:
                         airWorkforce += o.jobDone;
                        break;

                    case ProductionType.siegeWorkforce:
                        siegeWorkforce += o.jobDone;
                        break;
                }
                toRemove.Add(o);
            }

            if (groundWorkforce == 0 && airWorkforce == 0 && siegeWorkforce == 0)
            {
                break;
            }
        }

        foreach(RecruitmentOrder o in toRemove)
        {
            recruitment.Remove(o);
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
        text.text = builder.Append(Math.Truncate(amount).ToString("N0")).Append(" / ").Append(MaxCapacity.ToString("N0")).ToString();
    }


    void refreshArmyUI()
    {
        if( ArmyImageHolder != null && UIUnitPrefab != null )
        {
            int index = 7;
            foreach(KeyValuePair<ArmyType, int> item in army)
            {
                //item.key // item.value
                if(uiInstanciated.ContainsKey(item.Key))
                {
                    if(item.Value > 0)
                    {
                        if(uiInstanciated[item.Key] != null)
                        {
                            GameObject ui = uiInstanciated[item.Key];
                            ui.transform.Find("Text").GetComponent<Text>().text = item.Value.ToString();
                            ui.transform.Find("Image").GetComponent<Image>().sprite = unitLogos[index];
                        }
                        else
                        {
                            GameObject ui = Instantiate(UIUnitPrefab, ArmyImageHolder.transform);

                            Vector3 pos = ui.GetComponent<RectTransform>().localPosition;
                            pos.x += uiOffset;
                            ui.GetComponent<RectTransform>().localPosition = pos;

                            uiInstanciated[item.Key] = ui;
                            uiOffset += uiOffsetStep;
                        }
                    }
                    else
                    {
                        if (uiInstanciated[item.Key] != null)
                        {
                            GameObject ui = uiInstanciated[item.Key];
                            Destroy(ui);
                            uiInstanciated[item.Key] = null;
                            uiOffset -= uiOffsetStep;
                        }
                    }
                }
                else
                {
                    uiInstanciated[item.Key] = null;
                }

                index--;
            }
        }
    }

    public bool canAfford(ArmyType unit)
    {
        if ((float)UnitKnowledge.resourcesCosts[unit][0] <= Wood)
        {
            if ((float)UnitKnowledge.resourcesCosts[unit][1] <= Copper)
            {
                return true;
            }
        }

        return false;
    }

    public void addUnit(ArmyType unit, int howMany)
    {
        army[unit] += howMany;
    }

    public bool buy(ArmyType unit)
    {

        float woodAmount = (float)UnitKnowledge.resourcesCosts[unit][0];
        float copperAmount = (float)UnitKnowledge.resourcesCosts[unit][1];

        bool success = canAfford(unit);

        if(success)
        {
            Wood -= woodAmount;
            Copper -= copperAmount;

            if (army.ContainsKey(unit))
            {
                //army[unit] += 1;
                recruitment.Add(new RecruitmentOrder(unit, 1));
            }
        }

        return success;
    }
}

