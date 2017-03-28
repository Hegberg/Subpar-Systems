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

	Color colour = Color.white;

	protected List<GenericTraitsScript> currentTraits = new List<GenericTraitsScript>();

    // Use this for initialization
    void Start () {
        RefreshActions();
    }
	
	// Update is called once per frame
	void Update () {
		
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
		bool underControl = true;
		for (int i = 0; i < currentTraits.Count; ++i) {
			if (currentTraits [i].GetIfCrazy ()) {
				underControl = false;
				break;
			}
		}

		//make character not refresh if has gone mad

		float attacks = 1;
		for (int i = 0; i < currentTraits.Count; ++i) {
			//add modifiers together so that the defense modifier such that the percetages are all added together, and the total percente over or under is the new attack modifier
			attacks += currentTraits[i].ModifyNumOfAttacks() - 1;
		}
		attacksLeft = (int)attacks;
        //Debug.Log("character attacks = " + gameObject.name.ToString() + " " + attacksLeft);
        hasMoved = false;
		//unvoid character
		//GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		colour.a = 1f;
		GetComponent<SpriteRenderer> ().color = colour;
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
			colour.a = 0.5f;
			GetComponent<SpriteRenderer> ().color = colour;
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

		//update player health
		ShowHealthOnPlayer();
    }

	public void GoneCrazy() {
		Move ();
		Attack ();
	}

	public void Move() {
		//if any players alive, move
		if (GameControlScript.control.GetInGameCharacterList().Count > 0)
		{
			List<List<int>> FloodFillTiles = new List<List<int>>();
			//Return all valid movement tiles

			//broken, not giving tiles in a few cases, breaks nearest tile code below
			//Debug.Log("Current tile position is row " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0] + ", Index " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
			FloodFillTiles = AStarScript.control.FloodFillWithinRange(LevelControlScript.control.GetAStarMap(),
				LevelControlScript.control.GetAStarMapCost(),
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
				(int)movement);

			//Debug.Log("FloodFillTiles Count = " + FloodFillTiles.Count + " with movement range " + (int)movement);
			//Debug.Log(FloodFillTiles.Count + " count " + (int)movement + " movement");
			//Debug.Log("what");

			//Find the closest "player character"
			GameObject nearestEnemy = new GameObject();
			nearestEnemy =	FindClosestEnemy();

			List<int> closest = new List<int>();

			closest = nearestEnemy.GetComponent<GenericCharacterScript>().GetTileOccuping().
				GetComponent<GenericEarthScript>().GetTilePosition();
			//Debug.Log ("I am at row " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [0] + " ,  " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [1]);
			//Debug.Log("Closest Player position is row " + closest[0] + "," + closest[1]);

			List<int> nearestTile = new List<int>();
			int closestTileValue = int.MaxValue;

			List<List<GameObject>> tempMap = LevelControlScript.control.GetAStarMap();

			//find closest tile to that character
			foreach (var elementTile in FloodFillTiles)
			{
				//if(nearestTile == null && tempMap[elementTile[0]][elementTile[1]])
				//{
				//    nearestTile = elementTile;
				//}
				int difference = Mathf.Abs(elementTile[0] - closest[0]) + Mathf.Abs(elementTile[1] - closest[1]);
				//Check to see if the tile total different in coordinate is less than the current closest
				if (difference <= closestTileValue && tempMap[elementTile[0]][elementTile[1]].GetComponent<GenericEarthScript>().GetOccupingObject() == null)
				{
					//Swap the values
					//Debug.Log("The cloeset Tile currently is " + elementTile[0] + "," + elementTile[1] + " with difference of " + difference);
					closestTileValue = difference;
					nearestTile = elementTile;
				}
			}//end find the closest tile to enemy

			//Move the enemy to the tile coordinates
			//Debug.Log("END OF TESTING FIRST HALF");
			//Debug.Log(nearestTile.Count);
			//Debug.Log(nearestTile[0]);
			//Debug.Log(nearestTile[1]);

			//this is broken, nearest tile should give a tile, without this check, errors that shouldn't happen get thrown
			if (nearestTile.Count > 0)
			{
				GameObject tile = tempMap[nearestTile[0]][nearestTile[1]];
				MoveToTile(tile);
			}
		}
	}

	public void Attack() {
		//if any players alive, attack
		if (GameControlScript.control.GetInGameEnemyList().Count > 0)
		{
			List<List<int>> FloodFillTiles = new List<List<int>>();
			List<List<GameObject>> map = LevelControlScript.control.GetAStarMap();
			//Return all valid movement tiles
			FloodFillTiles = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(),
				LevelControlScript.control.GetAStarMapCost(),
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
				(int)range);

			//Find the closest "player character"
			GameObject nearestEnemy = FindClosestEnemy();

			List<int> targetPos = new List<int>();

			targetPos = nearestEnemy.GetComponent<GenericEnemyScript>().GetTileOccuping().
				GetComponent<GenericEarthScript>().GetTilePosition();

			bool playerTileFound = false;

			//if character is within character tile, allow them to attack that character
			for (int i = 0; i < FloodFillTiles.Count; ++i)
			{
				if (targetPos[0] == FloodFillTiles[i][0] && targetPos[1] == FloodFillTiles[i][1])
				{
					playerTileFound = true;
					break;
				}
			}

			//Now attack the player standing on the neartestTile
			if (playerTileFound)
			{
				nearestEnemy.GetComponent<GenericEnemyScript>().HPLost((int)attack);
			}
		}
	}

	public GameObject FindClosestEnemy()
	{
		int closestEnemyTileValue = int.MaxValue;
		GameObject nearestEnemy = new GameObject();
		//when no players alive do nothing
		if (GameControlScript.control.GetInGameCharacterList().Count > 0)
		{

			nearestEnemy = GameControlScript.control.GetInGameEnemyList()[0];
			List<int> closest = new List<int>();
			closest = GameControlScript.control.GetInGameEnemyList()[0].GetComponent<GenericEnemyScript>().
				GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition();

			int sourceTileRow = tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [0];
			int sourceTileIndex = tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1];

			int closestDis = AStarScript.control.CalculateHeuristicCost (sourceTileRow, sourceTileIndex, closest [0], closest [1]);
			int x = 0;
			foreach (var enemy in GameControlScript.control.GetInGameEnemyList())
			{
				//stop from checking against itself if that condition happens, will always happen for [0]
				//if (character != nearestPlayer)
				//{
				x = x + 1;
				int newClosestDis = AStarScript.control.CalculateHeuristicCostEnemy (
					sourceTileRow,
					sourceTileIndex,
					enemy.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0], 
					enemy.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1] 
				);                    	
				//int difference = Mathf.Abs(character.GetComponent<GenericCharacterScript>().GetTileOccuping().
				//GetComponent<GenericEarthScript>().GetTilePosition()[0] - closest[0]) + Mathf.Abs(character.GetComponent<GenericCharacterScript>().
				//GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] - closest[1]);
				//Check to see if the tile total different in coordinate is less than the current closest
				/*
				Debug.Log("SR: " + sourceTileRow + " SI: " + sourceTileIndex  + " SWAP:" + nearestPlayer.name.ToString() + " - " + character.name.ToString() + "\n"
					+ "NC: " + newClosestDis + " OC: "+ closestDis + " C:" + x + "\n" + "CSR: " + character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0] + 
					" CSI:" + character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1]
				);
*/
				if (newClosestDis <= closestDis)
				{

					//Swap the values
					closestDis = newClosestDis;
					nearestEnemy = enemy;
					//closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
					//    GetComponent<GenericEarthScript>().GetTilePosition();
				}
				//}
			}
		}
		//return null;
		return nearestEnemy;
	}

	public void MoveToTile(GameObject tileMovingTo)
	{
		tileOccuping.GetComponent<GenericEarthScript>().SetOccupingObject(null);
		tileOccuping.GetComponent<GenericEarthScript>().SetIsAnEnemyOccupyingThisTile(false);

		SetTileOccuping(tileMovingTo);

		//get correct position (so tile placement but slightly up so goes to middle of tile)
		Vector3 tempTile = tileMovingTo.transform.position;
		tempTile.z -= 0.01f;
		tempTile.y += (tileMovingTo.gameObject.GetComponent<Renderer>().bounds.size.y / (LevelControlScript.control.GetTileHeightRatio() * 2));

		this.gameObject.transform.position = tempTile;
	}

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
		tileOccuping.GetComponent<GenericEarthScript>().SetOccupingObject(this.gameObject);
    }

	public string GetName() {
		return name;
	}

	public void RemoveFromGame() {
		Destroy (this.gameObject);
	}
}