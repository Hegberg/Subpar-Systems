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
        missonlevel = GameControlScript.control.GetLevel() + 1;
        //should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "Search and Destroy";
        }
        if (missonlevel == 2)
        {
            mission.text = "Hardwired";
        }
        if (missonlevel == 3)
        {
            mission.text = "All Nightmare Long";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
