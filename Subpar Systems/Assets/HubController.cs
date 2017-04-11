using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubController : MonoBehaviour {
    int missonlevel = 2;
	// Use this for initialization
	void Start () {
        missonlevel = GameControlScript.control.GetLevel();
        Debug.Log(missonlevel);
        if (missonlevel == 1)
        {
            var scene = GameObject.FindWithTag("Level 1");
            scene.GetComponent<Canvas>().enabled = true;
        }
        else if (missonlevel == 2)
        {
            var scene = GameObject.FindWithTag("Level 2");
            scene.GetComponent<Canvas>().enabled = true;
            //activate scenes for 2
        }
        else
        {
            //level 3
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
