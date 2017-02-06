using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTraitsScript : MonoBehaviour {

	public static GenericTraitsScript control;

	private float hpModifier = 1.0f; //not affected
	private float attackModifier = 1.0f; //not affected
	private float defenseModifier = 1.0f; //not affected
	private float movementModifier = 1.0f; //not affected
	private float rangeModifier = 1.0f; //not affected

	//arguments for list are hpModifier, attackModifier, defenseModifier, movementModifier, and rangedModifier
	private List<float> aggresion = new List<float>() {1.0f, 1.5f, 1.0f, 1.0f, 1.0f};
	private List<float> wimp = new List<float>() {1.0f, 0.5f, 0.5f, 1.0f, 1.0f};
	private List<float> malnourished = new List<float>() {1.0f, 1.0f, 0.5f, 1.0f, 1.0f};

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

		DontDestroyOnLoad (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<float> GetAggresion(){
		return aggresion;
	}

	public List<float> GetWimp(){
		return wimp;
	}

	public List<float> GetMalnourished(){
		return malnourished;
	}
}
