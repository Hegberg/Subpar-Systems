using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameControlScript.control.SpawnCharacters();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
