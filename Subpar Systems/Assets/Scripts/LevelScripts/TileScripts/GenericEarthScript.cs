using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEarthScript : TileScript {

    private GameObject occupingObject;
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
        if (Input.GetMouseButtonDown(0) && (TurnControlScript.control.GetPlayerSelected() != null) && (occupingObject == null) &&
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasMoved())
        {
            //need to return correct tile or coordinates, 
            //but implement A* and not just teleport player with move player script
			List<List<int>> tempListInt = new List<List<int>>();

            tempListInt = AStarScript.control.findShitestPath(LevelControlScript.control.GetAStarMap(), 
				LevelControlScript.control.GetAStarMapCost(),
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
				TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
				tilePosition[0],
				tilePosition[1]);
			
			/*
			for (int i = 0; i < tempListInt.Count; ++i) {
				for (int j = 0; j < tempListInt[i].Count; ++j) {
					Debug.Log ("Pathfinding Results at " + i + " " + tempListInt[i][j]);
				}
			}
			*/

            GameObject player = TurnControlScript.control.GetPlayerSelected();
            TurnControlScript.control.MovePlayer(gameObject);
            occupingObject = player;
        }
    }

    public void SetOccupingObject(GameObject setTo)
    {
        occupingObject = setTo;
    }

	public void SetTilePosition(List<int> position)
	{
		tilePosition = position;
	}

	public List<int> GetTilePosition()
	{
		return tilePosition;
	}
}
