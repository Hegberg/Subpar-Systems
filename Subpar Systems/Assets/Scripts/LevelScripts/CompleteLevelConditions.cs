using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelConditions : MonoBehaviour {

	public CompleteLevelConditions control;

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

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
