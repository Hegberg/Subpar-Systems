using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character10Script : GenericCharacterScript
{

    // Use this for initialization
    void Start()
    {
        currentTraits = GameControlScript.control.GetTraitsOfACharacter(9);
        Name = "Yuri Sokolov";
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