using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Script : MonoBehaviour
{

    //0 -> earth, 1 -> water, 2 -> rock
    //first element = tile type, second element = tile height
    //list<list> is row, list<list<list>>> is item in row
    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
       new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{2,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1},
            new List<int>{2,1}, new List<int>{2,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
       new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
        new List<List<int>> { 
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} },
       new List<List<int>> { 
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1}, new List<int>{0,1} }
    };

    private int[] playerSpawn1 = { 4, 0 };
    private int[] playerSpawn2 = { 5, 0 };
    private int[] playerSpawn3 = { 4, 1 };
    private int[] playerSpawn4 = { 5, 1 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 1, 1, 0 };
    private int[] enemySpawn2 = { 0, 4, 0 };
    private int[] enemySpawn3 = { 1, 4, 0 };
    private int[] enemySpawn4 = { 5, 4, 0 };
    private int[] enemySpawn5 = { 4, 5, 0 };
    private int[] enemySpawn6 = { 5, 5, 0 };
    private int[] enemySpawn7 = { 9, 5, 0 };
    private int[] enemySpawn8 = { 8, 6, 0 };
    private int[] enemySpawn9 = { 9, 6, 0 };
    private int[] enemySpawn10 = { 1, 7, 0 };
    private int[] enemySpawn11 = { 0, 8, 0 };
    private int[] enemySpawn12 = { 7, 10, 0 };
    private int[] enemySpawn13 = { 8, 10, 0 };
    private int[] enemySpawn14 = { 3, 11, 0 };
    private int[] enemySpawn15 = { 7, 11, 0 };
    private int[] enemySpawn16 = { 8, 4, 0 };



    private List<int[]> enemySpawnLocations = new List<int[]>();

    // Use this for initialization
    void Start()
    {
        playerSpawnLocations.Add(playerSpawn1);
        playerSpawnLocations.Add(playerSpawn2);
        playerSpawnLocations.Add(playerSpawn3);
        playerSpawnLocations.Add(playerSpawn4);

        enemySpawnLocations.Add(enemySpawn1);
        enemySpawnLocations.Add(enemySpawn2);
        enemySpawnLocations.Add(enemySpawn3);
        enemySpawnLocations.Add(enemySpawn4);
        enemySpawnLocations.Add(enemySpawn5);
        enemySpawnLocations.Add(enemySpawn6);
        enemySpawnLocations.Add(enemySpawn7);
        enemySpawnLocations.Add(enemySpawn8);
        enemySpawnLocations.Add(enemySpawn9);
        enemySpawnLocations.Add(enemySpawn10);
        enemySpawnLocations.Add(enemySpawn11);
        enemySpawnLocations.Add(enemySpawn12);
        enemySpawnLocations.Add(enemySpawn13);
        enemySpawnLocations.Add(enemySpawn14);
        enemySpawnLocations.Add(enemySpawn15);
        enemySpawnLocations.Add(enemySpawn16);
        StartCoroutine(MapGenerateWait());
        //LevelControlScript.control.CreateMap (map, playerSpawnLocations, enemySpawnLocations);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MapGenerateWait()
    {
        yield return new WaitForSeconds(0.01f);
        LevelControlScript.control.CreateMap(map, playerSpawnLocations, enemySpawnLocations);
    }
}
