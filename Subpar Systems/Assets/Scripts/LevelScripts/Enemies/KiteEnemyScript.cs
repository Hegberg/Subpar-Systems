﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteEnemyScript : GenericEnemyScript {

    private bool didKite = false;

	public void checkAttack()
	{
		this.Attack ();
	}

	public override void Move()
    {
		//Check Attack
		checkAttack();
	
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


            //Find the closest "player character"
			GameObject nearestPlayer = new GameObject();
			nearestPlayer =	FindClosestPlayer();

            List<int> closest = new List<int>();

            closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                    GetComponent<GenericEarthScript>().GetTilePosition();

			List<int> nearestTile = new List<int>();
			int closestTileValue = int.MaxValue;

			List<List<GameObject>> tempMap = LevelControlScript.control.GetAStarMap();
			if (!didKite) {
				//find closest tile to that character
				foreach (var elementTile in FloodFillTiles) {

					int difference = Mathf.Abs (elementTile [0] - closest [0]) + Mathf.Abs (elementTile [1] - closest [1]);
					//Check to see if the tile total different in coordinate is less than the current closest
					if (difference <= closestTileValue && tempMap [elementTile [0]] [elementTile [1]].GetComponent<GenericEarthScript> ().GetOccupingObject () == null) {
						//Swap the values
						//Debug.Log("The cloeset Tile currently is " + elementTile[0] + "," + elementTile[1] + " with difference of " + difference);
						closestTileValue = difference;
						nearestTile = elementTile;
					}
				}//end find the closest tile to enemy
			} else {
				//find closest tile to that character
				foreach (var elementTile in FloodFillTiles)
				{

					int difference = Mathf.Abs(elementTile[0] - closest[0]) + Mathf.Abs(elementTile[1] - closest[1]);
					//Check to see if the tile total different in coordinate is less than the current closest
					if (difference >= closestTileValue && tempMap[elementTile[0]][elementTile[1]].GetComponent<GenericEarthScript>().GetOccupingObject() == null)
					{
						//Swap the values
						//Debug.Log("The cloeset Tile currently is " + elementTile[0] + "," + elementTile[1] + " with difference of " + difference);
						closestTileValue = difference;
						nearestTile = elementTile;
					}
				}//end find the closest tile to enemy
			
			}

			//this is broken, nearest tile should give a tile, without this check, errors that shouldn't happen get thrown
			if (nearestTile.Count > 0)
			{
				GameObject tile = tempMap[nearestTile[0]][nearestTile[1]];
				MoveToTile(tile);
			}
		}
	}


    public override void Attack()
    {
		if (didKite) {
			return;
		}

        //if any players alive, attack
        if (GameControlScript.control.GetInGameCharacterList().Count > 0)
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
            GameObject nearestPlayer = FindClosestPlayer();

            List<int> targetPos = new List<int>();

            targetPos = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
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
			if (playerTileFound) {
				nearestPlayer.GetComponent<GenericCharacterScript> ().HPLost ((int)attack);
				didKite = true;
			} else {
				didKite = false;
			}
        }
    }
    
    
}
