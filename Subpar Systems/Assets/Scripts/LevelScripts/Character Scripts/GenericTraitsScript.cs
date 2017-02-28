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

public class F27BadWithM40Trait : GenericTraitsScript {
	//small general debuff between the 2 characters
	public override void InitializeValues ()
	{
		name = "F27BadWithM40";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M40") {
				attackModifier = 0.8f;
				defenseModifier = 0.8f;
				break;
			}
		}
	}
}

public class F32GoodWithM41Trait : GenericTraitsScript {
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "F32GoodWithM41";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M41") {
				attackModifier = 1.2f;
				defenseModifier = 1.2f;
				break;
			}
		}
	}
}

public class M31GoodWithM29Trait : GenericTraitsScript {
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "M31GoodWithM29";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M29") {
				attackModifier = 1.2f;
				defenseModifier = 1.2f;
				break;
			}
		}
	}
}

public class M31MarriedToF32Trait : GenericTraitsScript {
	//defense buff between the 2 characters (to protect each other
	public override void InitializeValues ()
	{
		name = "M31MarriedToF32";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F32") {
				attackModifier = 1.0f;
				defenseModifier = 1.5f;
				break;
			}
		}
	}
}

public class M31FriendM29DeadTrait : GenericTraitsScript {
	//massive debuff if M29 dies
	public override void InitializeValues ()
	{
		name = "M31FriendM29Dead";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "M29") {
				attackModifier = 0.45f;
				defenseModifier = 0.45f;
				break;
			}
		}
	}
}

public class M31WifeF32DeadTrait : GenericTraitsScript {
	//massive debuff if F32 dies
	public override void InitializeValues ()
	{
		name = "M31WifeF32Dead";
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "F32") {
				attackModifier = 0.45f;
				defenseModifier = 0.45f;
				break;
			}
		}
	}
}

public class AdrenalineJunky : GenericTraitsScript {
	//bonus attack if moves max tiles
	public override void InitializeValues ()
	{
		name = "Adrenaline Junky";

	}
}

public class RiflemanTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Rifleman";
    }
}

public class GrenedierTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Grenedier";
    }
}

public class AssaultTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Assault";
    }
}