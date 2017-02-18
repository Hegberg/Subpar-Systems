﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		currentTraits = GameControlScript.control.GetTraitsOfACharacter (1);
		name = "M40";
        RefreshActions();
        ModifyStats();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnMouseOver ()
	{
		base.OnMouseOver ();
	}
}
