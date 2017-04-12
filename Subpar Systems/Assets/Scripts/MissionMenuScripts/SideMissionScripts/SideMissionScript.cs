using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMissionScript : MonoBehaviour {
	private static List<GenericTraitsScript> missionTraits = new List<GenericTraitsScript>();
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
            //sideCharactersBool = GameControlScript.control.GetSideMissionChosen();
            //determine character to spawn
			DontDestroyOnLoad(this.gameObject);
        }

     
    }
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator getMissionTraits()
    {
        //Used to get traits on current mission.
        
		yield return new WaitForSeconds(1.0f);
         
		sideCharacters = GameControlScript.control.GetInGameSideCharacterList();
		List<GenericTraitsScript> characterTraits = new List<GenericTraitsScript> {};
		//Debug.Log (sideCharacters.Count + " side characters count");
		//Debug.Log (sideCharacters [0].GetComponent<GenericCharacterScript> ().GetName () + " side character 1 name");
		//Debug.Log (sideCharacters [1].GetComponent<GenericCharacterScript> ().GetName () + " side character 2 name");
        for (int i = 0; i < sideCharacters.Count; ++i)
        {

            characterTraits = sideCharacters[i].GetComponent<GenericCharacterScript>().GetTraits();
			//Debug.Log (characterTraits.Count + " character list object");
			//Debug.Log (sideCharacters[i].GetComponent<GenericCharacterScript>().GetTraits().Count + " character list object 2");
            for (int k = 0; k < characterTraits.Count; ++k)
            {
                missionTraits.Add(characterTraits[k]);
            }
			characterTraits.Clear ();
        }

		int sideMissionResult;
		if (GameControlScript.control.GetLevel () == 1) {
			sideMissionResult = runSideMission1Calculation ();
			GameControlScript.control.SetSideMissionResult (1, sideMissionResult);
		} else if (GameControlScript.control.GetLevel () == 2) {
			sideMissionResult = runSideMission1Calculation ();
			GameControlScript.control.SetSideMissionResult (2, sideMissionResult);
		} else if (GameControlScript.control.GetLevel () == 3) {
			sideMissionResult = runSideMission1Calculation ();
			GameControlScript.control.SetSideMissionResult (3, sideMissionResult);
		}
       
    }

    float runProbability(float successProb)
    {
        //Returns a float to see by just how much the team has succeedded or failed,
        //This is if we later have missions that have degrees of passing or failing.
        float rand = Random.Range(0.0f, 100.0f);
        return (successProb - rand);
    }

	public void runSideMission() {
		StartCoroutine (getMissionTraits());
	}

    public int runSideMission1Calculation()
    {
        /* SIDE MISSION 1:
         * An imperial tank was lost in the latest of several skirmishes with the indigenous populations. 
         * The tank’s crew escaped, but is currently holed up in a cave and will require extraction. 
         * (tank crew will help with next mission) [up to 2 officers]
         */
        //before this is called, you must pass units on current mission. 
        //returns 1 for success, 0 for fail. Using int so we can have different results on a mission, instead
        //of just pass or fail
        
        //Debug.Log(sideCharacters);
        bool assault = false;
        bool rifleman = false;
        float success = 0.0f;
        if (sideCharacters.Count == 0 )
        {
            //Run mission failed code since no one was sent.
            //Debug.Log("No one detected sent on the mission.");

            return 0;
        }  
        else
        {
            //Problem, traits wont stack with certain implementation.
            //UNTILL THEN, NOT DEALING WITH IT.
            //CURRENT PERCENTAGES: 1 ASSAULT 1 Riflemen = 100.
            //getMissionTraits();
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
            //Debug.Log("Success Prob");
            //Debug.Log(successProb);
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
    public int runSideMission2Calculation()
    {
        //A signal fire can be seen off in the distance. Send a team to investigate.
        if (sideCharacters.Count == 0)
        {
            //Run mission failed code since no one was sent.
            return 0;
        }
        //if murphy sent, fail the mission with unique text.

        //
        return 1;

    }
    public int runSideMission3Calculation()
    {
        //Lure away enemy Ultras
        if (sideCharacters.Count == 0)
        {
            //Run mission failed code since no one was sent.
            //Debug.Log("No one detected sent on the mission.");

            return 0;
        }
        //Patriotism trait gives bonuses.

        return 1;
    }
}

