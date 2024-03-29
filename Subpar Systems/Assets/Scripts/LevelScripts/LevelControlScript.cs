﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour {

    public static LevelControlScript control;

    public Transform TileParent;

    public Transform characterParent;

    public Transform enemyParent;

    private float maxCameraY = 0f;
    private float maxCameraX = 0f;
    private float minCameraY = 0f;
    private float minCameraX = 0f;

    private List<List<GameObject>> aStarMap = new List<List<GameObject>>();
    private List<List<List<int>>> aStarMapCost = new List<List<List<int>>>();

    //private float tileWidth = 0.64f;
    private float tileWidth;
    private float tileHeight;

    private float tileHeightRatio = 2.0f;

	private int playersAlive = 0;

	private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

	private List<List<List<int>>> mapData = new List<List<List<int>>>();

	private List<int> tankSpawn = new List<int> { 6, 8 };
	int levelTankSpawn = 2;

    // Use this for initialization
    void Start () {

        if(control == null)
        {
            control = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

		DontDestroyOnLoad (this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void BroadcastRefreshActionsToCharacters()
    {
        BroadcastMessage("RefreshActions");
    }

    public void BroadcastRemoveActionsToCharacters()
    {
        BroadcastMessage("RemoveActions");
    }

	public bool SpawnEnemy(int tilePositionX, int tilePositionY, int enemyType) {
		//Debug.Log (aStarMap[tilePositionX][tilePositionY].GetComponent<GenericEarthScript>().GetOccupingObject());
		Debug.Log(tilePositionX + " " + tilePositionY);
		if (aStarMap[tilePositionY][tilePositionX].GetComponent<GenericEarthScript>().GetOccupingObject() == null) {
			float tempY = 0;
			float tempX = 0;

			List<GameObject> tiles = GameControlScript.control.GetTiles();
			GameObject oneTile = tiles[mapData[tilePositionY][tilePositionX][0]];

			tileWidth = oneTile.GetComponent<Renderer>().bounds.size.x;
			tileHeight = oneTile.GetComponent<Renderer>().bounds.size.y;

			//if not offset do first, else do second
			if (tilePositionY % 2 == 1)
			{
				tempX = (tilePositionX * tileWidth);
			}
			else
			{
				tempX = (tilePositionX * tileWidth) + (tileWidth / 2);
			}
			Debug.Log (tileHeight / 3);

			//sets y to level 0 height
			tempY = ((mapData.Count - tilePositionY) * (tileHeight /3));
			tempY += tileHeight / 4;
			//put y to current level if not 0
			tempY += ((tileHeight / 3) * mapData[tilePositionY][tilePositionX][1]);
			Transform enemy = (Transform)Instantiate(
				GameControlScript.control.GetEnemies()[enemyType].transform,
				new Vector3(tempX, tempY, (0.02f * tilePositionX) - 0.01f), Quaternion.identity);
			enemy.SetParent(enemyParent);
			enemy.gameObject.GetComponent<GenericEnemyScript>().SetTileOccuping(
				aStarMap[tilePositionY][tilePositionX]);
			//enemy.gameObject.GetComponent<SpriteRenderer> ().material.color = Color.blue;
			EnemyParentScript.control.EnemyCreated ();
			GameControlScript.control.AddEnemyToInGameList(enemy.gameObject);
			return true;
		}
		return false;
	}

	public void CreateMap(List<List<List<int>>> map, List<int[]> playerSpawnLocations, 
		List<int[]> enemySpawnLocations , List<EnemySpawner> tempEnemySpawners)
    {
        minCameraX = 0f;
        minCameraY = 0f;

        float fuckThomasX = 0.0f;
        float fuckThomasY = 0.0f;
        float fuckThomasZ = 0.0f;

		//reset map lists
		aStarMap.Clear();
		aStarMapCost.Clear ();
		mapData.Clear ();
		enemySpawners.Clear ();

        //0 is earth, 1 is water, 2 is mountian
        //start at bottom row and build up

        List<bool> charactersChosen = GameControlScript.control.GetChosen();

        int characterSpawning = 0;

        bool offset = false;
		List<GameObject> tiles = GameControlScript.control.GetTiles();

        for (int i = map.Count - 1; i >= 0; --i)
        {
            aStarMap.Add(new List<GameObject> { });
            aStarMapCost.Add(new List<List<int>> { });

            for (int j = 0; j < map[i].Count; ++j)
            {
                GameObject oneTile = tiles[map[i][j][0]];

                tileWidth = oneTile.GetComponent<Renderer>().bounds.size.x;
                tileHeight = oneTile.GetComponent<Renderer>().bounds.size.y;

                if (!offset)
                {
                    fuckThomasX = (j * tileWidth);
                }
                else
                {
                    fuckThomasX = (j * tileWidth) + (tileWidth / 2);
                }
                //sets y to level 0 height
                fuckThomasY = ((map.Count - i) * (tileHeight /3));
                //put y to current level if not 0
                fuckThomasY += ((tileHeight / 3) * map[i][j][1]);
                //fuckThomasZ += 0.01f;

                Vector3 tempVector = new Vector3(fuckThomasX, fuckThomasY, fuckThomasZ);

                Transform tile = (Transform)Instantiate(oneTile.transform, tempVector, Quaternion.identity);
                tile.SetParent(TileParent);

				//created for tile filler below
				Vector3 tempVectorFiller = new Vector3(fuckThomasX, fuckThomasY, fuckThomasZ);
				//spawn tiles below raised tiles
				for (int k = 0; k < map[i][j][1]; ++k) {
					//lower by 1 Y interval
					tempVectorFiller.y -= (tileHeight / 3);
					Transform tileFiller = (Transform)Instantiate(oneTile.transform,tempVectorFiller, Quaternion.identity);
					tileFiller.SetParent(TileParent);
					Destroy (tileFiller.GetComponent<Collider2D>());
				}

				//Check to see if it is an earth tile if it is give it it's current tile position
				if (map[i][j][0] == 0) {
					List<int> tempTilePosition = new List<int> ();
					tempTilePosition.Add (i);
					tempTilePosition.Add (j);
					tile.gameObject.GetComponent<GenericEarthScript>().SetTilePosition(tempTilePosition);
				}
                //add gameObject to regular aStarList
                aStarMap[map.Count - i - 1].Add(tile.gameObject);
                //add walkable/height to cost list
                //add is walkable first
                aStarMapCost[map.Count - i - 1].Add(new List<int>());
                aStarMapCost[map.Count - i - 1][j].Add(map[i][j][0]);
                //then add tile height
                aStarMapCost[map.Count - i - 1][j].Add(map[i][j][1]);

				//spawn tank code
				if (GameControlScript.control.GetLevel() == levelTankSpawn && tankSpawn[0] == j && tankSpawn[1] == i) {
					
					tempVector.z -= 0.01f;

					tempVector.y += (oneTile.GetComponent<Renderer>().bounds.size.y / (tileHeightRatio + 2));

					//12 is tank spot in list
					Transform tank = (Transform)Instantiate(
						GameControlScript.control.GetCharacters()[12],
						tempVector, Quaternion.identity);

					tank.gameObject.GetComponent<GenericCharacterScript>().SetTileOccuping(tile.gameObject);
					tile.gameObject.GetComponent<GenericEarthScript>().SetOccupingObject(tank.gameObject);
					tank.SetParent(characterParent);
					//characterSpawning += 1;
					GameControlScript.control.AddCharacterToInGameList(tank.gameObject);
				}

				//reset temp vector
				tempVector = new Vector3(fuckThomasX, fuckThomasY, fuckThomasZ);

                //spawn character code
                for (int k = 0; k < playerSpawnLocations.Count; ++k)
                {
                    if (playerSpawnLocations[k][0] == j && playerSpawnLocations[k][1] == i)
                    {   
                        tempVector.z -= 0.01f;

                        tempVector.y += (oneTile.GetComponent<Renderer>().bounds.size.y / tileHeightRatio);

                        //determine character to spawn
                        for(int l = characterSpawning; l < charactersChosen.Count; ++l)
                        {
                            //Debug.Log(charactersChosen[l]);
                            if (charactersChosen[l])
                            {
                                characterSpawning = l;
                                break;
                            }
                        }

                        Transform character = (Transform)Instantiate(
                            GameControlScript.control.GetCharacters()[characterSpawning],
                            tempVector, Quaternion.identity);
                        character.gameObject.GetComponent<GenericCharacterScript>().SetTileOccuping(tile.gameObject);
                        tile.gameObject.GetComponent<GenericEarthScript>().SetOccupingObject(character.gameObject);
                        character.SetParent(characterParent);
                        characterSpawning += 1;
                        GameControlScript.control.AddCharacterToInGameList(character.gameObject);
						//Debug.Log (character.GetComponent<GenericCharacterScript> ().GetName () + " character ini name in game");
                        break;
                    }
                }
				//reset temp vector
				tempVector = new Vector3(fuckThomasX, fuckThomasY, fuckThomasZ);

                //spawn enemy code
                for (int k = 0; k < enemySpawnLocations.Count; ++k)
                {
                    if (enemySpawnLocations[k][0] == j && enemySpawnLocations[k][1] == i)
                    {
                        tempVector.z -= 0.01f;

                        tempVector.y += (oneTile.GetComponent<Renderer>().bounds.size.y / (2 * tileHeightRatio));

                        Transform enemy = (Transform)Instantiate(
                            GameControlScript.control.GetEnemies()[enemySpawnLocations[k][2]].transform,
                            tempVector, Quaternion.identity);
                        enemy.gameObject.GetComponent<GenericEnemyScript>().SetTileOccuping(tile.gameObject);
						//Debug.Log (enemy.gameObject.GetComponent<GenericEnemyScript> ().GetTileOccuping ());
                        enemy.SetParent(enemyParent);
						EnemyParentScript.control.EnemyCreated ();
                        GameControlScript.control.AddEnemyToInGameList(enemy.gameObject);
                        break;
                    }
                }

                //set max camera postion based on map size
                if (tile.position.x > maxCameraX)
                {
                    maxCameraX = tile.position.x;
                }
                if (tile.position.y > maxCameraY)
                {
                    maxCameraY = tile.position.y;
                }
            }
            offset = !offset;
			fuckThomasZ += 0.02f;
        }


        aStarMap.Reverse();
        aStarMapCost.Reverse();

        //set camera limits to be local to preexisting camera position
        maxCameraX += -1.0f;
        maxCameraY += -0.5f;
        minCameraX += 1.0f;
        minCameraY += 0.5f;


		mapData = map;
		//set enemy spawners for the map
		enemySpawners = tempEnemySpawners;

        if (GameControlScript.control.GetLevel() == 3)
        {
            TurnControlScript.control.SetUsingTurnCounter(true);
        }

        //spawn sidemission character code
        Vector3 tempSideVector = new Vector3(0, 0, 0);

        List<bool> sideCharactersChosen = GameControlScript.control.GetSideMissionChosen();

        //determine side mission character to spawn
        for (int l = 0; l < sideCharactersChosen.Count; ++l)
        {
            //Debug.Log(sideCharactersChosen[l] + " " + l);
            if (sideCharactersChosen[l])
            {
                Transform character = (Transform)Instantiate(
                    GameControlScript.control.GetCharacters()[l],
                    tempSideVector, Quaternion.identity);
                //hide
                character.gameObject.GetComponent<Renderer>().enabled = false;
                character.gameObject.GetComponent<Collider2D>().enabled = false;
                GameControlScript.control.AddSideCharacterToInSideMissionList(character.gameObject);
				//Debug.Log (character.GetComponent<GenericCharacterScript> ().GetName () + " character ini name");
                //GameControlScript.control.AddCharacterToInGameList(character.gameObject);
				//Debug.Log(GameControlScript.control.GetInGameSideCharacterList().Count + " in game shit");
            }
        }
		//Debug.Log(GameControlScript.control.GetInGameSideCharacterList().Count + " in game shit after loop");

		//run side mission scripts\
		SideMissionScript.control.runSideMission();
		/*
		int sideMissionResult;
		if (GameControlScript.control.GetLevel () == 1) {
			sideMissionResult = SideMissionScript.control.runSideMission1 ();
			GameControlScript.control.SetSideMissionResult (sideMissionResult);
		} else if (GameControlScript.control.GetLevel () == 2) {
			sideMissionResult = SideMissionScript.control.runSideMission1 ();
			GameControlScript.control.SetSideMissionResult (sideMissionResult);
		} else if (GameControlScript.control.GetLevel () == 3) {
			sideMissionResult = SideMissionScript.control.runSideMission1 ();
			GameControlScript.control.SetSideMissionResult (sideMissionResult);
		}
		*/

        /* //comment out from here to disable extra spawn code

		//if i start even, offset starts false
		offset = false;
		//water index in GameControl
		int water = 1;

		fuckThomasZ = -0.40f;

		int iReplaceLow = 0;

		int iReplaceHigh = map.Count;

		//create extra map tiles
		for (int i = -20; i < map.Count + 20; ++i) {
			//overlaying map
			if (i >= 0 && i < map.Count) {
				for (int j = -10; j < map [i].Count + 10; ++j) {
					//if not overlaying existing map
					if (j < 0 || j >= map [i].Count) {
					
						ExtraWaterSpawnCode (tiles, offset, fuckThomasX, fuckThomasY, fuckThomasZ, i, j);
					}
				}
			} else if (i < 0) {
				for (int j = -10; j < map [iReplaceLow].Count + 10; ++j) {

					ExtraWaterSpawnCode (tiles, offset, fuckThomasX, fuckThomasY, fuckThomasZ, i, j);
				}
			} else if (i >= map.Count ) {
				for (int j = -10; j < map [iReplaceHigh - 1].Count + 10; ++j) {

					ExtraWaterSpawnCode (tiles, offset, fuckThomasX, fuckThomasY, fuckThomasZ, i, j);
				}
			}
			fuckThomasZ += 0.02f;
			offset = !offset;

		}
		*/

        //Set objective for the level
        CompleteLevelConditions.control.ShowObjective();
    }

	private void ExtraWaterSpawnCode(List<GameObject> tiles, bool offset, float fuckThomasX, 
		float fuckThomasY, float fuckThomasZ, int i, int j){
		int voidTile = 3;

		GameObject oneTile = tiles [voidTile];

		tileWidth = oneTile.GetComponent<Renderer> ().bounds.size.x;
		tileHeight = oneTile.GetComponent<Renderer> ().bounds.size.y;

		if (!offset) {
			fuckThomasX = (j * tileWidth);
		} else {
			fuckThomasX = (j * tileWidth) + (tileWidth / 2);
		}
		//sets y to level 0 height
		fuckThomasY = ((i+ 1) * (tileHeight / 3));
		//put y to current level if not 0, not needed, default sea level 0
		//fuckThomasY += ((tileHeight / 3) * map[i][j][1]);

		Vector3 tempVector = new Vector3 (fuckThomasX, fuckThomasY, fuckThomasZ);

		Transform tile = (Transform)Instantiate (oneTile.transform, tempVector, Quaternion.identity);
		tile.SetParent (TileParent);
	}

    public float GetCameraMinX()
    {
        return minCameraX;
    }

    public float GetCameraMinY()
    {
        return minCameraY;
    }

    public float GetCameraMaxX()
    {
        return maxCameraX;
    }

    public float GetCameraMaxY()
    {
        return maxCameraY;
    }
		
	public List<List<GameObject>> GetAStarMap()
	{
		return aStarMap;
	}

    public List<List<List<int>>> GetAStarMapCost()
    {
        return aStarMapCost;
    }

    public float GetTileHeightRatio()
    {
        return tileHeightRatio;
    }
		
	public void PlayerDied (){
		playersAlive = GameControlScript.control.GetInGameCharacterList().Count;
		if (playersAlive <= 0) {
			CompleteLevelConditions.control.LevelFailed();
		}
	}
		
	public Transform GetEnemyParent() {
		return enemyParent;
	}

	public List<EnemySpawner> GetEnemySpawners() {
		return enemySpawners;
	}
}
