using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {

    public static GameControler controler;

    PlayerData data;
    
	void Awake () {
		if(controler == null)
        {
            DontDestroyOnLoad(gameObject);
            controler = this;
        }
        else if(controler != this)
        {
            Destroy(gameObject);
        }
	}

    private void Start()
    {
        data = new PlayerData();
        data.name = "perdu";
        data.points = 10;
    }
}

class PlayerData
{
    public string name;
    public int points;
}