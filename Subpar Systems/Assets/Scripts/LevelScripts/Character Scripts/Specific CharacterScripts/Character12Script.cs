﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character12Script : GenericCharacterScript {

    // Use this for initialization
    void Start () {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(11);
        name = "character12";
        RefreshActions();
        ModifyStats();
    }

    // Update is called once per frame
    void Update() {

    }

    public override void OnMouseOver() {
        base.OnMouseOver();
    }
}