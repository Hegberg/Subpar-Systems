using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : GenericCharacterScript {

	private bool canAttack = true;

	// Use this for initialization
	void Start () {
		currentTraits = GameControlScript.control.GetTraitsOfACharacter(12);
		//Debug.Log (currentTraits.Count + " TankScript Traits count");
		Name = "Tank";
		role = "Survive";
		movement = 0;
		range = 4;
		RefreshActions();
	}

	// Update is called once per frame
	void Update() {

	}

	public override void OnMouseOver ()
	{
		if (canAttack) {
			base.OnMouseOver ();
		}
	}

	public void SetCanAttack(bool setTo) {
		canAttack = setTo;
	}
}