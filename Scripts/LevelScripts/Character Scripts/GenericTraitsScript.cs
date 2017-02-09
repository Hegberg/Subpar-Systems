using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTraitsScript {

	protected string name = "temp";
	protected float hpModifier = 1.0f; //not affected
	protected float attackModifier = 1.0f; //not affected
	protected float defenseModifier = 1.0f; //not affected
	protected float movementModifier = 1.0f; //not affected
	protected float rangeModifier = 1.0f; //not affected

	public virtual void ShowInfo() {
		Debug.Log (" name: " + name + ", hp modifier: " +  hpModifier + ", attack modifier: " + attackModifier 
			+ ", defense modifier: " + defenseModifier + ", movement modifier: " + movementModifier + 
			", range modifier: " + rangeModifier);
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
		
}

public class AggressionTrait : GenericTraitsScript {

	public void InitializeValues() {
		name = "Agresion";
		attackModifier = 1.5f;
	}
}

public class WimpTrait : GenericTraitsScript {

	public void InitializeValues() {
		name = "Wimp";
		attackModifier = 0.0f;
		defenseModifier = 0.5f;
	}
}

public class MalnourishedTrait : GenericTraitsScript {

	public void InitializeValues() {
		name = "Malnourished";
		attackModifier = 0.5f;
		defenseModifier = 0.5f;
	}
}