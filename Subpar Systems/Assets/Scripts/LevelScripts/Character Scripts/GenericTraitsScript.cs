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
	protected int consecutiveAttacks = 0;
	protected bool safetyShield = false; //if set true, stops death for 1 turn

    //protected Sprite icon;

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

	public virtual void SetConsecutiveAttacks(int setTo) {
		consecutiveAttacks = setTo;
	}

	public virtual bool GetSafetyShield() {
		return safetyShield;
	}

	public virtual void SetSafetyShield(bool safe) {
		safetyShield = safe;
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
		attackModifier = 1.5f;
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

public class SchoolBonds : GenericTraitsScript {
	//F-28 better with F-29
	//Ashe, Sabrina
	//small general buff between the 2 characters
	public override void InitializeValues ()
	{
		name = "SchoolBonds";
		positionInSpriteUIList = 3;
	}

	public override float ModifyAttack ()
	{
		bool ashe = false;
		bool sabrina = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Ashe") {
				ashe = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Sabrina") {
				sabrina = true;
			}
		}

		if (ashe && sabrina) {
			attackModifier = 1.5f;
		}

		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		bool ashe = false;
		bool sabrina = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Ashe") {
				ashe = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Sabrina") {
				sabrina = true;
			}
		}

		if (ashe && sabrina) {
			defenseModifier = 1.5f;
		}

		return defenseModifier;
	}
}

public class PersonalGrudges: GenericTraitsScript {
	//small general debuff between the 2 characters
	//f27, m40
	//Taliyah, Geoff
	public override void InitializeValues ()
	{
		name = "PersonalGrudges";
		positionInSpriteUIList = 4;
	}

	public override float ModifyAttack ()
	{
		bool taliyah = false;
		bool geoff = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Taliyah") {
				taliyah = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Geoff") {
				geoff = true;
			}
		}

		if (taliyah && geoff) {
			attackModifier = 0.5f;
		}

		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		bool taliyah = false;
		bool geoff = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Taliyah") {
				taliyah = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Geoff") {
				geoff = true;
			}
		}

		if (taliyah && geoff) {
			defenseModifier = 0.5f;
		}

		return defenseModifier;
	}
}

public class LackOfHumour: GenericTraitsScript {
	//does not work well with Roy LeGaul
	public override void InitializeValues ()
	{
		name = "LackOfHumour";
		positionInSpriteUIList = 5;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Roy LeGaul") {
				attackModifier = 0.5f;
				break;
			}
		}

		return attackModifier;
	}

	public override float ModifyDefense ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Roy LeGaul") {
				defenseModifier = 0.5f;
				break;
			}
		}

		return defenseModifier;
	}
}

public class DistractingThoughts: GenericTraitsScript {
	//less damage with Devi Devai
	public override void InitializeValues ()
	{
		name = "Distracting Thoughts";
		positionInSpriteUIList = 6;
	}

	public override float ModifyAttack ()
	{
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Devi Devai") {
				attackModifier = 0.5f;
				break;
			}
		}

		return attackModifier;
	}
}

public class MarriedLife: GenericTraitsScript {
	//defense buff between the 2 characters (to protect each other
	//good with m31, Terry Winters
	//Annie Winters
	public override void InitializeValues ()
	{
		name = "MarriedLife";
		positionInSpriteUIList = 7;
	}

	public override float ModifyAttack ()
	{
		bool terry = false;
		bool annie = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Terry Winters") {
				terry = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Annie Winters") {
				annie = true;
			}
		}

		if (terry && annie) {
			attackModifier = 1.5f;
		}

		return attackModifier;
	}

    public override void AttemptToSetCrazy()
    {
        for (int i = 0; i < GameControlScript.control.GetDeadCharacters().Count; ++i)
        {
            if (GameControlScript.control.GetDeadCharacters()[i] == "F32")
            {
                isCrazy = true;
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
		positionInSpriteUIList = 8;
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
        positionInSpriteUIList = 9;
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
        positionInSpriteUIList = 10;
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
        positionInSpriteUIList = 11;
    }
}

public class CanuckistanEquipment : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "CanuckistanSuperiorEquipment";
		positionInSpriteUIList = 12;
		rangeModifier = 2f;
		movementModifier = 0.5f;
	}
}

public class SleepDeprived : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "SleepDeprived";
		positionInSpriteUIList = 13;
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
	
public class Patriotism : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "Patriotism";
		positionInSpriteUIList = 14;
	}

	public override float ModifyAttack ()
	{
		if (GameControlScript.control.GetLevel() == 3) {
			attackModifier = 2.0f;
		} else {
			attackModifier = 1;
		}

		return attackModifier;
	}
}

public class FrontLineCommander : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "FrontLineCommander";
		positionInSpriteUIList = 15;
	}
}

public class TankTrait : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "Tank";
		positionInSpriteUIList = 16;
		movementModifier = 0;
		rangeModifier = 1;
	}
}

public class SSTraining : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "SSTraining";
		positionInSpriteUIList = 17;
		rangeModifier = 1.25f;
	}
}

public class Attachment : GenericTraitsScript
{
	//penalty if m31 ( Terry Winters ) or Devi dies
	public override void InitializeValues ()
	{
		name = "Attachment";
		positionInSpriteUIList = 18;
	}

	public override float ModifyAttack()
	{
		bool terry = false;
		bool devi = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Terry Winters") {
				terry = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Devi Devai") {
				devi = true;
			}
		}

		if (terry && devi) {
			attackModifier = 1.0f;
		} else if (terry && !devi || !terry && devi) {
			attackModifier = 0.5f;
		}

		return attackModifier;
	}

	public override float ModifyDefense()
	{
		bool terry = false;
		bool devi = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Terry Winters") {
				terry = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Devi Devai") {
				devi = true;
			}
		}

		if (terry && devi) {
			defenseModifier = 1.0f;
		} else if (terry && !devi || !terry && devi) {
			defenseModifier = 0.5f;
		}

		return defenseModifier;
	}
}

public class Hardworking : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "Hardworking";
		positionInSpriteUIList = 19;
		defenseModifier = 1.5f;
	}
}

public class BerserkRage : GenericTraitsScript
{
	//more damage for each turn attacking
	public override void InitializeValues ()
	{
		name = "BerserkRage";
		positionInSpriteUIList = 20;
	}

	public override float ModifyAttack ()
	{
		float additionalDamage = 0.1f * consecutiveAttacks;
		attackModifier = 1.0f + additionalDamage;
		consecutiveAttacks += 1;
		return attackModifier;
	}
}

public class AngerIssues : GenericTraitsScript
{
	//more damage for health gone, calculated in GenericCharacter
	public override void InitializeValues ()
	{
		name = "AngerIssues";
		positionInSpriteUIList = 21;
	}
}

public class TooAngryToFeelPain : GenericTraitsScript
{
	//more defense if devi dead
	public override void InitializeValues ()
	{
		name = "AngerIssues";
		positionInSpriteUIList = 22;
	}

	public override float ModifyDefense ()
	{
		defenseModifier = 1.5f;
		for (int i = 0; i < GameControlScript.control.GetDeadCharacters().Count; ++i)
		{
			if (GameControlScript.control.GetDeadCharacters()[i] == "Devi Devai")
			{
				defenseModifier = 2.0f;
				break;
			}
		}

		return defenseModifier;
	}
}
	
public class HandsOffLeaderShip : GenericTraitsScript
{
	//less attack
	public override void InitializeValues ()
	{
		name = "HandsOffLeaderShip";
		positionInSpriteUIList = 23;
		attackModifier = 0.8f;
	}
}

public class Comradery : GenericTraitsScript
{
	//f27, f25 work good together
	//Taliyah, Annie
	public override void InitializeValues ()
	{
		name = "HandsOffLeaderShip";
		positionInSpriteUIList = 24;
		attackModifier = 0.8f;
	}

	public override float ModifyAttack ()
	{
		bool taliyah = false;
		bool annie = false;
		for (int i = 0; i < GameControlScript.control.GetInGameCharacterList ().Count; ++i) {
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Taliyah") {
				taliyah = true;
			}
			if (GameControlScript.control.GetInGameCharacterList () [i].GetComponent<GenericCharacterScript> ().GetName () == "Annie Winters") {
				annie = true;
			}
		}

		if (taliyah && annie) {
			attackModifier = 1.5f;
		}

		return attackModifier;
	}
}

public class RunningTally : GenericTraitsScript
{
	//more attack
	public override void InitializeValues ()
	{
		name = "RunnungTally";
		positionInSpriteUIList = 25;
		attackModifier = 1.5f;
	}
}

public class LovedByTroops : GenericTraitsScript
{
	//more attack
	public override void InitializeValues ()
	{
		name = "LovedByTroops";
		positionInSpriteUIList = 26;
		attackModifier = 1.5f;
	}
}

public class DrugAddiction : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "DrugAddiction";
		positionInSpriteUIList = 26;
	}

	//33% chance to do half damage
	public override float ModifyAttack ()
	{
		if (Random.Range (0, 1) < 0.33f) {
			attackModifier = 0.5f;
		} else {
			attackModifier = 1;
		}

		return attackModifier;
	}

	//33% chance to have more movement 
	public override float ModifyMovement ()
	{
		if (Random.Range (0, 1) < 0.33f) {
			movementModifier = 1.5f;
		} else {
			movementModifier = 1;
		}

		return movementModifier;
	}
}

public class SomethingToLiveFor : GenericTraitsScript
{
	public override void InitializeValues ()
	{
		name = "SomethingToLiveFor";
		positionInSpriteUIList = 27;
		safetyShield = true;
	}
}