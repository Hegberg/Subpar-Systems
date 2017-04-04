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
        missonlevel = GameControlScript.control.GetLevel() + 1;
//should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "Sir, nests of Swarmers and Slimes have been spotted on the south-east corner of the Forest sector. It is imperative that we eliminate all hostile life in the location so we can begin the extraction of mineral resources from the area. ";
        }
        if (missonlevel == 2)
        {
            mission.text = "The extraction zone has been cleared of hostiles, however we will need all the firepower we can acquire to complete our objectives. The recently lost tank would provide us with excellent artillery support, and reports have shown that the tank hasn’t moved from where it was first lost. We must escort an engineering team to the tank and hold its position until they fix it.";
        }
        if (missonlevel == 3)
        {
            mission.text = "Enemy forces have engaged the extraction zone. We must hold the main gate while we wait for the portal to open. ";
        }
    }
	
    // Update is called once per frame
	void Update () {
		
	}
}
