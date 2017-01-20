using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControlScript : MonoBehaviour {

    public static TurnControlScript control;

    public Transform characterParent;

    private bool playerTurn = true;
    private bool isPlayerSelected = false;
    private GameObject playerSelected;

    public Button endTurn;

    // Use this for initialization
    void Start () {
        if (control == null)
        {
            control = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Button btn = endTurn.GetComponent<Button>();
        btn.onClick.AddListener(EndTurn);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        StartCoroutine(RevertTurn());
        Debug.Log("Player Turn Ended");
    }

    public void StartTurn()
    {
        playerTurn = true;
        Debug.Log("Player Turn Started");
        LevelControlScript.control.BroadcastRefreshActionsToCharacters();
    }

    IEnumerator RevertTurn()
    {
        yield return new WaitForSeconds(2);
        StartTurn();
    }

    public void MovePlayer(Vector2 positionToMoveto)
    {
        playerSelected.transform.position = positionToMoveto;
        //character moved so set move to true so they cannot move again
        playerSelected.GetComponent<GenericCharacterScript>().SetHasMoved(true);
        //player now needs to select new player
        SetIsPlayerSelected(false);
        playerSelected = null;
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

    public bool GetIsPlayerSelected()
    { 
        return isPlayerSelected;
    }

    public void SetIsPlayerSelected(bool selected)
    {
        isPlayerSelected = selected;
    }

    //when new player selected, set the game object and set isPlayerSelected to true since by virtue of a player being selected it is true
    public void SetPlayerSelected(GameObject selected)
    {
        playerSelected = selected;
        SetIsPlayerSelected(true);
    }
}
