using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarScript : MonoBehaviour {
	public static AStarScript control;
	private int maxRow;
	private int maxIndex;
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
	public List<List<int>> findShitestPath(List<List<GameObject>> map, int originRow, int originIndex, int goalRow, int goalIndex)
	{
		Debug.Log ("Original Row and Index: " + originRow + " " + originIndex);
		//Debug.Log (originIndex);
		Debug.Log ("Goal Row and Index: " + goalRow + " " +  goalIndex);
		//Debug.Log (goalIndex);
		Debug.Log("Return direction: " + ReturnDirection (originRow, originIndex, goalRow, goalIndex));

		//============TESTED TO THIS POINT 1.0 WORKS=====================//
		//Debug.Log("Tested at 1.0");
		//return null;

		//Initialize variables for gScore, fScore, starting position, goal position
		//For dictionary the List => the row/index number of the tile
		var gScore = new Dictionary<List<int>, int>();
		var fScore = new Dictionary<List<int>, int>();

		//Currently the maxIndex return 0...
		maxRow = map.Count;
		maxIndex = map[0].Count;

		//Debug.Log ("The MaxRow = " + map.FindIndex() + " The maxIndex = " + map[0].FindIndex());
		Debug.Log ("The MaxRow = " + map.Count + " The maxIndex = " + map[1].Count);

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
		List<int> currentNode = new List<int> ();
		int lowestFScore;

		//============TESTED TO THIS POINT 4.0 WORK=====================//
		//Debug.Log("Tested at 4.0");
		//Debug.Log ("fScore for starting position is " + fScore [startPosition]);
		//return null;


		while(!(openSet.Count == 0))
		{
			//Clear the current node
			currentNode.Clear();

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
				Debug.Log("======Got inside reconstruct path======");
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

					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex)) 
					{
						//Add the new neighbor
						List<int> neighborNode = new List<int> ();
						neighborNode.Add (gRow);
						neighborNode.Add (gIndex);

						//INSERT SOMETHING ABOUT GENERATION COUNTER

						int tentativeGScore = gScore[currentNode] + ReturnCostTile(ReturnDirection(currentNodeRow, currentNodeIndex, goalRow, gIndex));

						//============TESTED TO THIS POINT 8.0 WORKS=====================//
						Debug.Log("Tested at 8.0");
						Debug.Log ("NeighborNode Row and Index: " + neighborNode[0] + "," + neighborNode[1]);
						Debug.Log ("TentativeGScore: " + tentativeGScore);
						return null;

						//Add the neighbor to the openset if it not there
						//Also check to see if the tentativeGScore is less than the one current stored for neighbor
						if (!openSet.Contains(neighborNode)) {
							//So I am adding the correct neighborNodes
							Debug.Log ("I have added: " + neighborNode [0] + "," + neighborNode [1]);
							openSet.Add (neighborNode);
							Debug.Log ("Current Count of openSet: " + openSet.Count);
						} 
						else if (tentativeGScore >= gScore [neighborNode]) 
						{
							continue;
						}
						//Create a dic that contains the  Row/Index of current (where we came from)  VALUE
						//								  Row/Index of neighbor (Where we are going) KEY

						cameFromSet[neighborNode] = currentNode;

						//Change the related gScore and fScore
						gScore [neighborNode] = tentativeGScore;
						fScore [neighborNode] = gScore [neighborNode] + CalculateHeuristicCost (gRow, gIndex, goalRow, goalIndex);


					}
				}//end for loop
			
			}//end for loop

			//Currently can't get here.
			Debug.Log("We made it through one iteration!");
			return null;

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

		for (int i = 0; i < cameFromDic.Count; ++i) 
		{
			if (cameFromDic.ContainsKey (current)) 
			{
				//Add the current to final path
				finalPath.Add (current);
			
				Debug.Log ("Added to final path: " + current [0] + "," + current [1]);

				//Set the next current as the "came from"
				current = cameFromDic [current];
			}
		}//end for loop

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
	private bool CanGetNext(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		//Check all the various things to ensure it can get to next location
		if (!CheckBound(goalRow,goalIndex) 
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
	private bool CheckBound(int goalRow, int goalIndex)
	{
		
		if ((goalRow >= 0 && goalRow < maxRow) && (goalIndex >= 0 && goalIndex < maxIndex)) {
			return true;
		}

		return false;
	}//end checkBound

	//Return the cost of traveling 
	private int ReturnCostTile(int tileCost)
	{
		//INSERT CODE FOR REAL COST
		//Most likely going to be terrain cost 
		return 1;
	}

	/*
	 *	NE = 0 
	 * 	NW = 1
	 *	SE = 2
	 *	SW = 3
	*/
	//Return the direction the unit is takes when moving from current location to next tile
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
