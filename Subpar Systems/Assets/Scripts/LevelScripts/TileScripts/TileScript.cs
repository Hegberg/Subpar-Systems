using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    private bool playerCanMoveHere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && TurnControlScript.control.GetIsPlayerSelected())
        {
            
        }
    }
    */

    public virtual bool CanPlayerMoveHere()
    {
        return false;
    }
}
