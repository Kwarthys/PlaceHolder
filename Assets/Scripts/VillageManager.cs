using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ProductionType
{
    cuivre, bois, charbon, nouriture
};

public class VillageManager : MonoBehaviour {

    public static VillageManager instance { get; internal set; }

    [Header("Ressouces")]

    [SerializeField]
    float maxCapacity = 200;
    public float MaxCapacity { get { return maxCapacity; }}

    [SerializeField]
    float cuivre = 100;
    public float Cuivre { get { return cuivre; } internal set { cuivre = Mathf.Clamp(value, 0, MaxCapacity); } }

    [SerializeField]
    float bois = 100;
    public float Bois { get { return bois; }}

    [SerializeField]
    float charbon = 100;
    public float Charbon { get { return charbon; }}

    [SerializeField]
    float nourriture = 100;
    public float Nourriture { get { return nourriture; }}


    [SerializeField]
    List<Batiment> bats;


    [Header("UI")]
    [SerializeField]
    Image cuivreBar;
    [SerializeField]
    Text cuivreText;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Batiment b in bats)
        {
            switch(b.ProductionType)
            {
                case ProductionType.cuivre:
                    Cuivre += b.GetProd(Time.deltaTime);
                    break;
            }
        }

        cuivreBar.fillAmount = Mathf.Lerp(cuivreBar.fillAmount, Cuivre / maxCapacity, Time.deltaTime);
        StringBuilder builder = new StringBuilder();
        cuivreText.text = builder.Append(Cuivre.ToString("N0")).Append(" / ").Append(MaxCapacity.ToString("N0")).ToString();
	}
}

