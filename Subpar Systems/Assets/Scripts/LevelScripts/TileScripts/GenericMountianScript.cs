﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMountianScript : TileScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool CanPlayerMoveHere()
    {
        return false;
    }
}