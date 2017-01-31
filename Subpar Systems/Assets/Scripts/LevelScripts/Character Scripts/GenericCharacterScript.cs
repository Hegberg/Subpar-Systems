using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterScript : MonoBehaviour {

    private bool hasMoved = false;
    private bool hasAttacked = false;

    private GameObject tileOccuping;

    private int hp = 100;
    private int attack = 100;
    private int movement = 3;
    private int range = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        //if player clicked on and it's players turn, and this hasn't moved this turn, then do this
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn())
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

    public void SetHasAttacked(bool attacked)
    {
        hasAttacked = attacked;
    }

    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public bool GetHasAttacked()
    {
        return hasAttacked;
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int hpChangedTo)
    {
        hp = hpChangedTo;
    }

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
    }
}
