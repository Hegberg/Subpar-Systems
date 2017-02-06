using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		AddTrait (GenericTraitsScript.control.GetWimp ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnMouseOver ()
	{
		base.OnMouseOver ();
	}
}
