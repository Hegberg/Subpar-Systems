using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character9Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(8);
        name = "Jai Ono";
        role = "Assault";
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