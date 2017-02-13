using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterScript : MonoBehaviour {

	protected bool hasMoved = false;
	protected int attacksLeft = 1;

	protected GameObject tileOccuping;

	//hp of character
	protected float hp = 100;
	//attack damamage of the character's attack
	protected float attack = 100;
	//damage mitigated of any incoming attack
	protected float defense = 20;
	//amount of squares a character can move
	protected float movement = 4;
	//range character can fire
	protected float range = 4;

	protected List<GenericTraitsScript> currentTraits = new List<GenericTraitsScript>();

    // Use this for initialization
    void Start () {
		RefreshActions ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnMouseOver()
    {
        //if player clicked on and it's players turn, then select player
		if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && (!hasMoved || attacksLeft > 0))
        {
            TurnControlScript.control.SetPlayerSelected(this.gameObject);
			DebugShowTraits();
        }
    }

	public float GetAttack()
	{
		float attackModifier = 1.0f;
		for (int i = 0; i < currentTraits.Count; ++i) {
			//add modifiers together so that the attack modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
			attackModifier += currentTraits[i].ModifyAttack() - 1;
		}
		//stop modifier from going below 0%, and if at 0% stop attack
		//to be completed
		if (attackModifier < 0) {
			attackModifier = 0;
		}
		return attack * attackModifier;
	}

	public float GetHP()
	{
		return hp;
	}

	public float GetDefense()
	{
		float defenseModifier = 1.0f;
		for (int i = 0; i < currentTraits.Count; ++i) {
			//add modifiers together so that the defense modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
			defenseModifier += currentTraits[i].ModifyDefense() - 1;
		}
		//stop modifier from going below 0%
		if (defenseModifier < 0) {
			defenseModifier = 0;
		}
		return defense * defenseModifier;
	}

	public int GetMovement() {
		return (int)movement;
	}

	public int GetRange() {
		return (int)range;
	}

	public void DebugShowTraits(){
		for (int i = 0; i < currentTraits.Count; ++i) {
			currentTraits[i].ShowInfo();
		}
	}

	public void AddTrait(GenericTraitsScript trait){
		currentTraits.Add (trait);
	}

	public List<GenericTraitsScript> GetTraits() {
		return currentTraits;
	}

	//refresh actions and get rid of fade out
    public void RefreshActions()
    {
		float attacks = 1;
		for (int i = 0; i < currentTraits.Count; ++i) {
			//add modifiers together so that the defense modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
			attacks += currentTraits[i].ModifyNumOfAttacks() - 1;
		}
		attacksLeft = (int)attacks;
		hasMoved = false;
		//unvoid character
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
    }

    public void RemoveActions()
    {
		attacksLeft = 0;
        hasMoved = true;
		OutOfActions ();
    } 

    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
		OutOfActions ();
    }

	public void PlayerAttacked(int numOfTimesAttacked)
    {
		attacksLeft -= numOfTimesAttacked;
		if (attacksLeft <= 0) {
			TurnControlScript.control.UnHighlightEnemyTile ();
		}
		OutOfActions ();
    }

	public void OutOfActions() {
		//if player out of actions, make them fade out slightly
		if (attacksLeft <= 0 && hasMoved) {
			TurnControlScript.control.SetPlayerSelected (null);
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.50f);
		} 
	}

    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public int GetNumOfAttacks()
    {
		return (int)attacksLeft;
    }

	//need to implement permenant death
    public void SetHP(int hpChangedTo)
    {
        hp = hpChangedTo;
		if (hp <= 0) {
			LevelControlScript.control.PlayerDied ();
            GameControlScript.control.CharacterDied(this.gameObject);
        }
    }

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
    }
}