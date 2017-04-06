using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character5Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(4);
        name = "Géoff";
        role = "Grenadier"; 
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