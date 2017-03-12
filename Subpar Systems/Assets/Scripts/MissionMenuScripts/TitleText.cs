using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour {
    Text mission;
    int missonlevel = 0;
    // Use this for initialization
    void Start () { //will be changed after prototype to grab right info
        mission = GetComponent<Text>();
        missonlevel += 1; //should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "Search and Destroy";
        }
        else
            mission.text = "Level Name";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
