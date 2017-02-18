using System.Collections;
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

	public void CreateMap(List<List<List<int>>> map, List<int[]> playerSpawnLocations, List<int[]> enemySpawnLocations)
    {
        minCameraX = 0f;
        minCameraY = 0f;

        float fuckThomasX = 0.0f;
        float fuckThomasY = 0.0f;
        float fuckThomasZ = 0.0f;

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

                Transform tile = (Transform)Instantiate(oneTile.transform,tempVector, Quaternion.identity);
                tile.SetParent(TileParent);

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
						playersAlive += 1;
                        characterSpawning += 1;
                        GameControlScript.control.AddCharacterToInGameList(character.gameObject);
                        break;
                    }
                }

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
			fuckThomasZ += 0.01f;
        }


        aStarMap.Reverse();
        aStarMapCost.Reverse();

        //set camera limits to be local to preexisting camera position
        maxCameraX += -1.0f;
        maxCameraY += -0.5f;
        minCameraX += 1.0f;
        minCameraY += 0.5f;

		//if i start even, offset starts false
		offset = false;
		//water index in GameControl
		int water = 1;

		fuckThomasZ = -0.12f;

		int iReplaceLow = 0;

		int iReplaceHigh = map.Count;



		//create extra water
		for (int i = -12; i < map.Count + 12; ++i) {
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
			fuckThomasZ += 0.01f;
			offset = !offset;

		}
    }

	private void ExtraWaterSpawnCode(List<GameObject> tiles, bool offset, float fuckThomasX, 
		float fuckThomasY, float fuckThomasZ, int i, int j){
		int water = 1;

		GameObject oneTile = tiles [water];

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
		playersAlive -= 1;
		if (playersAlive <= 0) {
			TurnControlScript.control.LevelFailed ();
		}
	}

	public void PlayerSpawned() {
		playersAlive += 1;
	}
		
}
