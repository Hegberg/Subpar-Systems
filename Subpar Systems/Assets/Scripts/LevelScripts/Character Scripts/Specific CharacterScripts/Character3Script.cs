using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		MalnourishedTrait temp = new MalnourishedTrait ();
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
