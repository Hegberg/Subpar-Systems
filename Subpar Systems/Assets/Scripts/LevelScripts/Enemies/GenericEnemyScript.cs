using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

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
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && 
            !TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetHasAttacked())
        {
            hp -= TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetAttack();
            TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().SetHasAttacked(true);
            if (hp <= 0)
            {
                Destroy(gameObject);
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
}
