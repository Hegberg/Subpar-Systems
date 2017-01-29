using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

    private GameObject tileOccuping;

    private int hp = 100;
    private int attack = 100;
    private int movement = 3;
    private int range = 3;

    private bool isSelected = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && 
            TurnControlScript.control.GetPlayerSelected() != null &&
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasAttacked())
        {
            if (isSelected)
            {
                hp -= TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetAttack();
                TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().SetHasAttacked(true);
                TurnControlScript.control.SetEnemySelected(null);
                if (hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (TurnControlScript.control.GetEnemySelected() != null)
                {
                    TurnControlScript.control.GetEnemySelected().GetComponent<GenericEnemyScript>().SetIsSelected(false);
                }
                isSelected = true;
                TurnControlScript.control.SetEnemySelected(this.gameObject);
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
