using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Script : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		WimpTrait temp = new WimpTrait ();
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
