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

	public List<List<int>> traitSplash(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex)
	{
		List<List<int>> returnList = new List<List<int>> ();

		List<int> tempTile = new List<int> ();
		tempTile.Add (originRow);
		tempTile.Add (originIndex);
		returnList.Add (tempTile);

		List<int> index = new List<int> ();
		List<int> rows = new List<int> () { originRow - 1, originRow + 1 };
		if (originRow % 2 == 0) {
			index.Add (originIndex);
			index.Add (originIndex+1);
		} else {
			index.Add (originIndex-1);
			index.Add (originIndex);
		}

		for (int i = 0; i < 2; ++i) {
			int gRow = rows [i];
			for (int j = 0; j < 2; ++j) {
				int gIndex = index [j];
				//Debug.Log ("Current Node we are Checking: " + gRow + "," + gIndex);
				if (gRow < 0 || gRow >= map.Count || gIndex < 0 || gIndex >= map [gIndex].Count) {
					//Debug.Log ("I left in gRow and gIndex");
					continue;
				}
				if (CheckIfWalkable(mapCost[gRow][gIndex][0]) && CheckIfFriendly (map [gRow] [gIndex], true)) {
					List<int> validTile = new List<int> ();
					validTile.Add (gRow);
					validTile.Add (gIndex);
					returnList.Add (validTile);
				}
			}
		}
		return returnList;
	}

	public List<List<int>> findShitestPath(List<List<GameObject>> map, List<List<List<int>>> mapCost, int originRow, int originIndex, int goalRow, int goalIndex)
	{
		var gScore = new Dictionary<double, int>();
		var fScore = new Dictionary<double, int>();

		Dictionary<double, List<int>> openSet = new Dictionary<double, List<int>>();
		List<double> closeSet = new List<double>();
		Dictionary<double, double> cameFromSet =  new Dictionary<double, double>();

		//Init Starting and goal position
		double startPosition = pairFunction(originRow, originIndex);
		double goalPosition =  pairFunction(goalRow, goalIndex);

		//INSERT SOMETHING ABOUT GENERATION COUNTER
		bool isEnemy = false;
		//Check to see if this is enemy
	
		//CODE FOR PASSING THROUGH UNIT

		// j = Index
		for (int i = 0; i < map.Count; ++i)
		{
			for (int j = 0; j < map[i].Count; ++j) 
			{
				double tempTilePosition = pairFunction (i, j);

				gScore [tempTilePosition] = int.MaxValue;
				fScore [tempTilePosition] = int.MaxValue;
			}//end inner for loop

		}//end for loop


		gScore[startPosition] = 0;
		fScore [startPosition] = CalculateHeuristicCost (originRow, originIndex, goalRow, goalIndex);

		//Debug.Log ("startPosition: " + startPosition + " ,startRow: " + originRow + " ,startIndex: " + originIndex);
		//Debug.Log ("fScore: " + fScore [startPosition]);

		//Insert the starting node 
		openSet [startPosition] = reversePair (startPosition); 

		//Visit the nodes in openSet
		double lowestFScore;
		int loopCount = 0;
		while(!(openSet.Count == 0))
		{
			loopCount += 1;
			if (loopCount > 50) {
				return null;
			}
				

			//Clear the current node
			double currentNode = -1;

			//Find the lowest fScore in the opensEt
			lowestFScore = double.MaxValue;

			foreach (var tile in openSet) 
			{
				if (fScore[tile.Key]< lowestFScore) 
				{
					lowestFScore = fScore[tile.Key];
					currentNode = tile.Key;
				}
			}
				
			//Debug.Log ("CurrentNode: " + currentNode);
			//Debug.Log ("lowestFScore: " + lowestFScore);

			//Found the goal
			if (currentNode == goalPosition) 
			{
				//Debug.Log ("Got here");
				return ReconstructPath (cameFromSet, currentNode);
			}

			//Pop the currentNode from openSet
			openSet.Remove(currentNode);
			closeSet.Add (currentNode);


			//INSERT SOMETHING ABOUT GENERATION COUNTER

			List<int> tempNode = reversePair (currentNode);

			int currentNodeRow = tempNode[0];
			int currentNodeIndex = tempNode[1];

			//Debug.Log ("currentNodeIndex: " + currentNodeIndex + " ,currentNodeRow: " + currentNodeRow);

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


			//Iterate through all the surrounding nodes
			for(int i = 0; i < 2; ++i) 
			{
				int gRow = rows [i];
				for (int j = 0; j < 2; ++j)
				{
					int gIndex = index [j];


					//if g < 0 checking for nonexistant tile, so break out of this insatnace of loop
					if (gRow < 0 || gRow >= map.Count || gIndex < 0 || gIndex >= map[gIndex].Count) 
					{
						//Debug.Log ("I left in gRow and gIndex");
						continue;
					}
					if (CanGetNext (currentNodeRow, currentNodeIndex, gRow, gIndex, map.Count, map[gIndex].Count,  mapCost[gRow][gIndex][0])) 
					{

						if (CheckIfFriendly (map[gRow][gIndex], isEnemy) && gRow != goalRow && gIndex != goalIndex) {
							//Debug.Log("Current node gRow and gIndex that got skipped: " + gRow + " " + gIndex);
							continue;

						} 
						//Add the new neighbor
						double neighborNode = pairFunction(gRow, gIndex);

						//This commented out code is old, just have it incase I screw up later on
						//int tentativeGScore = gScore[currentNode] + ReturnCostTile(ReturnDirection(currentNodeRow, currentNodeIndex, goalRow, gIndex));
						int tentativeGScore = gScore[currentNode] + ReturnCostTile(currentNodeRow, currentNodeIndex, mapCost);


						//Add the neighbor to the openset if it not there
						//Also check to see if the tentativeGScore is less than the one current stored for neighbor
						if (!closeSet.Contains(neighborNode)) {
							openSet [neighborNode] = reversePair (neighborNode);

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

	public List<int> reversePair(double score)
	{
		//Debug.Log ("Score: " + score);
		int w = (int) (Mathf.Floor ((Mathf.Sqrt ((8 * ((float)score)) + 1) - 1) / 2));
		//Debug.Log ("w: " + w);
		int t = (int)((Mathf.Pow (w, 2) + w) / 2);
		//Debug.Log ("t: " + t);
		int index = ((int)score) - t;
		//Debug.Log ("index: " + index);
		int row = w - index;
		//Debug.Log ("row: " + row);

		List<int> returnList = new List<int> ();
		returnList.Add (row);
		returnList.Add (index);
		return returnList;
	}

	public double pairFunction(int row, int index){
		return ((0.5) * (row + index) * (row + index + 1) ) + index;
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

			bool isEnemy = false;
			//Check to see if this is enemy
			if (map[originRow][originIndex].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy() == true)
			{
				isEnemy = true;
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
							if (!CheckIfWalkable (mapCost [gRow] [gIndex] [0]) || CheckIfFriendly(map[gRow][gIndex], isEnemy)) {
								if (!hash.ContainsKey (pairFunction (gRow, gIndex))) {
									openSet.Add (neighborNode);
									attackRangeRemain [neighborNode] = newCost;
									hash [pairFunction (gRow, gIndex)] = neighborNode;
								}
							} else if (!hash.ContainsKey (pairFunction (gRow, gIndex))) {
								openSet.Add (neighborNode);
								attackSet.Add (neighborNode);
								attackRangeRemain [neighborNode] = newCost;
								hash [pairFunction (gRow, gIndex)] = neighborNode;
							} else {
								continue;
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
	private List<List<int>> ReconstructPath(Dictionary<double, double> cameFromDic, double currentNode)
	{
		double current = currentNode;
		List<List<int>> finalPath = new List<List<int>> ();

		//Add the end node
		//Debug.Log("The goal node is: " + currentNode[0] + " " +  currentNode[1]);


		for (int i = 0; i < cameFromDic.Count; ++i) 
		{
			if (cameFromDic.ContainsKey (current)) 
			{
				//Add the current to final path
				finalPath.Add (reversePair(cameFromDic[current]));

				//Debug.Log ("Added to final path: " + cameFromDic[current][0] + "," + cameFromDic[current][1]);

				//Set the next current as the "came from"
				current = cameFromDic[current];
			}
		}//end for loop
			

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
