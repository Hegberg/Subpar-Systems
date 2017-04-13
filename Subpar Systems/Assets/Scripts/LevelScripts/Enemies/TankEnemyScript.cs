using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyScript : GenericEnemyScript {

	// Use this for initialization
	void Start () {
		hp = 350;
        maxHP = 350;
        attack = 75;
		movement = 2;
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
