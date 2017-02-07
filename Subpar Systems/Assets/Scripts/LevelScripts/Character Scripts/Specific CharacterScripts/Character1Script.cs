using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		AggressionTrait temp = new AggressionTrait ();
		temp.InitializeValues ();
		AddTrait (temp);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnMouseOver ()
	{
		base.OnMouseOver ();
	}
}
