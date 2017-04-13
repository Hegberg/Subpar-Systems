﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character8Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(7);
        Name = "Annie Winters";
        role = "Assault";
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