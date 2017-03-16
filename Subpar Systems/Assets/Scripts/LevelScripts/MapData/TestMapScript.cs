using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapScript : MonoBehaviour {

	//0 -> earth, 1 -> water, 2 -> rock
	//first element = tile type, second element = tile height
	//list<list> is row, list<list<list>>> is item in row
	private List<List<List<int>>> map = new List<List<List<int>>> {
		new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
			new List<int>{1,0}, new List<int>{0,1}, new List<int>{2,1}, new List<int>{0,2} },
		new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{1,0}, new List<int>{1,0},
			new List<int>{0,1}, new List<int>{0,2}, new List<int>{2,2}, new List<int>{0,2} },
		new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,1}, new List<int>{0,2}, new List<int>{2,2}, new List<int>{0,1} },
		new List<List<int>> {new List<int>{2,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,1}, new List<int>{0,1}, new List<int>{2,1}, new List<int>{2,0} },
		new List<List<int>> {new List<int>{2,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
		new List<List<int>> {new List<int>{1,0}, new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
		new List<List<int>> {new List<int>{1,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,0}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} },
		new List<List<int>> {new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,0}, new List<int>{0,8}, new List<int>{2,0}, new List<int>{2,0} },
		new List<List<int>> {new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0}, new List<int>{0,0},
			new List<int>{0,8}, new List<int>{0,0}, new List<int>{2,0}, new List<int>{2,0} }
	};

	private int[] playerSpawn1 = { 0, 8 };
	private int[] playerSpawn2 = { 1, 8 };
	private int[] playerSpawn3 = { 2, 8 };
	private int[] playerSpawn4 = { 3, 8 };

	private List<int[]> playerSpawnLocations = new List<int[]>();


	//x, y, enemy type
	private int[] enemySpawn1 = { 5, 0 , 0 };
	private int[] enemySpawn2 = { 4, 1 , 0 };
	private int[] enemySpawn3 = { 5, 1 , 0 };
	private int[] enemySpawn4 = { 4, 2 , 0 };

	private List<int[]> enemySpawnLocations = new List<int[]>();

	private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

	// Use this for initialization
	void Start () {
		playerSpawnLocations.Add(playerSpawn1);
		playerSpawnLocations.Add(playerSpawn2);
		playerSpawnLocations.Add(playerSpawn3);
		playerSpawnLocations.Add(playerSpawn4);

		enemySpawnLocations.Add(enemySpawn1);
		enemySpawnLocations.Add(enemySpawn2);
		enemySpawnLocations.Add(enemySpawn3);
		enemySpawnLocations.Add(enemySpawn4);
		StartCoroutine (MapGenerateWait());
		//LevelControlScript.control.CreateMap (map, playerSpawnLocations, enemySpawnLocations);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MapGenerateWait()
	{
		yield return new WaitForSeconds(0.01f);
		LevelControlScript.control.CreateMap (map, playerSpawnLocations, enemySpawnLocations, enemySpawners);
	}
}
