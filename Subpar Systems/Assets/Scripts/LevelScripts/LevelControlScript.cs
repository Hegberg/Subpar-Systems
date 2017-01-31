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

    /*
    private List<int[]> map = new List<int[]>();

    private int[] row1 = { 2, 0, 1, 1, 1, 0 , 2, 2};
    private int[] row2 = {   2, 0, 1, 1, 0, 0 };
    private int[] row3 = { 2, 0, 0, 0, 0, 2 };
    private int[] row4 = {   1, 1, 0, 0, 1, 2 };
    private int[] row5 = { 0, 1, 0, 0, 1, 1 };
    private int[] row6 = {   0, 0, 0, 0, 1, 2 };
    private int[] row7 = { 0, 0, 0, 0, 0, 1 };
    private int[] row8 = {   0, 0, 0, 0, 0, 2 };
    private int[] row9 = { 0, 0, 0, 0, 0, 1 };
    */

    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{1,0}, new List<int>{0,1}, new List<int>{2,1}, new List<int>{2,2} },
        new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,1}, new List<int>{0,2}, new List<int>{2,2}, new List<int>{2,2} },
        new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,1}, new List<int>{0,2}, new List<int>{2,2}, new List<int>{2,1} },
        new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{2,1}, new List<int>{2,0} },
        new List<List<int>> {new List<int>{2,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
        new List<List<int>> {new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
        new List<List<int>> {new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
        new List<List<int>> {new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
        new List<List<int>> {new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} }
    };

    //private int[][][] map2 = { new int[][] { } };
    
    //private int numOfRows = 9;

    private int[] playerSpawn1 = { 8, 0 };
    private int[] playerSpawn2 = { 8, 1 };
    private int[] playerSpawn3 = { 8, 2 };
    private int[] playerSpawn4 = { 8, 3 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 0, 5 , 0 };
    private int[] enemySpawn2 = { 1, 4 , 0 };
    private int[] enemySpawn3 = { 1, 5 , 0 };
    private int[] enemySpawn4 = { 2, 4 , 0 };

    private List<int[]> enemySpawnLocations = new List<int[]>();

    private List<List<GameObject>> aStarMap = new List<List<GameObject>>();

    //private float tileWidth = 0.64f;
    private float tileWidth;
    private float tileHeight;

    private float tileHeightRatio = 2.0f;

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

        playerSpawnLocations.Add(playerSpawn1);
        playerSpawnLocations.Add(playerSpawn2);
        playerSpawnLocations.Add(playerSpawn3);
        playerSpawnLocations.Add(playerSpawn4);

        enemySpawnLocations.Add(enemySpawn1);
        enemySpawnLocations.Add(enemySpawn2);
        enemySpawnLocations.Add(enemySpawn3);
        enemySpawnLocations.Add(enemySpawn4);

        CreateMap(map);

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

    public void CreateMap(List<List<List<int>>> map)
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
        for (int i = map.Count - 1; i >= 0; --i)
        {
            aStarMap.Add(new List<GameObject> { });

            for (int j = 0; j < map[i].Count; ++j)
            {
                List<GameObject> tiles = GameControlScript.control.GetTiles();

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
                fuckThomasZ += 0.01f;

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
                //Debug.Log("test : " + (map.Count - i - 1));
                aStarMap[map.Count - i - 1].Add(tile.gameObject);

                //spawn character code
                for (int k = 0; k < playerSpawnLocations.Count; ++k)
                {
                    if (playerSpawnLocations[k][0] == i && playerSpawnLocations[k][1] == j)
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
                        break;
                    }
                }

                //spawn enemy code
                for (int k = 0; k < enemySpawnLocations.Count; ++k)
                {
                    if (enemySpawnLocations[k][0] == i && enemySpawnLocations[k][1] == j)
                    {
                        tempVector.z -= 0.01f;

                        tempVector.y += (oneTile.GetComponent<Renderer>().bounds.size.y / (2 * tileHeightRatio));

                        Transform enemy = (Transform)Instantiate(
                            GameControlScript.control.GetEnemies()[enemySpawnLocations[k][2]].transform,
                            tempVector, Quaternion.identity);
                        enemy.gameObject.GetComponent<GenericEnemyScript>().SetTileOccuping(tile.gameObject);
                        tile.gameObject.GetComponent<GenericEarthScript>().SetOccupingObject(enemy.gameObject);
                        enemy.SetParent(enemyParent);
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

            //fill new list
        }

        Debug.Log(aStarMap.Count);

        for (int i = 0; i < aStarMap.Count; ++i)
        {
            Debug.Log("Count of i " + aStarMap[i].Count);
        }

        aStarMap.Reverse();

        /*
        for (int i = 0; i < aStarMap.Count; ++i)
        {
            Debug.Log("Count of i " + aStarMap[i].Count);
        }
        */

        //angled generation commented out
        /*
        for (int i = 0; i < map.Count; ++i)
        {
            for (int j = 0; j < map[i].Length; ++j)
            {
                fuckThomasX = (j * (tileWidth)/2);
                fuckThomasY = (-i * tileHeight) - (j * (tileHeight / 2));
                fuckThomasZ -= 0.01f;
                Transform tile = (Transform)Instantiate(GameControlScript.control.GetTiles()[map[i][j]],
                    new Vector3(fuckThomasX,
                    fuckThomasY,
                    fuckThomasZ),
                    Quaternion.identity);
                tile.SetParent(TileParent);
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
        }
        */

        //set camera limits to be local to preexisting camera position
        maxCameraX += -1.0f;
        maxCameraY += -0.5f;
        minCameraX += 1.0f;
        minCameraY += 0.5f;
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

    public float GetTileHeightRatio()
    {
        return tileHeightRatio;
    }

}
