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
	protected int positionInSpriteUIList = 0;
	protected bool isCrazy = false;

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

    public virtual string GetName()
    {
        return name;
    }
		
	public virtual int GetPositionInSpriteControlList(){
		return positionInSpriteUIList;
	}

	public virtual bool GetIfCrazy() {
		return isCrazy;
	}

	public virtual void AttemptToSetCrazy(){
		//Traits that have something die set crazy to true here
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
        attackModifier = 0.70f; //33 above
        defenseModifier = 1.0f; //not affected
        movementModifier = 0.5f; //2
        rangeModifier = 1.5f; //6
        //I wanted machineGunners to deal regular damage, but if they can
        //move and shoot twice I'm going to reduce it.
        positionInSpriteUIList = 0;
	}
}

public class BrutalEfficiencyTrait : GenericTraitsScript {
	public override void InitializeValues ()
	{
		name = "Brutal Efficiency";
		defenseModifier = 0.5f;
		positionInSpriteUIList = 1;
	}
}

public class BacklineCommanderTrait : GenericTraitsScript {
	public override void InitializeValues ()
	{
		name = "Backline Commander";
		positionInSpriteUIList = 2;
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
		positionInSpriteUIList = 3;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F25") {
				attackModifier = 1.2f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F25") {
				defenseModifier = 1.2f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class F27BadWithM40Trait : GenericTraitsScript {
	//small general debuff between the 2 characters
	public override void InitializeValues ()
	{
		name = "F27BadWithM40";
		positionInSpriteUIList = 4;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M40") {
				attackModifier = 0.8f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M40") {
				defenseModifier = 0.8f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class F32GoodWithM41Trait : GenericTraitsScript {
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "F32GoodWithM41";
		positionInSpriteUIList = 5;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M41") {
				attackModifier = 1.2f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M41") {
				defenseModifier = 1.2f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class M31GoodWithM29Trait : GenericTraitsScript {
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "M31GoodWithM29";
		positionInSpriteUIList = 6;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M29") {
				attackModifier = 1.2f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "M29") {
				defenseModifier = 1.2f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class M31MarriedToF32Trait : GenericTraitsScript {
	//defense buff between the 2 characters (to protect each other
	public override void InitializeValues ()
	{
		name = "M31MarriedToF32";
		positionInSpriteUIList = 7;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F32") {
				defenseModifier = 1.5f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class M31FriendM29DeadTrait : GenericTraitsScript {
	//massive debuff if M29 dies
	public override void InitializeValues ()
	{
		name = "M31FriendM29Dead";
		positionInSpriteUIList = 8;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "M29") {
				attackModifier = 0.45f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "M29") {
				defenseModifier = 0.45f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class M31WifeF32DeadTrait : GenericTraitsScript {
	//massive debuff if F32 dies
	public override void InitializeValues ()
	{
		name = "M31WifeF32Dead";
		positionInSpriteUIList = 9;
	}

	public override void AttemptToSetCrazy ()
	{
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "F32") {
				isCrazy = true;
				break;
			}
		}
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "F32") {
				attackModifier = 0.45f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters ().Count; ++i) {
			if (GameControlScript.control.GetDeadCharacters ()[i] == "F32") {
				defenseModifier = 0.45f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class AdrenalineJunky : GenericTraitsScript {
	//bonus attack if moves max tiles
	public override void InitializeValues ()
	{
		name = "Adrenaline Junky";
		positionInSpriteUIList = 10;
	}
}

public class RiflemanTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Rifleman";
        attackModifier = 1.0f; //not affected
        defenseModifier = 1.0f; //not affected
        movementModifier = 0.75f; //3 
        rangeModifier = 1.0f; //not affected
        positionInSpriteUIList = 11;
    }
}

public class GrenedierTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Grenedier";
        attackModifier = .75f; //lower
        defenseModifier = 1.0f; //not affected
        movementModifier = 0.75f; //3
        rangeModifier = 0.75f; //3
        //Need some sort of advantage against certain monsters to make it
        //so grenadiers have a role.  
        //Two ideas, make it so grenades weaken targets, or that they have
        //advantages against other targets. Testing required.
        positionInSpriteUIList = 12;
    }
}

public class AssaultTrait : GenericTraitsScript
{
    public override void InitializeValues()
    {
        name = "Assault";
        attackModifier = 1.5f; //high
        defenseModifier = 1.0f; //not affected
        movementModifier = 1.25f; //5
        rangeModifier = 0.25f; //1
        positionInSpriteUIList = 13;
    }
}

public class F29FriendsWithF28 : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "F29FriendsWithF28";
		attackModifier = 1;
		defenseModifier = 1;
		movementModifier = 1.00f; //
		rangeModifier = 1.00f; //
		positionInSpriteUIList = 14;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F28") {
				attackModifier = 1.2f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F28") {
				defenseModifier = 1.2f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class SleepDeprived : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "SleepDeprived";
		positionInSpriteUIList = 15;
	}

	//50% chance to do half damage
	public override float ModifyAttack ()
	{
		if (Random.Range (0, 1) < 0.5f) {
			attackModifier = 0.5f;
		} else {
			attackModifier = 1;
		}

		return attackModifier;
	}
}
	
public class F28FriendsWithF29 : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "F28FriendsWithF29";
		attackModifier = 1;
		defenseModifier = 1;
		movementModifier = 1.00f; //
		rangeModifier = 1.00f; //
		positionInSpriteUIList = 16;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F29") {
				attackModifier = 1.2f;
				break;
			}
		}
		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "F29") {
				defenseModifier = 1.2f;
				break;
			}
		}
		return defenseModifier;
	}
}

public class FrontLineCommander : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "FrontLineCommander";
		positionInSpriteUIList = 17;
	}
}