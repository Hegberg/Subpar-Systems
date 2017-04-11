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
            mission.text = "An imperial tank was lost in the latest of several skirmishes with the indigenous populations. The tank’s crew escaped, but is currently holed up in a cave and will require extraction.";
        }
        if (missonlevel == 2)
        {
            mission.text = "We have also discerned the location of one Sir Frederick McGaffin, a veritable genius in the world of flora and fauna. He may be able to give us insight into the region and where we should next focus our attacks. A small team could extract him from his research base and bring him here for questioning.";
        }
        if (missonlevel == 3)
        {
            mission.text = "The enemy force coming toward the front gate is huge, though it is nowhere near as big as our reports indicated. It can be assumed those forces will be attacking the base from another direction. We should station some officers on our perimeter to hold off alternative enemy attacks. ";
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
