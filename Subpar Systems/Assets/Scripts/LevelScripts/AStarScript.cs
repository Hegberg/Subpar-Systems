using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarScript : MonoBehaviour {
	public static AStarScript control;

	//Commented out to try dynamic maxRow/maxIndex
	//private int maxRow;
	//private int maxIndex;

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

	/*
		WARNING NONE OF THE FOLLOWING CODE IS TEST AND WILL AND COULD BREAK EVERYTHING THAT EXIST!
	*/
	public List<List<int>> findShitestPath(List<List<GameObject>> map, List<List<int>> mapCost, int originRow, int originIndex, int goalRow, int goalIndex)
	{
		//Debug.Log ("Original Row and Index: " + originRow + " " + originIndex);
		//Debug.Log (originIndex);
		//Debug.Log ("Goal Row and Index: " + goalRow + " " +  goalIndex);
		//Debug.Log (goalIndex);
		//Debug.Log("Return direction: " + ReturnDirection (originRow, originIndex, goalRow, goalIndex));

		//============TESTED TO THIS POINT 1.0 WORKS=====================//
		//Debug.Log("Tested at 1.0");
		//return null;

		//Initialize variables for gScore, fScore, starting position, goal position
		//For dictionary the List => the row/index number of the tile
		var gScore = new Dictionary<List<int>, int>();
		var fScore = new Dictionary<List<int>, int>();

		//These use to be hardcoded, but I am experiementing with dynamic
		//maxRow = map.Count;
		//maxIndex = map[0].Count;

		//Debug.Log ("The MaxRow = " + map.FindIndex() + " The maxIndex = " + map[0].FindIndex());
		//Debug.Log ("The MaxRow = " + map.Count + " The maxIndex = " + map[1].Count);

		List<List<int>> openSet = new List<List<int>> ();
		Dictionary<List<int>, List<int>> cameFromSet =  new Dictionary<List<int>, List<int>>();

		//Init Starting and goal position
		List<int> startPosition = new List<int> ();
		startPosition.Add (originRow);
		startPosition.Add (originIndex);
		List<int> goalPosition = new List<int> ();
		goalPosition.Add (goalRow);
		goalPosition.Add (goalIndex);

		//INSERT SOMETHING ABOUT GENERATION COUNTER



		//============TESTED TO THIS POINT 2.0 WORKS=====================//
		//Debug.Log("Tested at 2.0");
		//Debug.Log ("map.Count: " + map.Count);
		//Debug.Log ("map[0].Count: " + map [0].Count);
		//return null;


		//Init all values of gScore/fScore to infinity
		// i = Row
		// j = Index
		for (int i = 0; i < map.Count; ++i)
		{
			for (int j = 0; j < map[i].Count; ++j) 
			{
				List<int> tempTilePosition = new List<int> ();
				//Add the new row and index
				tempTilePosition.Add(i);
				tempTilePosition.Add(j);

				gScore [tempTilePosition] = int.MaxValue;
				fScore [tempTilePosition] = int.MaxValue;

				//Debug.Log ("gScore, fscore at i,j = " + i + "," + j + "," + gScore [tempTilePosition] + "," + fScore[tempTilePosition]);
			}//end inner for loop

		}//end for loop

		//============TESTED TO THIS POINT 3.0 WORK=====================//
		//Debug.Log("Tested at 3.0");
		//return null;

		//Init start node
		gScore[startPosition] = 0;
		fScore [startPosition] = CalculateHeuristicCost (originRow, originIndex, goalRow, goalIndex);

		//Insert the starting node 
		openSet.Add(startPosition);


		/*
		 * These debug prove that the start node is in openSet
		 * 
		List<int> debugOpenSet = new List<int> ();
		debugOpenSet = openSet [0];
		Debug.Log ("GOT HERE " + debugOpenSet [0] + debugOpenSet [1]);
		Debug.Log ("OpenSet containing values: " + openSet.ToString());

		for (int i = 0; i < openSet.Count; ++i) {
			for (int j = 0; j < openSet[i].Count; ++j) {
				Debug.Log ("element inside: " + openSet[i][j]);
			}
		}
		Debug.Log ("Got here");
		*/

		//Visit the nodes in openSet
		int lowestFScore;

		//============TESTED TO THIS POINT 4.0 WORK=====================//
		//Debug.Log("Tested at 4.0");
		//Debug.Log ("fScore for starting position is " + fScore [startPosition]);
		//return null;


		while(!(openSet.Count == 0))
		{
			//Clear the current node
			List<int> currentNode = new List<int> ();

			//Find the lowest fScore in the opensEt
			lowestFScore = int.MaxValue;

			//Debug.Log ("=====Start fScore Debug======");
			for (int i = 0; i < openSet.Count; ++i) 
			{
				//Debug to check inside the element of openSet
				/*
				for (int k = 0; k < openSet.Count; ++k) {
					for (int j = 0; j < openSet[k].Count; ++j) {
						Debug.Log ("element inside: " + openSet[k][j]);
					}
				}
				*/
				if(fScore[openSet[i]] < lowestFScore)
				{
					lowestFScore = fScore[openSet[i]];
					currentNode = openSet[i];
					//Debug.Log ("What got selected as current node: " + currentNode [0] + "," + currentNode [1]);
				}
			}//end for loop fScore

			//============TESTED TO THIS POINT 5.0 WORK=====================//
			//Debug.Log("Tested at 5.0");
			//Debug.Log ("Past fScore section of AStar " + fScore [startPosition]);
			//Debug.Log ("=====End fScore Debug======");
			//return null;


			//Found the goal
			if (currentNode[0] == goalPosition[0] && currentNode[1] == goalPosition[1]) 
			{
				//Return the path back
				//Debug.Log("======Got inside reconstruct path======");
				//Debug.Log("The gScore of the tile " + currentNode[0] + "," + currentNode[1] + " is: " + gScore[currentNode]);
				return ReconstructPath (cameFromSet, currentNode);
			}

			//Pop the currentNode from openSet
			openSet.Remove(currentNode);

			//INSERT SOMETHING ABOUT GENERATION COUNTER

			int currentNodeRow = currentNode[0];
			int currentNodeIndex = currentNode[1];

			//============TESTED TO THIS POINT 6.0 WORKS=====================//
			//Debug.Log("Tested at 6.0");
			//Debug.Log ("Current Node in OpenSet: " + currentNode[0] + ","+ currentNode[1]);
			//Debug.Log ("Does the openset contain currentNode anymore. Should be False: " + openSet.Contains (currentNode));
			//return null;


			//Iterate through all the surrounding nodes
			for (int gRow = currentNodeRow - 1; gRow < currentNodeRow + 2; ++gRow) 
			{
				for (int gIndex = currentNodeIndex - 1; gIndex < currentNodeIndex + 2; ++gIndex) 
				{
					//============TESTED TO THIS POINT 7.0 WORKS=====================//
					//Debug.Log("Tested at 7.0");
					//Debug.Log ("Current node values: " + currentNodeRow + "," + currentNodeIndex);
					//Debug.Log ("Current node Row min and max: " + (currentNodeRow - 1) + ","+ (currentNodeRow + 1));
					//Debug.Log ("Current node Index min and max: " + (currentNodeIndex - 1) + ","+ (currentNodeIndex + 1));
					//return null;


					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex, map)) 
					{
						//Add the new neighbor
						List<int> neighborNode = new List<int> ();
						neighborNode.Add (gRow);
						neighborNode.Add (gIndex);

						//INSERT SOMETHING ABOUT GENERATION COUNTER

						//This commented out code is old, just have it incase I screw up later on
						//int tentativeGScore = gScore[currentNode] + ReturnCostTile(ReturnDirection(currentNodeRow, currentNodeIndex, goalRow, gIndex));
						int tentativeGScore = gScore[currentNode] + ReturnCostTile(currentNodeRow, currentNodeIndex, mapCost);

						//=====================DUMMY CODE to HANDLE MOVEMENT LIMIT=====================//
						/*
						if (tentativeGScore > movementLimit)
						{
							break;
						}
						*/
						//======================DUMMY CODE to HANDLE MOVEMENT LIMIT====================//


						//============TESTED TO THIS POINT 8.0 WORKS=====================//
						//Debug.Log("Tested at 8.0");
						//Debug.Log ("NeighborNode Row and Index: " + neighborNode[0] + "," + neighborNode[1]);
						//Debug.Log ("TentativeGScore: " + tentativeGScore);
						//return null;


						//Add the neighbor to the openset if it not there
						//Also check to see if the tentativeGScore is less than the one current stored for neighbor
						if (!openSet.Contains(neighborNode)) {
							
							openSet.Add (neighborNode);

							//============TESTED TO THIS POINT 9.0A WORKS=====================//
							//Debug.Log("Tested at 9.0A");
							//Debug.Log ("neighborNode that got added: " + neighborNode [0] + "," + neighborNode [1]);
							//Debug.Log ("Check to see if openset has the neighborNode. Should be True: " + openSet.Contains(neighborNode));
							//return null;

						} 
						else if (tentativeGScore >= gScore [neighborNode]) 
						{
							//============TESTED TO THIS POINT 9.0B UNTESTED YET=====================//
							//Debug.Log("Tested at 9.0B");
							//Debug.Log ("neighborNode that got skipped: " + neighborNode [0] + "," + neighborNode [1]);
							//Debug.Log ("Check to see if openset has the neighborNode. Should be True: " + openSet.Contains(neighborNode));
							//return null;

							continue;
						}
						//Create a dic that contains the  Row/Index of current (where we came from)  VALUE
						//								  Row/Index of neighbor (Where we are going) KEY
						cameFromSet[neighborNode] = currentNode;

						//Change the related gScore and fScore
						gScore [neighborNode] = tentativeGScore;
						fScore [neighborNode] = gScore [neighborNode] + CalculateHeuristicCost (gRow, gIndex, goalRow, goalIndex);

						//============TESTED TO THIS POINT 10.0 WORKS=====================//
						//Debug.Log("Tested at 10.0");
						//Debug.Log ("cameFromSet[neighborNode]: " + cameFromSet[neighborNode][0] + "," + cameFromSet[neighborNode][1] + "= " + currentNode[0] + "," + currentNode[1]);
						//Debug.Log ("gScore[neighborNode]: " + gScore[neighborNode] + "=" + tentativeGScore);
						//Debug.Log ("fScore[neighborNode]: " + fScore[neighborNode] + "=" + (gScore [neighborNode] + CalculateHeuristicCost (gRow, gIndex, goalRow, goalIndex)) );
						//return null;
					}
				}//end for loop
			
			}//end for loop

			//Currently can't get here.
			//============TESTED TO THIS POINT 11.0 WORKS=====================//
			//Debug.Log("Tested at 11.0");
			//Debug.Log ("Size of cameFromSet: " + cameFromSet.Count);

			//foreach(var blah in cameFromSet){
			//	Debug.Log ("cameFromSet " + blah.Key[0] + "," + blah.Key[1] + " : "+ blah.Value[0] + "," + blah.Value[1]);
			//}

			//Debug.Log ("Size of openSet: " + openSet.Count);
			//for (int i = 0; i < opgoenSet.Count; ++i) 
			//{
			//	Debug.Log ("openSet at " + i + " : " + openSet[i][0] + "," + openSet[i][1]);
			//}

			//return null;

		}//end while loop

		//We should never get HERE. LIKE EVER
		return null;
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
	private bool CanGetNext(int originRow, int originIndex, int goalRow, int goalIndex, List<List<GameObject>> map)
	{
		//Check all the various things to ensure it can get to next location
		if (!CheckBound(goalRow, goalIndex, map.Count, map[goalIndex].Count) 
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

	//Return the cost of traveling 
	private int ReturnCostTile(int tileRow, int tileIndex, List<List<int>> mapCost)
	{
		//Hard coded the +1 because base tile movement cost is 0
		return mapCost [tileRow][tileIndex] + 1;
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
