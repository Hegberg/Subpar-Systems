using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterScript : MonoBehaviour {

    private bool hasMoved = false;
    private bool hasAttacked = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        //if player clicked on and it's players turn, and this hasn't moved this turn, then do this
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && !hasMoved)
        {
            TurnControlScript.control.SetPlayerSelected(this.gameObject);
        }
    }

    public void RefreshActions()
    {
        hasAttacked = false;
        hasMoved = false;
    }

    public void RemoveActions()
    {
        hasAttacked = true;
        hasMoved = true;
    } 

    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }
}
