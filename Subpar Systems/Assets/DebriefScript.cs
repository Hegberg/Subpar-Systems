using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebriefScript : MonoBehaviour {
    Text mission;
    int missonlevel = 0;
    int sideSuccess;
    //string sideMission;
    // Use this for initialization
    void Start () {
        mission = GetComponent<Text>();
        missonlevel += 1; //should be changed to getlevel
        //sideSuccess = SideMissionScript.control.runSideMission1();
        //Debug.Log(sideSuccess);

        if (missonlevel == 1)
        {
            /*
            if (sideSuccess == 1)
            {
                //sideMission = "Side mission passed.";
                mission.text = "The Monster nests have been eliminated, and side mission passed. Mission Success.";
            }
            else
            {
                //sideMission = "Side mission failed though.";
                mission.text = "The Monster nests have been eliminated, but side mission failed. Mission Success.";
            }
            */
            if (Random.Range(0, 2) >= 1) { 
              mission.text = "The Monster nests have been eliminated. Mission Success. The teams sent to extract the tank crew were successful.";//need a way to track side missions/char death/ traits unlocked to display
            }
            else
            {
                mission.text = "The Monster nests have been eliminated. Mission Success. The teams sent to extract the tank crew failed, but returned unharmed.";//need a way to track side missions/char death/ traits unlocked to display

            }
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
