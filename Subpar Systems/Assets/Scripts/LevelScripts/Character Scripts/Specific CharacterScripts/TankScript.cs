using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : GenericCharacterScript {

	// Use this for initialization
	void Start () {
		currentTraits = null;
		Name = "Tank";
		role = "Survive";
		movement = 0;
		RefreshActions();
	}

	// Update is called once per frame
	void Update() {

	}

	public override void OnMouseOver() {
		//do nothing
	}
}