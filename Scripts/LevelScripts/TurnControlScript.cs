using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnControlScript : MonoBehaviour {

    public static TurnControlScript control;

    public Transform characterParent;

    private bool playerTurn = true;
    private GameObject playerSelected;
    private GameObject enemySelected;

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

	public void LevelPassed(){
		GameControlScript.control.NextLevel ();
		SceneManager.LoadScene ("MissionMenu");
	}
		
	public void LevelFailed(){
		SceneManager.LoadScene ("MissionMenu");
	}

    public void EndTurn()
    {
        StartCoroutine(RevertTurn());
        UnHighlightPlayerTile();
        UnHighlightEnemyTile();
        playerSelected = null;
        Debug.Log("Player Turn Ended");
        LevelControlScript.control.BroadcastRemoveActionsToCharacters();
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

    public void HighlightPlayerTile()
    {
        if (playerSelected != null)
        {
            playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                material.color = Color.blue;
        }
    }

    public void UnHighlightPlayerTile()
    {
        if (playerSelected != null)
        {
            playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                material.color = Color.white;
        }
    }

    public void HighlightEnemyTile()
    {
        if (enemySelected != null)
        {
            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                material.color = Color.red;
        }
    }

    public void UnHighlightEnemyTile()
    {
        if (enemySelected != null)
        {
            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                material.color = Color.white;
        }
    }

    public void MovePlayer(GameObject tileMovingTo)
    {
        //get correct position (so tile placement but slightly up so goes to middle of tile)
        Vector3 tempTile = tileMovingTo.transform.position;
        tempTile.y += (tileMovingTo.gameObject.GetComponent<Renderer>().bounds.size.y / LevelControlScript.control.GetTileHeightRatio());

        playerSelected.transform.position = tempTile;

        //for both lines below need those scripts called to be used for walkable tiles and characters
        //so code doesn't break if try to create and use other scripts

        //character moved so set move to true so they cannot move again
        playerSelected.GetComponent<GenericCharacterScript>().SetHasMoved(true);
        //set old tile to have nothing on it
        GameObject prevTile = playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping();
        UnHighlightPlayerTile();
        prevTile.GetComponent<GenericEarthScript>().SetOccupingObject(null);

        //set tile occupying to correct tile
        playerSelected.GetComponent<GenericCharacterScript>().SetTileOccuping(tileMovingTo);
        HighlightPlayerTile();
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

    //when new player selected, set the game object and set isPlayerSelected to true since by virtue of a player being selected it is true
    public void SetPlayerSelected(GameObject selected)
    {
        if (playerSelected != null)
        {
            UnHighlightPlayerTile();
        }
        playerSelected = selected;
        if (enemySelected != null)
        {
            enemySelected.GetComponent<GenericEnemyScript>().SetIsSelected(false);
        }
        SetEnemySelected(null);
        if (playerSelected != null)
        {
            HighlightPlayerTile();
        }
    }

    public GameObject GetPlayerSelected()
    {
        return playerSelected;
    }

    public void SetEnemySelected(GameObject selected)
    {
        if (enemySelected != null)
        {
            UnHighlightEnemyTile();
        }
        enemySelected = selected;
        if (enemySelected != null)
        {
            HighlightEnemyTile();
        }
    }

    public GameObject GetEnemySelected()
    {
        return enemySelected;
    }
}
