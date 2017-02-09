using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2Script : MonoBehaviour
{

    //0 -> earth, 1 -> water, 2 -> rock
    //first element = tile type, second element = tile height
    //list<list> is row, list<list<list>>> is item in row
    private List<List<List<int>>> map = new List<List<List<int>>> {
        new List<List<int>> { //1
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //2
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //3
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //4
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //5
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //6
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //7
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //8
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
        new List<List<int>> { //9
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
       new List<List<int>> { //10
            new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}, new List<int>{1,0}},
       new List<List<int>> { //11
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //12
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //13
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //14
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,1}, new List<int>{2,1}, new List<int>{2,1}},
       new List<List<int>> { //15
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
       new List<List<int>> { //16
            new List<int>{0,0}, new List<int>{2,1}, new List<int>{2,1}, new List<int>{2,1}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //17
            new List<int>{2,1}, new List<int>{2,1}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //18
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //19
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}},
        new List<List<int>> { //20
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
            new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}}
    };

    private int[] playerSpawn1 = { 4, 3 };
    private int[] playerSpawn2 = { 6, 3 };
    private int[] playerSpawn3 = { 4, 5 };
    private int[] playerSpawn4 = { 6, 5 };

    private List<int[]> playerSpawnLocations = new List<int[]>();


    //x, y, enemy type
    private int[] enemySpawn1 = { 3, 0, 0 };
    private int[] enemySpawn2 = { 5, 0, 0 };
    private int[] enemySpawn3 = { 5, 7, 0 };
    private int[] enemySpawn4 = { 4, 8, 0 };
    private int[] enemySpawn5 = { 6, 10, 0 };
    private int[] enemySpawn6 = { 8,11, 0 };
    private int[] enemySpawn7 = { 4, 12, 0 };
    private int[] enemySpawn8 = { 8, 14, 0 };
    private int[] enemySpawn9 = { 9, 15, 0 };
    private int[] enemySpawn10 = { 8, 16, 0 };
    private int[] enemySpawn11 = { 1, 16, 0 };
    private int[] enemySpawn12 = { 3, 17, 0 };
    private int[] enemySpawn13 = { 2, 18, 0 };




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
