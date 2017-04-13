﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character5Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(4);
        Name = "Geoff";
        role = "Grenadier";
        StartCoroutine(RefreshActions());
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