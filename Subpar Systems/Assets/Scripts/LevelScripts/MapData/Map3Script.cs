using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Script : MonoBehaviour
{

    //0 -> earth, 1 -> water, 2 -> rock
    //first element = tile type, second element = tile height
    //list<list> is row, list<list<list>>> is item in row
    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> { //1
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //2
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //3
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{2,2}, new List<int>{2,2}, new List<int>{2,2}, new List<int>{0,0}},
        new List<List<int>> { //4
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
       new List<List<int>> { //5
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,2}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
       new List<List<int>> { //6
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,2}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //7
            new List<int>{0,0}, new List<int>{2,2}, new List<int>{2,2}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{2,2}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //8
            new List<int>{0,0}, new List<int>{2,2}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //9
            new List<int>{0,0}, new List<int>{2,2}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
       new List<List<int>> { //10
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //11
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //12
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
       new List<List<int>> { //13
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
    };

    private int[] playerSpawn1 = { 3, 11 };
    private int[] playerSpawn2 = { 4, 11 };
    private int[] playerSpawn3 = { 3, 12 };
    private int[] playerSpawn4 = { 4, 12 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 0, 0, 0 };
    private int[] enemySpawn2 = { 1, 1, 0 };
    private int[] enemySpawn3 = { 2, 0, 0 };
    private int[] enemySpawn4 = { 3, 1, 0 };
    private int[] enemySpawn5 = { 4, 0, 0 };
    private int[] enemySpawn6 = { 5, 1, 0 };
    private int[] enemySpawn7 = { 6, 0, 0 };
    private int[] enemySpawn8 = { 7, 1, 0 };



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
