﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMissionScript : MonoBehaviour {
    private static List<GenericTraitsScript> missionTraits;
    private static List<GameObject> sideCharacters;
    private static List<bool> sideCharactersBool;
    private float successProb;
    public static SideMissionScript control;



    // Use this for initialization
    void Start() {
        //Assume we are given a list of characters chosen for sidemissions. Until then, just pulling from the full list. 
        //Until hooked up, this shouldn't cause problems
        //FOR EARLY USE ONLY DO NOT ALLOW IT TO REMAIN
        //characters = GameControlScript.control.GetCharacters();

        //add character prefabs
        if (control == null)
        {
            control = this;
            sideCharactersBool = GameControlScript.control.GetSideMissionChosen();
            //determine character to spawn
        }
     
    }
	
	// Update is called once per frame
	void Update () {
	}

    void getMissionTraits()
    {
        //Used to get traits on current mission.
        List<GenericTraitsScript> characterTraits;
        for (int i = 0; i < sideCharacters.Count; ++i)
        {

            characterTraits = sideCharacters[i].GetComponent<GenericCharacterScript>().GetTraits();
            for (int k = 0; k < characterTraits.Count; ++k)
            {
                missionTraits.Add(characterTraits[k]);
            }
        }

    }

    float runProbability(float successProb)
    {
        //Returns a float to see by just how much the team has succeedded or failed,
        //This is if we later have missions that have degrees of passing or failing.
        float rand = Random.Range(0.0f, 100.0f);
        return (successProb - rand);
    }
    void selectCurrentUnits(List<GameObject> selectedCharacters)
    {
        //getUnits for current mission.
        //sideCharacters = selectedCharacters;
    }
    public int runSideMission1()
    {
        /* SIDE MISSION 1:
         * An imperial tank was lost in the latest of several skirmishes with the indigenous populations. 
         * The tank’s crew escaped, but is currently holed up in a cave and will require extraction. 
         * (tank crew will help with next mission) [up to 2 officers]
         */
        //before this is called, you must pass units on current mission. 
        //returns 1 for success, 0 for fail. Using int so we can have different results on a mission, instead
        //of just pass or fail

        bool assault = false;
        bool rifleman = false;
        float success = 0.0f;
        if (sideCharacters.Count == 0 )
        {
            //Run mission failed code since no one was sent.
            return 0;
        }  
        else
        {
            //Problem, traits wont stack with certain implementation.
            //UNTILL THEN, NOT DEALING WITH IT.
            //CURRENT PERCENTAGES: 1 ASSAULT 1 Riflemen = 100.
            getMissionTraits();
            successProb = 60.0f;
            for (int i = 0; i < missionTraits.Count; ++i)
            {
                if (missionTraits[i].GetName() == "Rifleman")
                {
                    rifleman = true;
                    successProb += 12.5f;
                }
                if (missionTraits[i].GetName() == "Assault")
                {

                    assault = true;
                    successProb += 15.0f;
                }
                if (missionTraits[i].GetName() == "Grenedier") successProb -= 5.0f;
                if (missionTraits[i].GetName() == "Machine Gun") successProb -= 10.0f;
            }
            if (assault && rifleman) successProb = 100.0f;

            success = runProbability(successProb);
            if (success >= 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

