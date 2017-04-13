using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : GenericEnemyScript {

	// Use this for initialization
	void Start () {
		hp = 50;
        maxHP = 50;
		attack = 25;
		movement = 6;
		range = 1;
        if (GameControlScript.control.GetLevel() == 3)
        {
            DetectRadius = 100;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
