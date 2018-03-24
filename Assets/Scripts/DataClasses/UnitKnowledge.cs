using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitKnowledge
{
    public static Dictionary<ArmyType, int[]> resourcesCosts = buildCosts();
    public static Dictionary<ArmyType, ProductionType> workforceType = buildTypes();

    private static Dictionary<ArmyType, ProductionType> buildTypes()
    {
        Dictionary<ArmyType, ProductionType> workforceType = new Dictionary<ArmyType, ProductionType>();

        workforceType[ArmyType.bomber] = ProductionType.siegeWorkforce;
        workforceType[ArmyType.artillery] = ProductionType.siegeWorkforce;
        workforceType[ArmyType.A3] = ProductionType.airWorkforce;
        workforceType[ArmyType.A2] = ProductionType.airWorkforce;
        workforceType[ArmyType.A1] = ProductionType.airWorkforce;
        workforceType[ArmyType.destroyer] = ProductionType.groundWorkforce;
        workforceType[ArmyType.frigate] = ProductionType.groundWorkforce;
        workforceType[ArmyType.cutter] = ProductionType.groundWorkforce;

        return workforceType;
    }

    private static Dictionary<ArmyType, int[]> buildCosts()
    {
        Dictionary<ArmyType, int[]> resourcesCosts = new Dictionary<ArmyType, int[]>();

        resourcesCosts[ArmyType.bomber] = new int[] { 200, 200, 200 };
        resourcesCosts[ArmyType.artillery] = new int[] { 200, 200, 200 };
        resourcesCosts[ArmyType.A3] = new int[] { 200, 200, 200 };
        resourcesCosts[ArmyType.A2] = new int[] { 200, 200, 200 };
        resourcesCosts[ArmyType.A1] = new int[] { 200, 200, 200 };
        resourcesCosts[ArmyType.destroyer] = new int[] { 100, 100, 7 };
        resourcesCosts[ArmyType.frigate] = new int[] { 70, 70, 5 };
        resourcesCosts[ArmyType.cutter] = new int[] { 50, 10, 2 };

        return resourcesCosts;
    }
}
