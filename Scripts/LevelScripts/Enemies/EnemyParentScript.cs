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

    void BroadcastAttack()
    {
        BroadcastMessage("Attack");
    }

    void BroadcastMove()
    {
        BroadcastMessage("Move");
    }
}
