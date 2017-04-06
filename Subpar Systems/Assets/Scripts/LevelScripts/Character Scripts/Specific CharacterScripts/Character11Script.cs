﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character11Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(10);
        Name = "Larry Winters";
        role = "Machine Gunner";
        RefreshActions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnMouseOver()
    {
        base.OnMouseOver();
    }
}