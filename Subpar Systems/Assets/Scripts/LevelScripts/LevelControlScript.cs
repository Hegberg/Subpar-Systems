using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour {

    public static LevelControlScript control;

    public Transform TileParent;

    public Camera mainCamera;
    private float cameraMoveSpeed = 0.25f;

    //need the amount of lists allocated to be the amount of rows added
    private int[][] map =  new int[6][];
    //rows need to be same length
    private int[] row1 = { 2, 0, 1, 1, 1, 0 };
    private int[] row2 = { 2, 0, 0, 1, 0, 0 };
    private int[] row3 = { 2, 0, 0, 0, 0, 2 };
    private int[] row4 = { 1, 1, 0, 0, 2, 2 };
    private int[] row5 = { 0, 0, 0, 0, 2, 2 };
    private int[] row6 = { 0, 0, 0, 0, 0, 2 };

    private int rowLength;

    //some reason haveing the order 0,2,4,6 makes it so the 3rd one can't be clicked, no idea why but this out of order order works fine
    private Vector2 spawn1 = new Vector2(6.0f, 0);
    private Vector2 spawn2 = new Vector2(0.0f, 0);
    private Vector2 spawn3 = new Vector2(4.0f, 0);
    private Vector2 spawn4 = new Vector2(2.0f, 0);

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
        
        map[0] = row1;
        map[1] = row2;
        map[2] = row3;
        map[3] = row4;
        map[4] = row5;
        map[5] = row6;

        rowLength = row1.Length;

        CreateMap(map, rowLength);
        GameControlScript.control.SpawnCharacters(spawn1, spawn2, spawn3, spawn4);

    }
	
	// Update is called once per frame
	void Update () {
        CameraMove();
	}

    private void CameraMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mainCamera.transform.localPosition += new Vector3(-cameraMoveSpeed, -cameraMoveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mainCamera.transform.localPosition += new Vector3(cameraMoveSpeed, cameraMoveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            mainCamera.transform.localPosition += new Vector3(-cameraMoveSpeed, cameraMoveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            mainCamera.transform.localPosition += new Vector3(cameraMoveSpeed, -cameraMoveSpeed, 0);
        }
    }

    public void CreateMap(int[][] map, int sizeOfSecondaryArrays)
    {
        //0 is earth, 1 is water, 2 is mountian
        //start at bottom row and build up
        for (int i = map.Length - 1; i >= 0; --i)
        {
            for (int j = 0; j < sizeOfSecondaryArrays; ++j)
            {
                Transform tile = (Transform)Instantiate(GameControlScript.control.GetTiles()[map[i][j]], 
                    new Vector3(0 + (j * GameControlScript.control.GetTileWidth()), 
                    0 + (((map.Length-1)-i) * GameControlScript.control.GetTileWidth()), 0), Quaternion.identity);
                tile.SetParent(TileParent);
            }
        }
    }
}
