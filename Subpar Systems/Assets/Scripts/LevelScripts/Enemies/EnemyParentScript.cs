using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentScript : MonoBehaviour {

	public static EnemyParentScript control;

	private int enemiesAlive = 0;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnemyDied(){
		enemiesAlive -= 1;
		CheckAmountOfRemainingEnemies ();
	}

	public void EnemyCreated() {
		enemiesAlive += 1;
	}

	public void CheckAmountOfRemainingEnemies(){
		if (enemiesAlive <= 0) {
			enemiesAlive = 0;
			CompleteLevelConditions.control.AllEnimiesDead();
		}
	}

	public void StartAITurn(){
		StartCoroutine (AIEnnumerate());
	}

    private void BroadcastAttack()
    {
        //BroadcastMessage("Attack");
		foreach(var enemy in GameControlScript.control.GetInGameEnemyList())
		{
			enemy.GetComponent<GenericEnemyScript>().Attack();
		}
    }

	IEnumerator AIEnnumerate()
	{
		int x = 5;
		//yield return new WaitForSeconds(0.01f);
		for (int i = 0; i < GameControlScript.control.GetInGameEnemyList ().Count; ++i) {
			GameObject enemy = GameControlScript.control.GetInGameEnemyList () [i];

			yield return new WaitForSeconds(0.1f);

			if (enemy != null) {
				enemy.GetComponent<GenericEnemyScript> ().Move ();
			}
			yield return new WaitForSeconds(0.1f);

			if (enemy != null) {
				enemy.GetComponent<GenericEnemyScript> ().Attack ();
			}

		}

		foreach (var enemySpawner in LevelControlScript.control.GetEnemySpawners()) {
			yield return new WaitForSeconds (0.01f);
			//Debug.Log ("Enemy Spawn Attempt");
			enemySpawner.SpawnEnemy ();
		}

		yield return new WaitForSeconds(0.1f);

		TurnControlScript.control.AllEnemiesHaveAttacked ();
	}

    private void BroadcastMove()
    {
        //BroadcastMessage("Move");
        
        foreach(var enemy in GameControlScript.control.GetInGameEnemyList())
        {
            enemy.GetComponent<GenericEnemyScript>().Move();
			enemy.GetComponent<GenericEnemyScript>().Attack();
        }

    }
}
