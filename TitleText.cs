using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour {
    Text mission;
	// Use this for initialization
	void Start () { //will be changed after prototype to grab right info
        mission = GetComponent<Text>();
        mission.text = "Search and Destroy";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
