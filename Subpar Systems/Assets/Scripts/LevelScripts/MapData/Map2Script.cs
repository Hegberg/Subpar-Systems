﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2Script : MonoBehaviour
{

    //0 -> earth, 1 -> water, 2 -> rock
    //first element = tile type, second element = tile height
    //list<list> is row, list<list<list>>> is item in row
    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> { //0
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{2,1}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //1
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //2
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //3
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //4
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //5
            new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //6
            new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //7
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //8
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //9
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //10
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //11
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //12
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //13
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //14
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //15
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //16
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //17
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}, new List<int>{0,0}},
        new List<List<int>> { //18
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}, new List<int>{0,0}},
        new List<List<int>> { //19
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}}

    };

    private int[] playerSpawn1 = { 3, 5 };
    private int[] playerSpawn2 = { 2, 7 };
    private int[] playerSpawn3 = { 2, 4 };
    private int[] playerSpawn4 = { 1, 6 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 1, 1, 3 };
    private int[] enemySpawn2 = { 0, 3, 0 };
    private int[] enemySpawn3 = { 4, 7, 0 };
    private int[] enemySpawn4 = { 4, 11, 2 };
    private int[] enemySpawn5 = { 5, 9, 1 };
    private int[] enemySpawn6 = { 5, 4, 0 };
    private int[] enemySpawn7 = { 6, 2, 2 };
    private int[] enemySpawn8 = { 6, 19, 0 };
    private int[] enemySpawn9 = { 7, 17, 1 };
    private int[] enemySpawn10 = { 9, 5, 2 };
    private int[] enemySpawn11 = { 9, 8, 2 }; //Broken
    private int[] enemySpawn12 = { 9, 11, 0 };
    private int[] enemySpawn13 = { 9, 14, 1 };

    //TANKS SPAWNS AT 10,6
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

    private EnemySpawner enemySpawner1 = new EnemySpawner(new List<int>{5,0}, 2, 2 , 0, 0);
    private EnemySpawner enemySpawner2 = new EnemySpawner(new List<int> { 9, 4 }, 1, 1, 2, 3);
    private EnemySpawner enemySpawner3 = new EnemySpawner(new List<int> { 9, 8 }, 2, 2, 1, 2);
    private EnemySpawner enemySpawner4 = new EnemySpawner(new List<int> { 9, 12 }, 2, 1, 2, 2);
    private EnemySpawner enemySpawner5 = new EnemySpawner(new List<int> { 5, 19 }, 1, 2, 0, 2);
    private EnemySpawner enemySpawner6 = new EnemySpawner(new List<int> { 7, 19 }, 2, 2, 0, 3);
    private EnemySpawner enemySpawner7 = new EnemySpawner(new List<int> { 9, 0 }, 1, 2, 3, 3);

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
