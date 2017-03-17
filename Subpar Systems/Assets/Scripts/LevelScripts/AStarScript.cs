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

	public List<List<int>> findShitestPath(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int goalRow, int goalIndex)
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

		//TESTING NEW OPENSET
		//Dictionary<string, List<int>> openSetNew = new Dictionary<string, List<int>>();

		Dictionary<List<int>, List<int>> cameFromSet =  new Dictionary<List<int>, List<int>>();

		//Init Starting and goal position
		List<int> startPosition = new List<int> ();
		startPosition.Add (originRow);
		startPosition.Add (originIndex);
		List<int> goalPosition = new List<int> ();
		goalPosition.Add (goalRow);
		goalPosition.Add (goalIndex);

		//INSERT SOMETHING ABOUT GENERATION COUNTER
		bool isEnemy = true;
		//Check to see if this is enemy
		if (map[originRow][originIndex].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy() == true)
		{
			isEnemy = false;
		}	
		//CODE FOR PASSING THROUGH UNIT



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

		//===================TESTING NEW OPENSET====================//
		//openSetNew[startPosition[0].ToString() + "-" + startPosition[1].ToString()] = startPosition;
		//===================TESTING NEW OPENSET====================//


		//These debug prove that the start node is in openSet
		/*
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


			//===================TESTING NEW OPENSET====================//
			/*
			foreach (var element in openSetNew) 
			{
				if (fScore [element.Value] < lowestFScore) 
				{
					lowestFScore = fScore [element.Value];
					currentNode = element.Value;
				}
			}
			*/
			//===================TESTING NEW OPENSET====================//


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
				//Debug.Log(ReconstructPath(cameFromSet, currentNode));
				return ReconstructPath (cameFromSet, currentNode);
			}

			//Pop the currentNode from openSet
			openSet.Remove(currentNode);

			//===================TESTING NEW OPENSET====================//
			//openSetNew.Remove(currentNode[0].ToString() + "-" + currentNode[1].ToString());
			//===================TESTING NEW OPENSET====================//

			//INSERT SOMETHING ABOUT GENERATION COUNTER

			int currentNodeRow = currentNode[0];
			int currentNodeIndex = currentNode[1];

			//Calculate Index and Row to check for neighbour
			List<int> index = new List<int> ();

			List<int> rows = new List<int> () { currentNodeRow - 1, currentNodeRow + 1 };
			if (currentNodeRow % 2 == 0) {
				index.Add (currentNodeIndex);
				index.Add (currentNodeIndex+1);
			} else {
				index.Add (currentNodeIndex-1);
				index.Add (currentNodeIndex);
			}

			//============TESTED TO THIS POINT 6.0 WORKS=====================//
			//Debug.Log("Tested at 6.0");
			//Debug.Log ("Current Node in OpenSet: " + currentNode[0] + ","+ currentNode[1]);
			//Debug.Log ("Does the openset contain currentNode anymore. Should be False: " + openSet.Contains (currentNode));
			//return null;


			//Iterate through all the surrounding nodes
			for(int i = 0; i < 2; ++i) 
			{
				int gRow = rows [i];
				for (int j = 0; j < 2; ++j)
				{
					int gIndex = index [j];

					//============TESTED TO THIS POINT 7.0 WORKS=====================//
					//Debug.Log("Tested at 7.0");
					//Debug.Log("Current node values: " + currentNodeRow + "," + currentNodeIndex);
					//Debug.Log("Current node Row min and max: " + (currentNodeRow - 1) + "," + (currentNodeRow + 1));
					//Debug.Log("Current node Index min and max: " + (currentNodeIndex - 1) + "," + (currentNodeIndex + 1));
					//return null;

					//if g < 0 checking for nonexistant tile, so break out of this insatnace of loop
					if (gRow < 0 || gRow >= map.Count || gIndex < 0 || gIndex >= map[gIndex].Count) 
					{
						//Debug.Log ("I left in gRow and gIndex");
						continue;
					}
					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex, map.Count, map[gIndex].Count,  mapCost[gRow][gIndex][0])) 
					{
						//Debug.Log("Current node gRow and gIndex that we can reach: " + gRow + " " + gIndex);
						if (CheckIfFriendly (map[gRow][gIndex], isEnemy)) {
							//Debug.Log("Current node gRow and gIndex that got skipped: " + gRow + " " + gIndex);
							continue;

						} else {
							//Debug.Log("Current node gRow and gIndex that passed: " + gRow + " " + gIndex);
						}

						//Add the new neighbor
						List<int> neighborNode = new List<int> ();
						neighborNode.Add (gRow);
						neighborNode.Add (gIndex);



						//INSERT SOMETHING ABOUT GENERATION COUNTER

						//This commented out code is old, just have it incase I screw up later on
						//int tentativeGScore = gScore[currentNode] + ReturnCostTile(ReturnDirection(currentNodeRow, currentNodeIndex, goalRow, gIndex));
						int tentativeGScore = gScore[currentNode] + ReturnCostTile(currentNodeRow, currentNodeIndex, mapCost);

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
						//Debug.Log("Node that got added to cameFromSet: " + gRow + " " + gIndex);
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
			//return null;

			//foreach(var blah in cameFromSet){
			//	Debug.Log ("cameFromSet " + blah.Key[0] + "," + blah.Key[1] + " : "+ blah.Value[0] + "," + blah.Value[1]);
			//}

			//Debug.Log ("Size of openSet: " + openSet.Count);
			//for (int i = 0; i < openSet.Count; ++i) 
			//{
			//	Debug.Log ("openSet at " + i + " : " + openSet[i][0] + "," + openSet[i][1]);
			//}

			//return null;

			//=============DEBUG LOOP STOPPER=============//
			/*
			if (increment == endNumLoop) {
				
				Debug.Log ("Size of openSet: " + openSet.Count);
				for (int i = 0; i < openSet.Count; ++i) 
				{
					Debug.Log ("openSet at " + i + " : " + openSet[i][0] + "," + openSet[i][1]);
				}
				List<int> testing = new List<int>();
				testing.Add(3);
				testing.Add(2);
				Debug.Log("testing is equal to: " + testing[0] + "," + testing[1]);
				Debug.Log ("openSet has the list: " + openSet [47] [0] + "," + openSet [47] [1]);
				Debug.Log("Does openset contain 3,2? " + openSet.Contains(testing));

				return null;	
			} 
			else 
			{
				increment++;
			}

		*/


		}//end while loop

		//We should never get HERE. LIKE EVER
		return null;
	}

	public double pairFunction(int row, int index){
		return ((0.5) * (index + row) * (index + row + 1) ) + row;
	}

	public List<List<List<int>>> FloodFillAttackAndMovement(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int attackrange, int movement)
	{
		List<List<int>> movementTiles = FloodFillWithinRange (map, mapCost, originRow, originIndex, movement);
		List<List<int>> enhancedAttackTile = FloodFillAttackRange (map, mapCost, originRow, originIndex, attackrange + movement);

		Dictionary<double, List<int>> hash = new Dictionary<double, List<int>> ();

		List<List<int>> returnAttackTile = new List<List<int>> ();
		/*
		int x = 0;
		Debug.Log ("MTS: " + movementTiles.Count);
		Debug.Log ("EATS: " + enhancedAttackTile.Count);
		Debug.Log ("MV: " + movement);
		Debug.Log ("AR: " + attackrange);
		return null;
		*/
		for (int i = 0; i < movementTiles.Count; ++i) 
		{
			List<int> tile = new List<int> ();
			tile.Add (movementTiles [i] [0]);
			tile.Add (movementTiles [i] [1]);
			double hashValue = pairFunction(tile[0],tile[1]);
			hash[hashValue] = tile;

		}

		for (int i = 0; i < enhancedAttackTile.Count; ++i) 
		{
			List<int> tile = new List<int> ();
			tile.Add (enhancedAttackTile [i] [0]);
			tile.Add (enhancedAttackTile [i] [1]);
			double hashValue = pairFunction (tile [0], tile [1]);
			//Debug.Log ("H: " + hashValue + " R: " + tile [0] + " I: " + tile [1]);
			//hash.Add (hashValue, tile);
			if (!hash.ContainsKey (hashValue)) 
			{
				returnAttackTile.Add (tile);
			}
			/*
			x = x + 1;
			Debug.Log ("I:" + i + " C " + enhancedAttackTile.Count);
			if (x == 50) {
				Debug.Log ("This is looping");
				break;
			}
			*/
		}



		List<List<List<int>>> moveAttack = new List<List<List<int>>> (); 
		moveAttack.Add (movementTiles);
		moveAttack.Add (returnAttackTile);
		return moveAttack;

	}

	//Returns tile that are within the range of the map that can be attacked
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

		Dictionary<double, List<int>> hash = new Dictionary<double, List<int>> ();
		hash[pairFunction(originRow, originIndex)] = startPosition;

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
				index.Add (currentNodeIndex);
				index.Add (currentNodeIndex+1);
			} else {
				index.Add (currentNodeIndex-1);
				index.Add (currentNodeIndex);
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
							if (!hash.ContainsKey (pairFunction (gRow, gIndex))) 
							{
								openSet.Add (neighborNode);
								attackSet.Add (neighborNode);
								attackRangeRemain [neighborNode] = newCost;
								hash[pairFunction (gRow, gIndex)] = neighborNode;
							}

						} else {
							continue;
						}
					}
				}//end for loop

			}//end for loop

		}//end while loop
		/*
		for (int i = 0; i < attackSet.Count; ++i) {
			Debug.Log ("attackSet Results at " + i + " " + attackSet[i][0] + "," + attackSet[i][1]);
		}
		*/

		//Return tiles that are within attack range
		return attackSet;
	} 

	//Returns a list of all valid walkable tiles within range
	public List<List<int>> FloodFillWithinRange(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int movementRange)
	{
		//Debug.Log ("Starting Flood");
		var movementRemain = new Dictionary<List<int>, int>();

		List<List<int>> openSet = new List<List<int>> ();
		List<List<int>> cameFromSet =  new List<List<int>>();

		//Init Starting and goal position
		List<int> startPosition = new List<int> ();
		startPosition.Add (originRow);
		startPosition.Add (originIndex);

		movementRemain[startPosition] = movementRange;
		openSet.Add(startPosition);

		Dictionary<double, List<int>> hash = new Dictionary<double, List<int>> ();
		hash[pairFunction(originRow, originIndex)] = startPosition;

		bool isEnemy = false;
		//Check to see if this is enemy
		if (map[originRow][originIndex].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy() == true)
		{
			isEnemy = true;
		}	

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
				index.Add (currentNodeIndex);
				index.Add (currentNodeIndex+1);
			} else {
				index.Add (currentNodeIndex-1);
				index.Add (currentNodeIndex);
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
					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex, map.Count, map[gIndex].Count,  mapCost[gRow][gIndex][0])) 
					{
						//Add the new neighbor

						int newCost = movementRemain[currentNode] - ReturnCostTile(gRow, gIndex, mapCost);
						//Debug.Log ("I Got pass the new cost with " + newCost);
						if (CheckIfFriendly(map[gRow][gIndex], isEnemy) && newCost > 0) 
						{
							List<int> neighborNode = new List<int> ();
							neighborNode.Add (gRow);
							neighborNode.Add (gIndex);
							if (!hash.ContainsKey (pairFunction (gRow, gIndex))) {
								openSet.Add (neighborNode);
								movementRemain [neighborNode] = newCost;
								hash[pairFunction (gRow, gIndex)] = neighborNode;
							}
						}
						//Debug.Log ("Current node cost " + currentNodeRow + "," + currentNodeIndex + " movement " + movementRemain [currentNode] + " COST " + newCost);
						else if (newCost >= 0 && !CheckIfOccupied(map[gRow][gIndex])) {
							List<int> neighborNode = new List<int> ();
							neighborNode.Add (gRow);
							neighborNode.Add (gIndex);
							//Debug.Log ("Added the neighborNode " + neighborNode [0] + "," + neighborNode [1]);
							if (!hash.ContainsKey (pairFunction (gRow, gIndex))) {
								openSet.Add (neighborNode);
								cameFromSet.Add (neighborNode);
								movementRemain [neighborNode] = newCost;
								hash[pairFunction (gRow, gIndex)] = neighborNode;
							}
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
		//Debug.Log ("End floodfill");
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
	public int CalculateHeuristicCost(int originRow, int originIndex, int goalRow, int goalIndex)
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

	public int CalculateHeuristicCostEnemy(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		return (int) (Mathf.Abs (originRow - goalRow) + Mathf.Abs (originIndex - goalIndex));
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
	private bool CanGetNext(int originRow, int originIndex, int goalRow, int goalIndex, int mapRowCount, int mapIndexCount,  int mapType)
	{
        //Check all the various things to ensure it can get to next location
		if (!CheckBound(goalRow, goalIndex, mapRowCount, mapIndexCount) 
			|| !CheckOneTileAway(originRow,originIndex,goalRow,goalIndex) 
			|| CheckIsSelf(originRow,originIndex,goalRow,goalIndex)
            || !CheckIfWalkable( mapType)) {
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
			//Debug.Log ("Rejected is Self");
			return true;
		}
		return false;
	}//end CheckIsSelf

	//Check to see if the tile is 1 tile away
	private bool CheckOneTileAway(int originRow, int originIndex, int goalRow, int goalIndex)
	{
		if (originRow % 2 == 0) {
			if (((originIndex - goalIndex) == 0 || (originIndex - goalIndex) == -1) && Mathf.Abs (originRow - goalRow) == 1) {
				return true;
			}
		} else {
			if (((originIndex - goalIndex) == 0 || (originIndex - goalIndex) == 1) && Mathf.Abs (originRow - goalRow) == 1) {
				return true;
			}
		}
		//Debug.Log ("Rejected Not one tile away");
		return false;
	}//end checkOneTileAway

	//Check to see if the next tile is within the gamebound
	private bool CheckBound(int goalRow, int goalIndex, int maxRow, int maxIndex)
	{
        if (goalRow >= maxRow || goalIndex >= maxIndex) 
		{
			//Debug.Log ("Rejected Out of Bound");
            return false;
		}

		if ((goalRow >= 0 && goalRow < maxRow) && (goalIndex >= 0 && goalIndex < maxIndex)) {
			return true;
		}
		//Debug.Log ("Rejected Out of Bound");
        return false;
	}//end checkBound
		

	private bool CheckIfFriendly(GameObject mapTile, bool isEnemy)
	{
		//If you are an enemy AI, You cannot walk pass friendly
		if (isEnemy) 
		{
			if ((mapTile.GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy() == true)
				&&(mapTile.GetComponent<GenericEarthScript> ().GetOccupingObject () != null)) 
			{
				//Debug.Log ("I am not a friendly");
				return true;
			}
			return false;
		}	
		//If you are a friendly you can walk by friendly
		else
		{
			if ((mapTile.GetComponent<GenericEarthScript> ().GetOccupingObject () != null)
			    && (mapTile.GetComponent<GenericEarthScript> ().GetIsOccupyingObjectAnEnemy () == false)) 
			{
				return true;
			}	
			//Debug.Log ("I am a friendly");
			return false;
		}
		//Debug.Log ("Did not meet either condition in CheckIfFriendly");
	}

	private bool CheckIfOccupied(GameObject mapTile)
	{
		if (mapTile.GetComponent<GenericEarthScript> ().GetOccupingObject () == null) {
			return false;
		}
		return true;
	}

	private bool CheckIfWalkable(int mapType)
    {
        //0 -> earth, 1->water, 2->mountian
        //right now 0 means earth, and earth only walkable tile
		if (mapType == 0 
            //check if empty tile as well, nneds to be done after first if or else will check tiles without GenericEarthScript code and exception will be raised
            )
        {

            return true;
        }

        //otherwise
		//Debug.Log ("Rejected Walkable");
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
