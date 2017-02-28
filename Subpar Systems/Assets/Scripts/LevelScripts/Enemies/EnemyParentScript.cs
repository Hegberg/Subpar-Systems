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
			TurnControlScript.control.LevelPassed ();
		}
	}

	public void StartAITurn(){
		StartCoroutine (AIEnnumerate());
		//BroadcastAttack ();
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
		//yield return new WaitForSeconds(0.01f);
		foreach(var enemy in GameControlScript.control.GetInGameEnemyList())
		{
			yield return new WaitForSeconds(0.01f);
			enemy.GetComponent<GenericEnemyScript>().Move();
			yield return new WaitForSeconds(0.01f);
			enemy.GetComponent<GenericEnemyScript>().Attack();
		}
		yield return new WaitForSeconds(0.01f);
		TurnControlScript.control.StartTurn ();
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
