using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEarthScript : TileScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //since characters can move on earth, send coordinates to move back
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetIsPlayerSelected())
        {
            //need to return correct tile or coordinates, 
            //but implement A* and not just teleport player with move player script
            TurnControlScript.control.MovePlayer(gameObject.transform.position);
        }
    }

    public override bool CanPlayerMoveHere()
    {
        return true;
    }
}
