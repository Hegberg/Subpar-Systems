using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEarthScript : TileScript {

    private GameObject occupingObject;
    private bool isOccupyingObjectAnEnemy = false;
    private List<int> tilePosition = new List<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
    //since characters can move on earth, send coordinates to move back
    void OnMouseOver()
    {
		//if player slelected and hasn't moved
        if (Input.GetMouseButtonDown(0) && (TurnControlScript.control.GetPlayerSelected() != null) && (occupingObject == null) &&
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasMoved())
        {
			
            //need to return correct tile or coordinates, 
			List<List<int>> allValidTile = new List<List<int>> ();
			List<List<int>> returnPath = new List<List<int>> ();
			List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap();
			allValidTile = TurnControlScript.control.GetAllValidMovementTiles ();

			List<List<int>> testAStarPath = new List<List<int>>();

			testAStarPath = AStarScript.control.findShitestPath (LevelControlScript.control.GetAStarMap (),
				LevelControlScript.control.GetAStarMapCost (),
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
				tilePosition [0],
				tilePosition [1]
			);

			for (int k = 0; k < testAStarPath.Count; ++k) {
				Debug.Log ("testAStarPath Results at " + k + " " + testAStarPath[k][0] + "," + testAStarPath[k][1]);
			}

			for (int i = 0; i < allValidTile.Count; ++i) {
				if (allValidTile [i] [0] == tilePosition [0] && allValidTile [i] [1] == tilePosition [1] 
					&& TurnControlScript.control.GetPlayerSelected() != null) 
				{

					GameObject player = TurnControlScript.control.GetPlayerSelected();
					//Debug.Log("null: " + (TurnControlScript.control.GetPlayerSelected() == null));

					TurnControlScript.control.MovePlayer(gameObject);
					occupingObject = player;
                    isOccupyingObjectAnEnemy = false;
					//need this or loops through again with player already moved
					break;
                }
			}
				

			//When the player clicks somewhere outside the allValidTile, NULL THE FUCKER

			//Calculate the path if the goal is within the range. Replace tilePosition[0], tilePosition[1]
			/*
			returnPath = AStarScript.control.findShitestPath(LevelControlScript.control.GetAStarMap(), 
				LevelControlScript.control.GetAStarMapCost(),
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
				tilePosition[0],
				tilePosition[1]);
				*/

			//Do something with the return path, it going to be coordinates 

            
			//TurnControlScript.control.MovePlayer(gameObject);
            
        }
    }

    public void SetOccupingObject(GameObject setTo)
    {
        occupingObject = setTo;
		if (occupingObject == null) {
			SetIsAnEnemyOccupyingThisTile (false);
		}
    }

	public void SetTilePosition(List<int> position)
	{
		tilePosition = position;
	}

	public List<int> GetTilePosition()
	{
		return tilePosition;
	}

    public GameObject GetOccupingObject()
    {
        return occupingObject;
    }

    public void SetIsAnEnemyOccupyingThisTile(bool setOccupying)
    {
        isOccupyingObjectAnEnemy = setOccupying;
    }

    public bool GetIsOccupyingObjectAnEnemy()
    {
        return isOccupyingObjectAnEnemy;
    }
}
