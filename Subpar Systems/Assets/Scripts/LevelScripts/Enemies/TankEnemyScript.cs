using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyScript : GenericEnemyScript {

	// Use this for initialization
	void Start () {
		hp = 500;
        maxHP = 500;
        attack = 250;
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
