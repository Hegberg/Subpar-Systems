using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void CheckIfTankIsDead(){

	}
		
}
