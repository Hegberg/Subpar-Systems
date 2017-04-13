using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Script : MonoBehaviour
{

    //0 -> earth, 1 -> water, 2 -> rock
    //first element = tile type, second element = tile height
    //list<list> is row, list<list<list>>> is item in row
    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> { //0
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0},
            new List<int>{2,0}, new List<int>{2,0}},
        new List<List<int>> { //1
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0},
            new List<int>{2,0}, new List<int>{2,0}},
        new List<List<int>> { //2
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0},
            new List<int>{2,0}, new List<int>{2,0}},
        new List<List<int>> { //3
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{2,0}, new List<int>{2,0}},
        new List<List<int>> { //4
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{2,0}, new List<int>{2,0}},
        new List<List<int>> { //5
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{2,0}},
        new List<List<int>> { //6
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{2,0}},
        new List<List<int>> { //7
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //8
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //9
            new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{0,0},
            new List<int>{2,0}, new List<int>{0,0}},
        new List<List<int>> { //10
            new List<int>{2,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{2,0}, new List<int>{0,0}},
        new List<List<int>> { //11
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //12
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //13
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //14
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //15
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //16
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //17
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //18
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0},
            new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //19
            new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0}, new List<int>{2,0},
            new List<int>{2,0}, new List<int>{0,0}}
    };

    private int[] playerSpawn1 = { 1, 1 };
    private int[] playerSpawn2 = { 0, 2 };
    private int[] playerSpawn3 = { 0, 0 };
    private int[] playerSpawn4 = { 0, 1 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 3, 10, 0 };
    private int[] enemySpawn2 = { 3, 11, 1 };
    private int[] enemySpawn3 = { 4, 7, 1 };
    private int[] enemySpawn4 = { 4, 6, 0 };
    private int[] enemySpawn5 = { 5, 6, 1 };
    private int[] enemySpawn6 = { 5, 12, 1 };
    private int[] enemySpawn7 = { 4, 14, 1 };
    private int[] enemySpawn8 = { 5, 8, 0 };
    // private int[] enemySpawn11 = { 0, 1, 0 }; //Broken
    // private int[] enemySpawn12 = { 0, 2, 0 };
    // private int[] enemySpawn13 = { 0, 3, 0 };
    // private int[] enemySpawn14 = { 0, 4, 0 };
    // private int[] enemySpawn15 = { 0, 5, 0 };
    // private int[] enemySpawn16 = { 0, 6, 0 };
    // private int[] enemySpawn17 = { 0, 7, 0 };
    // private int[] enemySpawn18 = { 0, 8, 0 };
    // private int[] enemySpawn19 = { 0, 9, 0 };
    //// private int[] enemySpawn20 = { 0, 10, 0 };

    // //private int[] enemySpawn21 = { 0, 0, 0 };
    // private int[] enemySpawn22 = { 2, 9, 0 };
    // private int[] enemySpawn23 = { 4, 9, 0 };
    // private int[] enemySpawn24 = { 6, 9, 0 };
    // private int[] enemySpawn25 = { 8, 9, 0 };
    // private int[] enemySpawn26 = { 10, 9, 0 };
    //private int[] enemySpawn27 = { 12, 9, 0 };
    //private int[] enemySpawn28 = { 14, 9, 0 };
    //private int[] enemySpawn29 = { 16, 9, 0 };
    //private int[] enemySpawn30 = { 18, 9, 0 };
    //private int[] enemySpawn31 = { 18, 1, 0 }; //Broken
    //private int[] enemySpawn32 = { 18, 2, 0 };
    //private int[] enemySpawn33 = { 18, 3, 0 };
    //private int[] enemySpawn34 = { 18, 4, 0 };
    //private int[] enemySpawn35 = { 18, 5, 0 };
    //private int[] enemySpawn36 = { 18, 6, 0 };
    //private int[] enemySpawn37 = { 18, 7, 0 };
    //private int[] enemySpawn38 = { 18, 8, 0 };
    //private int[] enemySpawn39 = { 18, 9, 0 };
    //private int[] enemySpawn40 = { 18, 10, 0 };



    private List<int[]> enemySpawnLocations = new List<int[]>();

	private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    //Parameters are - position of spawn, maxSpawnCount, spawnRate(turns between each spawn), enemyType, turns to first spawn

    //y's have a max x of 6, 

    //Avaialbe y's, 7 8 9 10 11 12 13 14 15 16 17 18 19
    private EnemySpawner enemySpawner1 = new EnemySpawner(new List<int>{6,8}, 10, 2 , 1, 1);
    private EnemySpawner enemySpawner2 = new EnemySpawner(new List<int> { 6, 9 }, 10, 2, 2, 2);
    private EnemySpawner enemySpawner3 = new EnemySpawner(new List<int> { 6, 10 }, 10, 2, 1, 0);
    private EnemySpawner enemySpawner4 = new EnemySpawner(new List<int> { 6, 11 }, 10, 2, 2, 1);
    private EnemySpawner enemySpawner5 = new EnemySpawner(new List<int> { 6, 12 }, 10, 2, 3, 0);
    private EnemySpawner enemySpawner6 = new EnemySpawner(new List<int> { 6, 13 }, 10, 2, 1, 1);
    private EnemySpawner enemySpawner7 = new EnemySpawner(new List<int> { 6, 14 }, 10, 3, 2, 2);
    private EnemySpawner enemySpawner8 = new EnemySpawner(new List<int> { 6, 15 }, 10, 2, 1, 1);
    private EnemySpawner enemySpawner9 = new EnemySpawner(new List<int> { 6, 16 }, 10, 3, 2, 0);
    private EnemySpawner enemySpawner10 = new EnemySpawner(new List<int> { 6, 17 }, 10, 2, 1, 2);

    //ULTRA SPAWNS. DISABLE IF SIDE MISSION PASSED
    private EnemySpawner enemySpawner11 = new EnemySpawner(new List<int> { 6, 18 }, 1, 2, 3, 2);
    private EnemySpawner enemySpawner12 = new EnemySpawner(new List<int> { 6, 19 }, 1, 2, 3, 2);


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

        //enemySpawnLocations.Add(enemySpawn11);
        //enemySpawnLocations.Add(enemySpawn12);
        //enemySpawnLocations.Add(enemySpawn13);
        //enemySpawnLocations.Add(enemySpawn14);
        //enemySpawnLocations.Add(enemySpawn15);
        //enemySpawnLocations.Add(enemySpawn16);
        //enemySpawnLocations.Add(enemySpawn17);
        //enemySpawnLocations.Add(enemySpawn18);
        //enemySpawnLocations.Add(enemySpawn19);
        ////enemySpawnLocations.Add(enemySpawn20);

        ////enemySpawnLocations.Add(enemySpawn21);
        //enemySpawnLocations.Add(enemySpawn22);
        //enemySpawnLocations.Add(enemySpawn23);
        //enemySpawnLocations.Add(enemySpawn24);
        //enemySpawnLocations.Add(enemySpawn25);
        //enemySpawnLocations.Add(enemySpawn26);
        //enemySpawnLocations.Add(enemySpawn27);
        //enemySpawnLocations.Add(enemySpawn28);
        //enemySpawnLocations.Add(enemySpawn29);
        //enemySpawnLocations.Add(enemySpawn30);
        //enemySpawnLocations.Add(enemySpawn31);
        //enemySpawnLocations.Add(enemySpawn32);
        //enemySpawnLocations.Add(enemySpawn33);
        //enemySpawnLocations.Add(enemySpawn34);
        //enemySpawnLocations.Add(enemySpawn35);
        //enemySpawnLocations.Add(enemySpawn36);
        //enemySpawnLocations.Add(enemySpawn37);
        //enemySpawnLocations.Add(enemySpawn38);
        //enemySpawnLocations.Add(enemySpawn39);
        //enemySpawnLocations.Add(enemySpawn40);

        enemySpawners.Add(enemySpawner1);
        enemySpawners.Add(enemySpawner2);
        enemySpawners.Add(enemySpawner3);
        enemySpawners.Add(enemySpawner4);
        enemySpawners.Add(enemySpawner5);
        enemySpawners.Add(enemySpawner6);
        enemySpawners.Add(enemySpawner7);
        enemySpawners.Add(enemySpawner8);
        enemySpawners.Add(enemySpawner9);
        enemySpawners.Add(enemySpawner10);
        //if side mission 2 failed, spawn these
        if (GameControlScript.control.GetSideMission2Result() != 1)
        {
            enemySpawners.Add(enemySpawner11);
            enemySpawners.Add(enemySpawner12);
        }

        StartCoroutine(MapGenerateWait());
        //LevelControlScript.control.CreateMap (map, playerSpawnLocations, enemySpawnLocations);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MapGenerateWait()
    {
        TraitSpriteControl.control.UnShowTraits();
        yield return new WaitForSeconds(0.01f);
		LevelControlScript.control.CreateMap(map, playerSpawnLocations, enemySpawnLocations, enemySpawners);
    }
}
