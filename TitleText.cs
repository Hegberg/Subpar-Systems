using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour {
    Text mission;
	// Use this for initialization
	void Start () {
        mission = GetComponent<Text>();
        mission.text = "Mission One";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
