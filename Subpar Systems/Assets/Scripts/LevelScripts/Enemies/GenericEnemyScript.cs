using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

    private GameObject tileOccuping;

    private float hp = 100;
    private float attack = 100;
    private float movement = 3;
    private float range = 3;

    private bool isSelected = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
		//if player turn, player selected and player hasn't attacked
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && 
            TurnControlScript.control.GetPlayerSelected() != null &&
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasAttacked())
        {
            if (isSelected)
            {
				//calculate damage taken, if it is 0 attack doesn't happen
				if (TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack () != 0) {
					hp -= TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack ();
					//set player attacked to true
					TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().SetHasAttacked (true);
					//deselect enemy
					TurnControlScript.control.SetEnemySelected (null);
					isSelected = false;
					//check if enemy still alive
					if (hp <= 0) {
						EnemyParentScript.control.EnemyDied ();
						Destroy (gameObject);
					}
				} else {
					//do nothing attack doesn't happen
					//Debug.Log("0 attack");
				}
            }
            else
            {
				//if another enemy selected, deselect it
                if (TurnControlScript.control.GetEnemySelected() != null)
                {
                    TurnControlScript.control.GetEnemySelected().GetComponent<GenericEnemyScript>().SetIsSelected(false);
                }
				isSelected = true;
				TurnControlScript.control.SetEnemySelected (this.gameObject);
            }
        }
    }

    void Move()
    {
		List<List<int>> FloodFillTiles = new List<List<int>> ();
		//Return all valid movement tiles
		FloodFillTiles = AStarScript.control.FloodFillWithinRange(LevelControlScript.control.GetAStarMap(), 
			LevelControlScript.control.GetAStarMapCost(),
			TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
			TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
			(int) movement);

		List<int> closest = new List<int>();
		//Find the closest "player character"

		List<int> nearestTile = new List<int> ();
		int closestTileValue = int.MaxValue;
		foreach (var elementTile in FloodFillTiles)
		{
			int difference = Mathf.Abs (elementTile [0] - closest [0]) + Mathf.Abs (elementTile [1] - closest [1]);
			//Check to see if the tile total different in coordinate is less than the current closest
			if (difference <= closestTileValue) 
			{
				//Swap the values
				closestTileValue = difference;
				nearestTile = elementTile;
			}
		}//end find the closest enemy
    	
		//Move the enemy to the tile coordinates
	}

    void Attack()
    {
		List<List<int>> FloodFillTiles = new List<List<int>> ();
		//Return all valid movement tiles
		FloodFillTiles = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(), 
			LevelControlScript.control.GetAStarMapCost(),
			TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
			TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
			(int) range);

		List<int> closest = new List<int>();
		//Find the closest "player character"

		List<int> target = new List<int> ();
		int closestTileValue = int.MaxValue;

		foreach (var elementTile in FloodFillTiles)
		{
			//First check to see if there is a player object on it
			//If there is not one (look at the data structure that holds all the row,index of the player character)
			//Continue
			//Else do the follow

			//Current behaviour is that attack the cloest enemy
			int difference = Mathf.Abs (elementTile [0] - closest [0]) + Mathf.Abs (elementTile [1] - closest [1]);
			//Check to see if the tile total different in coordinate is less than the current closest
			if (difference <= closestTileValue) 
			{
				//Swap the values
				closestTileValue = difference;
				target = elementTile;
			}
		}//end find the closest enemy

		//Now attack the player standing on the neartestTile

		//Move the enemy to the tile coordinates
    }

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }
}
