using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarScript : MonoBehaviour {
	public static AStarScript control;

	//Commented out to try dynamic maxRow/maxIndex
	//private int maxRow;
	//private int maxIndex;

	//========DEBUG LOOP STOPPER=======//
	public int endNumLoop;
	public int increment;

	// Use this for initialization
	void Start () {
		if(control == null)
		{
			control = this;

			//Debug loop variables
			endNumLoop = 25;
			increment = 0;
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

	/*
		WARNING NONE OF THE FOLLOWING CODE IS TEST AND WILL AND COULD BREAK EVERYTHING THAT EXIST!
	*/


	//Returns tile that are within the range of the map
	public List<List<int>> FloodFillAttackRange(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int attackrange)
	{
		//Debug.Log ("Starting attaackflood");
		var attackRangeRemain = new Dictionary<List<int>, int>();

		List<List<int>> openSet = new List<List<int>> ();
		List<List<int>> attackSet =  new List<List<int>>();

		//Init Starting and goal position
		List<int> startPosition = new List<int> ();
		startPosition.Add (originRow);
		startPosition.Add (originIndex);

		attackRangeRemain[startPosition] = attackrange;
		openSet.Add(startPosition);

		while(!(openSet.Count == 0))
		{
			
			List<int> currentNode = new List<int> ();
			List<int> index = new List<int> ();
			currentNode = openSet[0];
			//Debug.Log ("Current node in openset " + currentNode [0] + currentNode [1]);
			openSet.RemoveAt(0);
			int currentNodeRow = currentNode[0];
			int currentNodeIndex = currentNode[1];

			List<int> rows = new List<int> () { currentNodeRow - 1, currentNodeRow + 1 };
			if (currentNodeRow % 2 == 0) {
				index.Add (currentNodeIndex-1);
				index.Add (currentNodeIndex);
			} else {
				index.Add (currentNodeIndex);
				index.Add (currentNodeIndex+1);
			}
			//Debug.Log ("What is in the row " + rows [0] + "," + rows [1]);
			//Debug.Log ("What is in the index " + index [0] + "," + index [1]);

			for(int i = 0; i < 2; ++i) 
			{
				int gRow = rows [i];
				for (int j = 0; j < 2; ++j)
				{
					int gIndex = index [j];
					//Debug.Log ("Current Node we are Checking: " + gRow + "," + gIndex);
					if (gRow < 0 || gRow >= map.Count || gIndex < 0 || gIndex >= map[gIndex].Count) 
					{
						//Debug.Log ("I left in gRow and gIndex");
						continue;
					}

					//A modified version of CanGetNext that ignores some of the restrictions
					if (CanGetNextAttackRange (currentNodeRow, currentNodeIndex, gRow, gIndex, map.Count, map[gIndex].Count)) 
					{
						//Determine new cost given attack range, needs to be changed to add in height
						int newCost = attackRangeRemain[currentNode] - ReturnCostTile(gRow, gIndex, mapCost);
						//Debug.Log ("Current node cost " + currentNodeRow + "," + currentNodeIndex + " " + movementRemain [currentNode] + " COST " + newCost);
						if (newCost >= 0) {
							List<int> neighborNode = new List<int> ();
							neighborNode.Add (gRow);
							neighborNode.Add (gIndex);
							openSet.Add (neighborNode);
							attackSet.Add (neighborNode);
							attackRangeRemain [neighborNode] = newCost;
						} else {
							continue;
						}
					}
				}//end for loop

			}//end for loop

		}//end while loop

		for (int i = 0; i < attackSet.Count; ++i) {
			//Debug.Log ("attackSet Results at " + i + " " + attackSet[i][0] + "," + attackSet[i][1]);
		}


		//Return tiles that are within attack range
		return attackSet;
	} 

	//Returns a list of all valid walkable tiles within range
	public List<List<int>> FloodFillWithinRange(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int movementRange)
	{
		Debug.Log ("Starting Flood");
		var movementRemain = new Dictionary<List<int>, int>();

		List<List<int>> openSet = new List<List<int>> ();
		List<List<int>> cameFromSet =  new List<List<int>>();

		//Init Starting and goal position
		List<int> startPosition = new List<int> ();
		startPosition.Add (originRow);
		startPosition.Add (originIndex);

		movementRemain[startPosition] = movementRange;
		openSet.Add(startPosition);

		while(!(openSet.Count == 0))
		{
		//	Debug.Log ("In while loop");
			//Clear the current node

			List<int> currentNode = new List<int> ();
			List<int> index = new List<int> ();
			currentNode = openSet[0];
			//Debug.Log ("Current node in openset " + currentNode [0] + currentNode [1]);
			openSet.RemoveAt(0);
			int currentNodeRow = currentNode[0];
			int currentNodeIndex = currentNode[1];

			List<int> rows = new List<int> () { currentNodeRow - 1, currentNodeRow + 1 };
			if (currentNodeRow % 2 == 0) {
				index.Add (currentNodeIndex-1);
				index.Add (currentNodeIndex);
			} else {
				index.Add (currentNodeIndex);
				index.Add (currentNodeIndex+1);
			}
			//Debug.Log ("What is in the row " + rows [0] + "," + rows [1]);
			//Debug.Log ("What is in the index " + index [0] + "," + index [1]);

			for(int i = 0; i < 2; ++i) 
			{
				int gRow = rows [i];
				for (int j = 0; j < 2; ++j)
				{
					int gIndex = index [j];
					//Debug.Log ("Current Node we are Checking: " + gRow + "," + gIndex);
					if (gRow < 0 || gRow >= map.Count || gIndex < 0 || gIndex >= map[gIndex].Count) 
					{
						//Debug.Log ("I left in gRow and gIndex");
						continue;
					}
					//Debug.Log ("Here is row and index " + gRow + "," + gIndex);
					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex, map.Count, map[gIndex].Count, map[gRow][gIndex], mapCost[gRow][gIndex][0])) 
					{
						//Add the new neighbor

						int newCost = movementRemain[currentNode] - ReturnCostTile(gRow, gIndex, mapCost);
						//Debug.Log ("Current node cost " + currentNodeRow + "," + currentNodeIndex + " movement " + movementRemain [currentNode] + " COST " + newCost);
						if (newCost >= 0) {
							List<int> neighborNode = new List<int> ();
							neighborNode.Add (gRow);
							neighborNode.Add (gIndex);
							//Debug.Log ("Added the neighborNode " + neighborNode [0] + "," + neighborNode [1]);
							openSet.Add (neighborNode);
							cameFromSet.Add (neighborNode);
							movementRemain [neighborNode] = newCost;
						} else {
							continue;
						}
					}
				}//end for loop

			}//end for loop
				
		}//end while loop
		/*Debug.Log("END THE LOOPS");
		for (int i = 0; i < cameFromSet.Count; ++i) {
			Debug.Log ("cameFromSet Results at " + i + " " + cameFromSet[i][0] + "," + cameFromSet[i][1]);
		}
		*/
		Debug.Log ("End floodfill");
		//We should never get HERE. LIKE EVER
		return cameFromSet;
	}

	//Parse through the cameFromList and returns a single path
	//We start from the end goal and go backwards
	//Remeber that the dictionary has the "destination" as key and "came from" as value
	private List<List<int>> ReconstructPath(Dictionary <List<int>, List<int>> cameFromDic, List<int> currentNode)
	{
		List<int> current = currentNode;
		List<List<int>> finalPath = new List<List<int>> ();

		//Add the end node
		//Debug.Log("The goal node is: " + currentNode[0] + " " +  currentNode[1]);
		finalPath.Add (current);

		for (int i = 0; i < cameFromDic.Count; ++i) 
		{
			if (cameFromDic.ContainsKey (current)) 
			{
				//Add the current to final path
				finalPath.Add (cameFromDic[current]);
			
				//Debug.Log ("Added to final path: " + cameFromDic[current][0] + "," + cameFromDic[current][1]);

				//Set the next current as the "came from"
				current = cameFromDic[current];
			}
		}//end for loop

		//============TESTED TO THIS POINT 12.0 WORKS=====================//
		//Debug.Log("Tested at 12.0");
		//for (int i = 0; i < finalPath.Count; ++i) {
		//	Debug.Log ("FinalPath Results at " + i + " " + finalPath[i][0] + "," + finalPath[i][1]);
		//}

		//Return back the proper order
		finalPath.Reverse();
	

		return finalPath;

	}//end ReconstructPath

	//Calculate an arbitary HeuristicCost that is basically Eucliden distance
	private int CalculateHeuristicCost(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		//There are temp values/equation for cost matrix
		//This would get replaced later by tile movement cost or something
		//Might just use the highest tile movement cost??
		int diagonalCost = 4;
		int cardinalCost = 2;

		//Calculate an arbitrary cost value
		int min = Mathf.Min (Mathf.Abs (originRow - goalRow), Mathf.Abs (originIndex - goalIndex));
		int max = Mathf.Max (Mathf.Abs (originRow - goalRow), Mathf.Abs (originIndex - goalIndex));

		//Current return cost is arbitary
		return (int)((min*diagonalCost) + ((max-min)*cardinalCost));
	}

	//Check can I move from this tile to the next
	private bool CanGetNextAttackRange(int originRow, int originIndex, int goalRow, int goalIndex, int mapRowCount, int mapIndexCount)
	{
		//Check all the various things to ensure it can get to next location
		if (!CheckBound(goalRow, goalIndex, mapRowCount, mapIndexCount) 
			|| !CheckOneTileAway(originRow,originIndex,goalRow,goalIndex) 
			|| CheckIsSelf(originRow,originIndex,goalRow,goalIndex)) 
		{
			return false;
		}
		//Debug.Log("true cangetnext");

		//Code to check if it is matching earthtile
		//Code might go here for "terrian cost stuff" 
		return true;
	}

	//Check can I move from this tile to the next
	private bool CanGetNext(int originRow, int originIndex, int goalRow, int goalIndex, int mapRowCount, int mapIndexCount, GameObject mapTile, int mapType)
	{
        //Check all the various things to ensure it can get to next location
		if (!CheckBound(goalRow, goalIndex, mapRowCount, mapIndexCount) 
			|| !CheckOneTileAway(originRow,originIndex,goalRow,goalIndex) 
			|| CheckIsSelf(originRow,originIndex,goalRow,goalIndex)
            || !CheckIfWalkable(mapTile, mapType)) {
			return false;
		}
        //Debug.Log("true cangetnext");

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
		if (originRow % 2 == 0) {
			if (((originIndex - goalIndex) == 0 || (originIndex - goalIndex) == 1) && Mathf.Abs (originRow - goalRow) == 1) {
				return true;
			}
		} else {
			if (((originIndex - goalIndex) == 0 || (originIndex - goalIndex) == -1) && Mathf.Abs (originRow - goalRow) == 1) {
				return true;
			}
		}
		return false;
	}//end checkOneTileAway

	//Check to see if the next tile is within the gamebound
	private bool CheckBound(int goalRow, int goalIndex, int maxRow, int maxIndex)
	{
        if (goalRow >= maxRow || goalIndex >= maxIndex) 
		{
            return false;
		}

		if ((goalRow >= 0 && goalRow < maxRow) && (goalIndex >= 0 && goalIndex < maxIndex)) {
            return true;
		}
        return false;
	}//end checkBound

	private bool CheckIfWalkable(GameObject mapTile, int mapType)
    {
        //0 -> earth, 1->water, 2->mountian
        //right now 0 means earth, and earth only walkable tile
		if (mapType == 0 && 
            //check if empty tile as well, nneds to be done after first if or else will check tiles without GenericEarthScript code and exception will be raised
            (mapTile.GetComponent<GenericEarthScript>().GetOccupingObject() == null))
        {

            return true;
        }

        //otherwise
        return false;
    }

	//Return the cost of traveling 
	private int ReturnCostTile(int tileRow, int tileIndex, List<List<List<int>>> mapCost)
	{
		//Hard coded the +1 because base tile movement cost is 0
		return mapCost [tileRow][tileIndex][1] + 1;
	}

	/*
	 *	NE = 0 
	 * 	NW = 1
	 *	SE = 2
	 *	SW = 3
	*/
	//Return the direction the unit is takes when moving from current location to next tile
	//NOT THIS CODE MIGHT BE COMPLETELY WORTHLESS SO I MIGHT GET RID OF IT LATER
	private int ReturnDirection( int originRow, int originIndex, int neighbourRow, int neighbourIndex)
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
