using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batiment : MonoBehaviour
{
    [SerializeField]
    ProductionType productionType;
    public ProductionType ProductionType { get { return productionType; } }
    [SerializeField]
    float prodPerSecond;

    public float GetProd(float time)
    {
        return time * prodPerSecond;
    }
}
