using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : GenericCharacterScript {

	private bool canAttack = false;

	// Use this for initialization
	void Start () {
		currentTraits = GameControlScript.control.GetTraitsOfACharacter(12);
		//Debug.Log (currentTraits.Count + " TankScript Traits count");
		Name = "Tank";
		role = "Survive";
		movement = 0;
		range = 4;
        hp = 1000;
        if (GameControlScript.control.GetSideMission1Result () == 1) {
			canAttack = true;
		}
        StartCoroutine(RefreshActions());
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

	public override void HPLost (int hpLost)
	{
		hp -= hpLost;
		bool stopFromDieing = false;
		bool check = false;
		for (int i = 0; i < currentTraits.Count; ++i) {
			check = currentTraits[i].StopFromDieing();
			//if stop from dieing is true, set stop from dieng to true
			if (check) {
				stopFromDieing = true;
			}
		}
		//if hp <= 0 but stop from dieing true, set hp to 1
		if (stopFromDieing && hp <= 0) {
			hp = 1;
		}
		if (hp <= 0) {
			GameControlScript.control.CharacterDied(this.gameObject);
			//this after gamecontrol call since levelControl call relies on game control count
			LevelControlScript.control.PlayerDied ();

			//if the tank dies the level needs to fail
			CompleteLevelConditions.control.LevelFailed ();

			Destroy(this.gameObject);
		}

		//update player health
		ShowHealthOnPlayer();
	}

	public void SetCanAttack(bool setTo) {
		canAttack = setTo;
	}
}