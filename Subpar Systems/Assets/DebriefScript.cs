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
        missonlevel = GameControlScript.control.GetLevel() - 1;
        //Debug.Log(missonlevel);
        //should be changed to getlevel
        //sideSuccess = SideMissionScript.control.runSideMission1();
        //Debug.Log(sideSuccess);

        if (missonlevel == 1)
        {
			int sideMissionSuccess = GameControlScript.control.GetSideMission1Result ();
			if (sideMissionSuccess == 1)
            {
                //sideMission = "Side mission passed.";
                mission.text = "The Monsters have been eliminated, and the Tank Crew was successfully retrieved. You will be able to control a tank in the next mission. Mission Success. ";
            }
            else
            {
                //sideMission = "Side mission failed though.";
                mission.text = "The Monsters have been eliminated, but the tank crew was not able to be rescued. Mission Success.";
            }
            /*
            if (Random.Range(0, 2) >= 1) { 
              mission.text = "The Monster nests have been eliminated. Mission Success. The teams sent to extract the tank crew were successful.";//need a way to track side missions/char death/ traits unlocked to display
            }
            else
            {
                mission.text = "The Monster nests have been eliminated. Mission Success. The teams sent to extract the tank crew failed, but returned unharmed.";//need a way to track side missions/char death/ traits unlocked to display

            }
            */
        }
        if (missonlevel == 2)
        {
            int sideMissionSuccess = GameControlScript.control.GetSideMission2Result();
            if (sideMissionSuccess == 1)
            {
                mission.text = "The tank was secured. The smoke signals were from Canuckistan soldiers. They were sympathetic to our cause, and warned us about a plan to herd monsters through a recently opened portal. This portal appeared next to a major population center. You must stop this.";
            }
            if (sideMissionSuccess == 0)
            {
                mission.text = "The tank was secured. There was signs of Canuckistan soldiers, however when your soldiers arrived they could not find anything. Mission Success.";

            }
            if (sideMissionSuccess == -1)
            {
                mission.text = "The tank was secured. The smoke signals were from Canuckistan soldiers. George Murphy found the Canuckistan soldiers, and swiftly elimanted them. Mission Success.";
            }
        } 
        if (missonlevel == 3)
        {
            mission.text = "Well done, Commander. We're moving some extra forces into the area to relieve the pressure from your base. We have no further assignments for you at this time. Mission Success.";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
