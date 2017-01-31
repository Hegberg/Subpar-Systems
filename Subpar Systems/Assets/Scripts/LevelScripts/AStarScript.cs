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
		Debug.Log ("Testing Return Direction");
		Debug.Log (originRow + " " + originIndex);
		//Debug.Log (originIndex);
		Debug.Log (goalRow + " " +  goalIndex);
		//Debug.Log (goalIndex);
		Debug.Log ("The end of the Stats");
		Debug.Log(ReturnDirection (originRow, originIndex, goalRow, goalIndex));


		//Initialize variables for gScore, fScore, starting position, goal position
		//For dictionary the List => the row/index number of the tile
		var gScore = new Dictionary<List<int>, int>();
		var fScore = new Dictionary<List<int>, int>();

		maxRow = map.Count;
		maxIndex = map [0].Count;

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

		//Init all values of gScore/fScore to infinity
		// i = Row
		// j = Index
		List<int> tempTilePosition = new List<int> ();
		for (int i = 0; i < map.Count; ++i)
		{
			for (int j = 0; i < map[i].Count; ++j) 
			{
				//Clear the old values
				tempTilePosition.Clear ();
				//Add the new row and index
				tempTilePosition.Add (i);
				tempTilePosition.Add (j);

				gScore [tempTilePosition] = int.MaxValue;
				fScore [tempTilePosition] = int.MaxValue;
			}//end inner for loop
		}//end for loop

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
		List<int> neighborNode = new List<int> ();
		int lowestFScore;
		while(!(openSet.Count == 0))
		{
			//Clear the current node
			currentNode.Clear();

			//Find the lowest fScore in the opensEt
			lowestFScore = int.MaxValue;
			for (int i = 0; i < openSet.Count; ++i) 
			{
				if(fScore[openSet[i]] < lowestFScore)
				{
					lowestFScore = fScore[openSet[i]];
					currentNode = openSet[i];
				}
			}//end for loop

			//Found the goal
			if (currentNode.Equals (goalPosition)) 
			{
				//Return the path back
				return ReconstructPath (cameFromSet, currentNode);
			}

			//Pop the currentNode from openSet
			openSet.Remove(currentNode);
			//INSERT SOMETHING ABOUT GENERATION COUNTER

			Debug.Log ("Current Node: " + currentNode[0] + currentNode[1]);

			int currentNodeRow = currentNode[0];
			int currentNodeIndex = currentNode[1];

			//Iterate through all the surrounding nodes
			for (int gRow = currentNodeRow - 1; gRow < currentNodeRow + 2; ++gRow) 
			{
				for (int gIndex = currentNodeIndex - 1; gIndex < currentNodeIndex + 2; ++gIndex) 
				{
					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex)) 
					{
						//Add the new neighbor
						neighborNode.Clear ();
						neighborNode.Add (gRow);
						neighborNode.Add (gIndex);

						//INSERT SOMETHING ABOUT GENERATION COUNTER

						int tentativeGScore = gScore [currentNode] + ReturnCostTile(ReturnDirection(currentNodeRow, currentNodeIndex, goalRow, gIndex));

						//Add the neighbor to the openset if it not there
						//Also check to see if the tentativeGScore is less than the one current stored for neighbor
						if (!openSet.Contains(neighborNode)) {
							openSet.Add (neighborNode);
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
		//There are temp values for cost matrix
		//This would get replaced later by a more realistic one 

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
		if (goalRow < 0 || goalRow > maxRow) {
			return false;
		}
		if (goalIndex < 0 || goalIndex > maxIndex) {
			return false;
		}
		return true;
	}//end checkBound

	//Return the cost of traveling 
	private int ReturnCostTile(int tileCost)
	{
		//INSERT CODE FOR REAL COST
		//Most likely going to be terrain cost 
		return tileCost;
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
