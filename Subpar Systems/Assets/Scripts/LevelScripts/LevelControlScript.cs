using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour {

    public static LevelControlScript control;

    public Transform TileParent;

    public Transform characterParent;

    private float maxCameraY = 0f;
    private float maxCameraX = 0f;
    private float minCameraY = 0f;
    private float minCameraX = 0f;

    private List<int[]> map = new List<int[]>();

    private int[] row1 = { 2, 0, 1, 1, 1, 0 };
    private int[] row2 = {   2, 0, 1, 1, 0, 0 };
    private int[] row3 = { 2, 0, 0, 0, 0, 2 };
    private int[] row4 = {   1, 1, 0, 0, 1, 2 };
    private int[] row5 = { 0, 1, 0, 0, 1, 1 };
    private int[] row6 = {   0, 0, 0, 0, 1, 2 };
    private int[] row7 = { 0, 0, 0, 0, 0, 1 };
    private int[] row8 = {   0, 0, 0, 0, 0, 2 };
    private int[] row9 = { 0, 0, 0, 0, 0, 1 };

    //some reason haveing the order 0,2,4,6 makes it so the 3rd one can't be clicked, no idea why but this out of order order works fine
    private int[] spawn1 = { 8, 0 };
    private int[] spawn2 = { 8, 1 };
    private int[] spawn3 = { 8, 2 };
    private int[] spawn4 = { 8, 3 };

    private List<int[]> spawnLocations = new List<int[]>();

    //private float tileWidth = 0.64f;
    private float tileWidth;
    private float tileHeight;

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

        //need to add all rows to map
        map.Add(row1);
        map.Add(row2);
        map.Add(row3);
        map.Add(row4);
        map.Add(row5);
        map.Add(row6);
        map.Add(row7);
        map.Add(row8);
        map.Add(row9);

        spawnLocations.Add(spawn1);
        spawnLocations.Add(spawn2);
        spawnLocations.Add(spawn3);
        spawnLocations.Add(spawn4);

        CreateMap(map);

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CheckAjacentTiles()
    {
        //need to implement
    }

    public void BroadcastRefreshActionsToCharacters()
    {
        BroadcastMessage("RefreshActions");
    }

    public void BroadcastRemoveActionsToCharacters()
    {
        BroadcastMessage("RemoveActions");
    }

    public void CreateMap(List<int[]> map)
    {
        minCameraX = 0f;
        minCameraY = 0f;

        float fuckThomasX = 0.0f;
        float fuckThomasY = 0.0f;
        float fuckThomasZ = 0.0f;

        //0 is earth, 1 is water, 2 is mountian
        //start at bottom row and build up

        int characterSpawning = 0;
        bool offset = false;
        for (int i = map.Count - 1; i >= 0; --i)
        {
            for (int j = 0; j < map[i].Length; ++j)
            {
                List<GameObject> tiles = GameControlScript.control.GetTiles();

                GameObject oneTile = tiles[map[i][j]];

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
                fuckThomasY = ((map.Count - i) * (((tileHeight*2)/3) /2));
                fuckThomasZ += 0.01f;

                Vector3 tempVector = new Vector3(fuckThomasX, fuckThomasY, fuckThomasZ);

                Transform tile = (Transform)Instantiate(oneTile.transform,tempVector, Quaternion.identity);
                tile.SetParent(TileParent);

                //spawn character code
                for (int k = 0; k < spawnLocations.Count; ++k)
                {
                    if (spawnLocations[k][0] == i && spawnLocations[k][1] == j)
                    {   
                        tempVector.z -= 2;

                        tempVector.y += (oneTile.GetComponent<Renderer>().bounds.size.y * 2)/3;

                        Transform character = (Transform)Instantiate(
                            GameControlScript.control.GetCharacters()[characterSpawning],
                            tempVector, Quaternion.identity);
                        character.gameObject.GetComponent<GenericCharacterScript>().SetTileOccuping(tile.gameObject);
                        character.SetParent(characterParent);
                        characterSpawning += 1;
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
        }
        

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
}
