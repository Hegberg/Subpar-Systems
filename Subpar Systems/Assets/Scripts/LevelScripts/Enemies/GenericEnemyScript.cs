using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

    private GameObject tileOccuping;

    private float hp = 100;
    private float attack = 100;
    private float movement = 3;
    private float range = 3;

    private bool isSelected = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
		//if player turn, player selected and player hasn't attacked
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && 
            TurnControlScript.control.GetPlayerSelected() != null &&
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasAttacked())
        {
            if (isSelected)
            {
				//calculate damage taken, if it is 0 attack doesn't happen
				if (TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack () != 0) {
					hp -= TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack ();
					//set player attacked to true
					TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().SetHasAttacked (true);
					//deselect enemy
					TurnControlScript.control.SetEnemySelected (null);
					isSelected = false;
					//check if enemy still alive
					if (hp <= 0) {
						Destroy (gameObject);
					}
				} else {
					//do nothing attack doesn't happen
					//Debug.Log("0 attack");
				}
            }
            else
            {
				//if another enemy selected, deselect it
                if (TurnControlScript.control.GetEnemySelected() != null)
                {
                    TurnControlScript.control.GetEnemySelected().GetComponent<GenericEnemyScript>().SetIsSelected(false);
                }
				isSelected = true;
				TurnControlScript.control.SetEnemySelected (this.gameObject);
            }
        }
    }

    void Move()
    {

    }

    void Attack()
    {

    }

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }
}
