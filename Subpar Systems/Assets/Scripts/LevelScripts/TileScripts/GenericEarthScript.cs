using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEarthScript : TileScript {

    private GameObject occupingObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //since characters can move on earth, send coordinates to move back
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && (TurnControlScript.control.GetPlayerSelected() != null) && (occupingObject == null))
        {
            //need to return correct tile or coordinates, 
            //but implement A* and not just teleport player with move player script
            GameObject player = TurnControlScript.control.GetPlayerSelected();
            TurnControlScript.control.MovePlayer(gameObject);
            occupingObject = player;
        }
    }

    public override bool CanPlayerMoveHere()
    {
        return true;
    }

    public bool GetOccupingObject()
    {
        return occupingObject;
    }

    public void SetOccupingObject(GameObject setTo)
    {
        occupingObject = setTo;
    }
}
