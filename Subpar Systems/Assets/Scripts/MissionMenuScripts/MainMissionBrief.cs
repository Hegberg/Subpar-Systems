using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMissionBrief : MonoBehaviour {
    Text mission;
    public int missonlevel = 0;
    int level;
    // Use this for initialization
    void Start () { //will be changed after prototype to grab right info
        mission = GetComponent<Text>();
        missonlevel = GameControlScript.control.GetLevel();
//should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "Sir, I understand you just arrived but we have an issue that requires your immediate attention. Nests of Swarmers and Slimes have been spotted on the south-east corner of the Forest sector. It is imperative that we eliminate all hostile life in the location so we can begin the extraction of mineral resources from the area. ";
        }
        if (missonlevel == 2)
        {
            mission.text = "Our tanks are worth too much to be left behind, and reports have shown that the damage the tank recieved has only immobilized it. Send a team to protect the tank and kill all creatures nearby.";
        }
        if (missonlevel == 3)
        {
            mission.text = "A large swarm of monsters is charging towards a portal nearby. We must prevent any monsters from getting through. ";
        }
    }
	
    // Update is called once per frame
	void Update () {
		
	}
}
