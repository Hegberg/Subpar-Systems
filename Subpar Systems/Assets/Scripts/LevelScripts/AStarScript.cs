using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarScript : MonoBehaviour {
	public static AStarScript control;
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

	void run(){
		LevelControlScript.control.GetAStarMap();
	}

	public List<GameObject> findShitestPath(List<List<GameObject>> map, int originRow, int originIndex, int goalRow, int goalIndex)
	{
		Debug.Log ("Testing Return Direction");
		Debug.Log(returnDirection (originIndex, originIndex, true, true));

		return null;
	}


	/*
	 *	NE = 0 
	 * 	NW = 1
	 *	SE = 2
	 *	SW = 3
	*/
	public int returnDirection(int originIndex, int neighbourIndex, bool isAbove, bool isOddRow)
	{
		if (!isOddRow) {
			//Check Even row
			if (!isAbove) {
				if (originIndex == neighbourIndex) {
					return 0; //NE	
				} else {
					return 2; //SE
				}
			} else {
				if (originIndex == neighbourIndex) {
					return 1; //NW	
				} else {
					return 3; //SW
				}			
			}
		} else {
			//Check Odd rows
			if (!isAbove) {
				if (originIndex == neighbourIndex) {
					return 2; //SE	
				} else {
					return 0; //NE
				}
			} else {
				if (originIndex == neighbourIndex) {
					return 3; //SW	
				} else {
					return 1; //NW
				}			
			}		
		}//end list

		//If it get here it is broken
		return -1;
	}

}
