using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActivity : MonoBehaviour {

    [SerializeField]
    List<GameObject> toToggle = new List<GameObject>();

    public bool activated = false;

    private void Update()
    {
        Building b = GetComponent<Building>();

        if(b != null)
        {
            if(activated && b.recruitment.Count == 0)
            {
                deactivate();
            }
            else if(!activated && b.recruitment.Count > 0)
            {
                activate();
            }
        }
    }

    private void Start()
    {
        toggle();
    }

    void activate () {
        activated = true;

        toggle();
	}

    void deactivate()
    {
        activated = false;

        toggle();
    }

    void toggle()
    {
        foreach (GameObject g in toToggle)
        {
            g.SetActive(activated);
        }
    }
}
