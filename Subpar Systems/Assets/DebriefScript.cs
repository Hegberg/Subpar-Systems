using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebriefScript : MonoBehaviour {
    Text mission;
    int missonlevel = 0;
    // Use this for initialization
    void Start () {
        mission = GetComponent<Text>();
        missonlevel += 1; //should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "The Monster nests have been eliminated. Mission Success.";//need a way to track side missions/char death/ traits unlocked to display
        }
        if (missonlevel == 2)
        {
            mission.text = "The tank was secured. Mission Success. ";
        }
        if (missonlevel == 3)
        {
            mission.text = "Our forces have survived the attack. Mission Success";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
