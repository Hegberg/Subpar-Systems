using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character4Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		currentTraits = GameControlScript.control.GetTraitsOfACharacter (3);
        Name = "Roy LeGaul";
        role = "Rifleman";
        StartCoroutine(RefreshActions());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnMouseOver ()
	{
		base.OnMouseOver ();
	}
}
