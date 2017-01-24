using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour {

    public static LevelControlScript control;

    public Transform TileParent;

    public Transform characterParent;

    public Camera mainCamera;
    private float cameraMoveSpeed = 0.05f;

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
    private Vector2 spawn1 = new Vector2(1.92f, 0.40f);
    private Vector2 spawn2 = new Vector2(0.0f, 0.40f);
    private Vector2 spawn3 = new Vector2(1.28f, 0.40f);
    private Vector2 spawn4 = new Vector2(0.64f, 0.40f);

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

        CreateMap(map);
        SpawnCharacters(spawn1, spawn2, spawn3, spawn4);

    }
	
	// Update is called once per frame
	void Update () {
        CameraMove();
	}

    private void CameraMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && 
            mainCamera.transform.position.x > minCameraX)
        {
            mainCamera.transform.localPosition += new Vector3(-cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) &&
            mainCamera.transform.position.x < maxCameraX )
        {
            mainCamera.transform.localPosition += new Vector3(cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) &&
            mainCamera.transform.position.y < maxCameraY)
        {
            mainCamera.transform.localPosition += new Vector3(0, cameraMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) &&
            mainCamera.transform.position.y > minCameraY)
        {
            mainCamera.transform.localPosition += new Vector3(0, -cameraMoveSpeed, 0);
        }
    }

    //Spawns 4 characters in correct places
    public void SpawnCharacters(Vector2 place1, Vector2 place2, Vector2 place3, Vector2 place4)
    {
        int selected = 0;
        for (int i = 0; i < GameControlScript.control.GetChosen().Capacity; ++i)
        {
            if (GameControlScript.control.GetChosen()[i])
            {
                if (selected == 0)
                {
                    Transform character = (Transform)Instantiate(GameControlScript.control.GetCharacters()[i], place1, Quaternion.identity);
                    character.SetParent(characterParent);
                    selected += 1;
                }
                else if (selected == 1)
                {
                    Transform character = (Transform)Instantiate(GameControlScript.control.GetCharacters()[i], place2, Quaternion.identity);
                    character.SetParent(characterParent);
                    selected += 1;
                }
                else if (selected == 2)
                {
                    Transform character = (Transform)Instantiate(GameControlScript.control.GetCharacters()[i], place3, Quaternion.identity);
                    character.SetParent(characterParent);
                    selected += 1;
                }
                else if (selected == 3)
                {
                    Transform character = (Transform)Instantiate(GameControlScript.control.GetCharacters()[i], place4, Quaternion.identity);
                    character.SetParent(characterParent);
                    selected += 1;
                }
            }
        }
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

                Transform tile = (Transform)Instantiate(oneTile.transform,
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
}
