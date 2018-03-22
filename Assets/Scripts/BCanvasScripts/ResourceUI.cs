using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {

    [SerializeField]
    Text productionText;
    [SerializeField]
    ProductionType resourceType;
    [SerializeField]
    bool bySecond;

    // Use this for initialization
    void Update () {
        StringBuilder builder = new StringBuilder();
        builder.Append("Producing : ").Append(GetComponent<Building>().GetProd(1).ToString("N0")).Append(" ");

        if(bySecond)
        {
            builder.Append("/s ");
        }

        productionText.text = builder.Append(resourceType.ToString()).ToString();
	}
}
