using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		currentTraits = GameControlScript.control.GetTraitsOfACharacter (0);
		Name = "Taliyah";
        role = "Riflewoman";
        RefreshActions();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnMouseOver ()
	{
		base.OnMouseOver ();
	}
}
