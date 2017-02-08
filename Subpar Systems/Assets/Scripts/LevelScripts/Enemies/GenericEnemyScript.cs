﻿using System.Collections;
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
                        GameControlScript.control.RemoveEnemyFromInGameList(this.gameObject);
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

    public void Move()
    {
		List<List<int>> FloodFillTiles = new List<List<int>> ();
		//Return all valid movement tiles

        //broken, not giving tiles in a few cases, breaks nearest tile code below
		FloodFillTiles = AStarScript.control.FloodFillWithinRange(LevelControlScript.control.GetAStarMap(), 
			LevelControlScript.control.GetAStarMapCost(),
			tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
			tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
			(int) movement);

        Debug.Log(FloodFillTiles.Count + " count " + (int)movement + " movement");
        Debug.Log("what");

        //Find the closest "player character"
        GameObject nearestPlayer = FindClosestPlayer();

        List<int> closest = new List<int>();

        closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                GetComponent<GenericEarthScript>().GetTilePosition();


        List<int> nearestTile = new List<int>();
		int closestTileValue = int.MaxValue;

        List<List<GameObject>> tempMap = LevelControlScript.control.GetAStarMap();

        //find closest tile to that character
        foreach (var elementTile in FloodFillTiles)
		{
            if(nearestTile == null && tempMap[elementTile[0]][elementTile[1]])
            {
                nearestTile = elementTile;
            }
            int difference = Mathf.Abs (elementTile [0] - closest [0]) + Mathf.Abs (elementTile [1] - closest [1]);
			//Check to see if the tile total different in coordinate is less than the current closest
            if (difference <= closestTileValue && tempMap[elementTile[0]][elementTile[1]].GetComponent<GenericEarthScript>().GetOccupingObject() == null) 
			{
				//Swap the values
				closestTileValue = difference;
				nearestTile = elementTile;
			}
		}//end find the closest tile to enemy

        //Move the enemy to the tile coordinates
        Debug.Log(nearestTile.Count);
        //Debug.Log(nearestTile[0]);
        //Debug.Log(nearestTile[1]);

        //this is broken, nearest tile should give a tile, without this check, errors that shouldn't happen get thrown
        if (nearestTile.Count > 0)
        {
            GameObject tile = tempMap[nearestTile[0]][nearestTile[1]];
            MoveToTile(tile);
        }
    }

    public void Attack()
    {
		List<List<int>> FloodFillTiles = new List<List<int>> ();
		//Return all valid movement tiles
		FloodFillTiles = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(), 
			LevelControlScript.control.GetAStarMapCost(),
            tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
            tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
            (int) range);

        //Find the closest "player character"
        GameObject nearestPlayer = FindClosestPlayer();
        List<int> target = new List<int>();

        target = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                GetComponent<GenericEarthScript>().GetTilePosition();

        int closestTileValue = int.MaxValue;

        foreach (var elementTile in FloodFillTiles)
		{
			//First check to see if there is a player object on it
			//If there is not one (look at the data structure that holds all the row,index of the player character)
			//Continue
			//Else do the follow

			//Current behaviour is that attack the cloest enemy
			int difference = Mathf.Abs (elementTile [0] - target [0]) + Mathf.Abs (elementTile [1] - target [1]);
			//Check to see if the tile total different in coordinate is less than the current closest
			if (difference <= closestTileValue) 
			{
				//Swap the values
				closestTileValue = difference;
				target = elementTile;
			}
		}//end find the closest enemy

		//Now attack the player standing on the neartestTile

    }
    
    public GameObject FindClosestPlayer()
    {
        int closestCharacterTileValue = int.MaxValue;
        GameObject nearestPlayer = new GameObject();
        List<int> closest = new List<int>();
        closest = GameControlScript.control.GetInGameCharacterList()[0].GetComponent<GenericCharacterScript>().
            GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition();

        foreach (var character in GameControlScript.control.GetInGameCharacterList())
        {
            int difference = Mathf.Abs(character.GetComponent<GenericCharacterScript>().GetTileOccuping().
                GetComponent<GenericEarthScript>().GetTilePosition()[0] - closest[0]) + Mathf.Abs(character.GetComponent<GenericCharacterScript>().
                GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] - closest[1]);
            //Check to see if the tile total different in coordinate is less than the current closest
            if (difference <= closestCharacterTileValue)
            {
                //Swap the values
                closestCharacterTileValue = difference;
                nearestPlayer = character;
            }
        }

        return nearestPlayer;
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

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public virtual void MoveToTile(GameObject tileMovingTo)
    {
        SetTileOccuping(tileMovingTo);

        //get correct position (so tile placement but slightly up so goes to middle of tile)
        Vector3 tempTile = tileMovingTo.transform.position;
        tempTile.z -= 0.01f;
        tempTile.y += (tileMovingTo.gameObject.GetComponent<Renderer>().bounds.size.y / (LevelControlScript.control.GetTileHeightRatio() * 2));

        this.gameObject.transform.position = tempTile;
    }
}
