﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character11Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(10);
        name = "character11";
        RefreshActions();
        ModifyStats();
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