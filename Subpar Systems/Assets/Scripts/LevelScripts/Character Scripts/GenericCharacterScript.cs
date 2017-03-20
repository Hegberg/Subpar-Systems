using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterScript : MonoBehaviour {

	protected bool hasMoved = false;
	protected int attacksLeft = 1;

	protected GameObject tileOccuping;

	//name of character
	protected string name = "temp";

	//hp of character
	protected float hp = 100;
	//attack damamage of the character's attack
	protected float attack = 50;
	//damage mitigated of any incoming attack
	protected float defense = 20;
	//amount of squares a character can move
	protected float movement = 4;
	//range character can fire
	protected float range = 4;

	protected List<GenericTraitsScript> currentTraits = new List<GenericTraitsScript>();

    // Use this for initialization
    void Start () {
        RefreshActions();
        ModifyStats();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ModifyStats()
    {
        /*
        for (int i = 0; i < currentTraits.Count; ++i)
        {
            //add modifiers together so that the attack modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
            attackModifier += currentTraits[i].ModifyAttack() - 1;
        }
        */
    }
	/*
    void OnGUI()
    {
		
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        //GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), hp + "/" + 100);
		if (hp >= 66) {
			GUI.color = Color.green;
		} else if (hp >= 33) {
			GUI.color = Color.yellow;
		} else {
			GUI.color = Color.red;
		}

		GUI.HorizontalScrollbar (new Rect (targetPos.x - (this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 10), 
			Screen.height - targetPos.y - (this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y * 10), 
			30, 10), 0, hp, 0, 100);

    }
    */

	public virtual void ShowHealthOnPlayer() {
		Color colour = new Color();
		if (hp >= 66) {
			colour = Color.white;
		} else if (hp >= 33) {
			colour = Color.yellow;
		} else {
			colour = Color.red;
		}
		this.gameObject.GetComponent<SpriteRenderer> ().material.color = colour;
	}

    public virtual void OnMouseOver()
    {
        //if player clicked on and it's players turn, then select player
		if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && (!hasMoved || attacksLeft > 0))
        {
            TurnControlScript.control.SetPlayerSelected(this.gameObject);
			ShowTraits ();
			//DebugShowTraits();
        }
    }

	public void ShowTraits() {
		TraitSpriteControl.control.ShowTraits (currentTraits);
	}

	public void UnShowTraits() {
		TraitSpriteControl.control.UnShowTraits ();
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

        float movementModifier = 1.0f;
        for (int i = 0; i < currentTraits.Count; ++i)
        {
            //add modifiers together so that the defense modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
            movementModifier += currentTraits[i].ModifyMovement() - 1;
        }
        //stop modifier from going below 0%
        if (movementModifier < 0)
        {
            movementModifier = 0;
        }
        return (int)(movement * movementModifier);
	}

	public int GetRange() {
        float rangeModifier = 1.0f;
        for (int i = 0; i < currentTraits.Count; ++i)
        {
            //add modifiers together so that the defense modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
            rangeModifier += currentTraits[i].ModifyRange() - 1;
        }
        //stop modifier from going below 0%
        if (rangeModifier < 0)
        {
            rangeModifier = 0;
        }
        return (int)(range * rangeModifier);
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
        //Debug.Log("character attacks = " + gameObject.name.ToString() + " " + attacksLeft);
        hasMoved = false;
		//unvoid character
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		//update player health
		ShowHealthOnPlayer();
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
    public void HPLost(int hpLost)
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
            Destroy(this.gameObject);
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

	public string GetName() {
		return name;
	}

	public void RemoveFromGame() {
		Destroy (this.gameObject);
	}
}