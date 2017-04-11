using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevelConditions : MonoBehaviour {

	public static CompleteLevelConditions control;

	// Use this for initialization
	void Start () {
		if (control == null)
		{
			control = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LevelFailed() {
		GameControlScript.control.FailedLevel ();
	}

	public void AllEnimiesDead () {
		GameControlScript.control.NextLevel ();
	}

	public void CheckIfSurvivedEnoughTurns(int turns) {
		if (turns > 0) {
			if (TurnControlScript.control.GetCurrentTurn () >= turns) {
				GameControlScript.control.NextLevel ();
			}
		}
	}

	public void TankDied(){
		GameControlScript.control.FailedLevel ();
	}

	public void ShowObjective() {
		GameObject gameObjective = GameObject.Find ("GameObjective");
		gameObjective.GetComponent<Text> ().text = GameControlScript.control.GetMissionObjective ();
	}
}
