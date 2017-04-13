using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner {

	private List<int> tilePosition;
	private int maxSpawnCount =0;
	//turns from previous spawn, so after spawned, this goes to 1 on next turn
	private int spawnRate = 0;
	private int amountOfSpawnsUsed = 0;
	private int enemyType = 0;
	//
	private int turnsFromPreviousSpawn = 0;

	private int startTurn = 0;
	private int turnsToStartSpawning = 0;

	public EnemySpawner(List<int> newTilePosition = default(List<int>), int newMaxSpawnCount = 0, 
		int newSpawnRate = 0, int newEnemyType = 0, int newStartTurn = 0) {
		tilePosition = newTilePosition;
		maxSpawnCount = newMaxSpawnCount;
		spawnRate = newSpawnRate;
		amountOfSpawnsUsed = 0;
		enemyType = newEnemyType;
		turnsFromPreviousSpawn = newStartTurn;
		startTurn = newStartTurn;
	}

	public void SpawnEnemy() {
		turnsToStartSpawning += 1;
		//Debug.Log (turnsToStartSpawning + " a " + startTurn + " b");
		//if at start spawn turn, start trying to spawn
		if (turnsToStartSpawning >= startTurn) {
			turnsFromPreviousSpawn += 1;
			//if has spawns left and enough turns between spawns, try spawning, 
			//only if spawn succeeds do rest timer and increment amount of eneimeis spawned
			if (amountOfSpawnsUsed < maxSpawnCount && turnsFromPreviousSpawn >= spawnRate) {
				if (LevelControlScript.control.SpawnEnemy (tilePosition [0], tilePosition [1], enemyType)) {
					turnsFromPreviousSpawn = 0;
					amountOfSpawnsUsed += 1;
				}
			}
		}
	}

	public void SetData(List<int> newTilePosition, int newMaxSpawnCount, int newSpawnRate, int newAmountOfSpawnsUsed, int newEnemyType) {
		tilePosition = newTilePosition;
		maxSpawnCount = newMaxSpawnCount;
		spawnRate = newSpawnRate;
		amountOfSpawnsUsed = newAmountOfSpawnsUsed;
		enemyType = newEnemyType;
	}

	public List<int> GetTilePosition(){
		return tilePosition;
	}

	public int GetMaxSpawnCount() {
		return maxSpawnCount;
	}

	public int GetSpawnRate() {
		return spawnRate;
	}

	public int GetAmountOfSpawnsUsed() {
		return amountOfSpawnsUsed;
	}
}
