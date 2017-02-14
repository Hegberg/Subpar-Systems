﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTraitsScript {

	protected string name = "temp";
	protected float hpModifier = 1.0f; //not affected
	protected float attackModifier = 1.0f; //not affected
	protected float defenseModifier = 1.0f; //not affected
	protected float movementModifier = 1.0f; //not affected
	protected float rangeModifier = 1.0f; //not affected
	protected float numberOfAttacksModifier = 1;

	public virtual void InitializeValues() {
		name = "temp";
	}

	public virtual void ShowInfo() {
		Debug.Log (" name: " + name + ", hp modifier: " +  hpModifier + ", attack modifier: " + attackModifier 
			+ ", defense modifier: " + defenseModifier + ", movement modifier: " + movementModifier + 
			", range modifier: " + rangeModifier + ", attack amount modifier: " + numberOfAttacksModifier);
	}

	public virtual float ModifyHP() {
		return hpModifier;
	}

	public virtual float ModifyAttack(){
		return attackModifier;
	}

	public virtual float ModifyDefense(){
		return defenseModifier;
	}

	public virtual float ModifyMovement(){
		return movementModifier;
	}

	public virtual float ModifyRange(){
		return rangeModifier;
	}

	public virtual float ModifyNumOfAttacks() {
		return numberOfAttacksModifier;
	}

	public virtual bool StopFromDieing() {
		return false;
	}
		
}

public class AggressionTrait : GenericTraitsScript {

	public override void InitializeValues() {
		name = "Agresion";
		attackModifier = 1.5f;
	}
}

public class WimpTrait : GenericTraitsScript {

	public override void InitializeValues() {
		name = "Wimp";
		attackModifier = 0.0f;
		defenseModifier = 0.5f;
	}
}

public class MalnourishedTrait : GenericTraitsScript {

	public override void InitializeValues() {
		name = "Malnourished";
		attackModifier = 0.5f;
		defenseModifier = 0.5f;
	}
}

public class MachineGunTrait : GenericTraitsScript {
	public override void InitializeValues() {
		name = "Machine Gun";
		numberOfAttacksModifier = 2;
	}
}

public class BrutalEfficiencyTrait : GenericTraitsScript {
	public override void InitializeValues ()
	{
		name = "Brutal Efficiency";
		defenseModifier = 0.5f;
	}
}

public class BacklineCommanderTrait : GenericTraitsScript {
	public override void InitializeValues ()
	{
		name = "Backline Commander";
	}

	public override bool StopFromDieing ()
	{
		//if not the only one left, stop from dieing (return true)
		return GameControlScript.control.GetInGameCharacterList ().Count > 1;
	}
}

public class F27GoodWithF25Trait : GenericTraitsScript {
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "F27GoodWithF25";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F25") {
				attackModifier = 1.2f;
				defenseModifier = 1.2f;
				break;
			}
		}
	}
}