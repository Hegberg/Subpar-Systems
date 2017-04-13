using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sidemissionbio : MonoBehaviour {
    Text mission;
    int missonlevel = 0;
    int level;
    // Use this for initialization
    void Start()
    { //will be changed after prototype to grab right info
        mission = GetComponent<Text>();
        missonlevel = GameControlScript.control.GetLevel();
        //should be changed to getlevel
        if (missonlevel == 1)
        {
            mission.text = "An imperial tank was damaged in the latest of several skirmishes with the indigenous populations. The tank’s crew escaped, but is currently holed up in a cave and will require immediate extraction. Time is of the essence.";
        }
        if (missonlevel == 2)
        {
            mission.text = "Several smoke signals have been spotted by our scouts. Send a squad or two to investigate.";
        }
        if (missonlevel == 3)
        {
            mission.text = "A number of Ultra’s and  have been spotted in the backlines of the enemy forces. We should send a strike team to attempt to lure these ultra's away, otherwise we may have to face them all at once. ";
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
