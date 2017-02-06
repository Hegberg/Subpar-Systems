using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterScript : MonoBehaviour {

	protected bool hasMoved = false;
	protected bool hasAttacked = false;

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

	protected List<List<float>> currentTraits = new List<List<float>>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnMouseOver()
    {
        //if player clicked on and it's players turn, then select player
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn())
        {
            TurnControlScript.control.SetPlayerSelected(this.gameObject);
			//DebugShowTraits();
        }
    }

	public float GetAttack()
	{
		float attackModifier = 1.0f;
		for (int i = 0; i < currentTraits.Count; ++i) {
			//add modifiers together so that the attack modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
			attackModifier += (currentTraits [i] [1] - 1);
		}
		//stop modifier from going below 0%
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
			defenseModifier += (currentTraits [i] [2] - 1);
		}
		//stop modifier from going below 0%
		if (defenseModifier < 0) {
			defenseModifier = 0;
		}
		return defense * defenseModifier;
	}

	public float GetMovement() {
		return movement;
	}

	public float GetRange() {
		return range;
	}

	public void DebugShowTraits(){
		for (int i = 0; i < currentTraits.Count; ++i) {
			Debug.Log ("trait: " + i + ", hp modifier: " + currentTraits [i][0] + ", attack modifier: " + currentTraits [i][1] 
				+ ", defense modifier: " + currentTraits [i][2] + ", movement modifier: " + currentTraits [i][3] + 
				", range modifier: " + currentTraits [i][4]);
		}
	}

	public void AddTrait(List<float> trait){
		currentTraits.Add (trait);
	}

	public List<List<float>> GetTraits() {
		return currentTraits;
	}

    public void RefreshActions()
    {
        hasAttacked = false;
        hasMoved = false;
    }

    public void RemoveActions()
    {
        hasAttacked = true;
        hasMoved = true;
    } 

    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }

    public void SetHasAttacked(bool attacked)
    {
        hasAttacked = attacked;
    }

    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public bool GetHasAttacked()
    {
        return hasAttacked;
    }

    public void SetHP(int hpChangedTo)
    {
        hp = hpChangedTo;
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