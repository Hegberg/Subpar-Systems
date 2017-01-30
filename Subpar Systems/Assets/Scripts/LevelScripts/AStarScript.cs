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
		Debug.Log (originRow + " " + originIndex);
		//Debug.Log (originIndex);
		Debug.Log (goalRow + " " +  goalIndex);
		//Debug.Log (goalIndex);
		Debug.Log ("The end of the Stats");
		Debug.Log(ReturnDirection (originIndex, goalIndex, originRow, goalRow));

		return null;
	}

	//Calculate an arbitary HeuristicCost that is basically Eucliden distance
	private int CalculateHeuristicCost(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		//There are temp values for cost matrix
		int diagonalCost = 4;
		int cardinalCost = 2;

		//Calculate an arbitrary cost value
		float min = Mathf.Min (Mathf.Abs (originRow - goalRow), Mathf.Abs (originIndex - goalIndex));
		float max = Mathf.Max (Mathf.Abs (originRow - goalRow), Mathf.Abs (originIndex - goalIndex));

		//Current return cost is arbitary
		return ((min*diagonalCost) + ((max-min)*cardinalCost));
	}

	//Check can I move from this tile to the next
	private bool CanGetNext(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		//Check generic bounds
		int maxRow = 0;
		int maxIndex = 0;
		if (!CheckBound(goalRow,goalIndex, maxRow, maxIndex) 
			|| !CheckOneTileAway(originRow,originIndex,goalRow,goalIndex) 
			|| CheckIsSelf(originRow,originIndex,goalRow,goalIndex)) {
			return false;
		}

		//Code to check if it is matching earthtile
		//Code might go here for "terrian cost stuff" 
		return true;
	}

	//Check to see the tile is itself
	private bool CheckIsSelf(int originRow, int originIndex, int goalRow, int goalIndex){
		if (originRow == goalRow && originIndex == goalIndex) {
			return true;
		}
		return false;
	}//end CheckIsSelf

	//Check to see if the tile is 1 tile away
	private bool CheckOneTileAway(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		if (Mathf.Abs (originRow - goalRow) > 1 || Mathf.Abs (originIndex - goalIndex) > 1) {
			return false;
		}
		return true;
	}//end checkOneTileAway

	//Check to see if the next tile is within the gamebound
	private bool CheckBound(int goalRow, int goalIndex, int maxRow, int maxIndex)
	{
		if (goalRow < 0 || goalRow > maxRow) {
			return false;
		}
		if (goalIndex < 0 || goalIndex > maxIndex) {
			return false;
		}
		return true;
	}//end checkBound

	/*
	 *	NE = 0 
	 * 	NW = 1
	 *	SE = 2
	 *	SW = 3
	*/
	//Return the direction the unit is takes when moving from current location to next tile
	private int ReturnDirection(int originIndex, int neighbourIndex, int originRow, int neighbourRow)
	{
		
		if (originRow%2 == 0) {
			//Check Even row
			if (neighbourRow > originRow) {
				if (originIndex == neighbourIndex) {
					return 3; //SW	
				} else {
					return 2; //SE
				}
			} else {
				if (originIndex == neighbourIndex) {
					return 1; //NW	
				} else {
					return 0; //NE
				}			
			}
		} else {
			//Check Odd rows
			if (neighbourRow > originRow) {
				if (originIndex == neighbourIndex) {
					return 2; //SE	
				} else {
					return 3; //SW
				}
			} else {
				if (originIndex == neighbourIndex) {
					return 0; //NE
				} else {
					return 1; //NW
				}			
			}		
		}//end list

		//If it get here it is broken
		return -1;
	}//end returnDirection

}
