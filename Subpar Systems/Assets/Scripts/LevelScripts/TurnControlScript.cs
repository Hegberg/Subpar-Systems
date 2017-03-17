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

	private List<List<int>> allValidTile = new List<List<int>> ();
    private List<List<int>> allValidAttackTile = new List<List<int>>();

	private Color playerHighlight = Color.blue;
	private Color movementHighlight = Color.cyan;
	private Color enemyCanAttackHighlight = Color.yellow;
	private Color enemyTargetedHighlight = Color.red;
	private Color restoreOriginalColor = Color.white;

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
	}
		
	public void LevelFailed(){
        GameControlScript.control.FailedLevel();
	}

    public void EndTurn()
    {
        if (playerTurn)
        {
            playerTurn = false;
            Debug.Log("Player Turn Ended");
            //need this broadcast first, so checks in Unhighlight that are rellying on what state the character in are correct
            LevelControlScript.control.BroadcastRemoveActionsToCharacters();
            UnHighlightPlayerTile();
            UnHighlightEnemyTile();
            playerSelected = null;
			EnemyParentScript.control.StartAITurn();
        }
    }

    public void StartTurn()
    {
        playerTurn = true;
        Debug.Log("Player Turn Started");
        LevelControlScript.control.BroadcastRefreshActionsToCharacters();
    }

    public void HighlightPlayerTile()
    {
        if (playerSelected != null)
        {
            playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
			material.color = playerHighlight;
        }
    }

    public void UnHighlightPlayerTile()
    {
        if (playerSelected != null)
        {
            playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                material.color = Color.white;
            //unhighlight floodfill tiles
            List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap();
            for (int i = 0; i < allValidTile.Count; ++i)
            {
				movementmap[allValidTile[i][0]][allValidTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
            }
            //Highlight enemies the player can attack
            for (int i = 0; i < allValidAttackTile.Count; ++i)
            {
                if (movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].name.ToString() == "Earth(Clone)" &&
                    movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy())
                {
					movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
                }
            }
        }
    }

    public void HighlightEnemyTile()
    {
        if (enemySelected != null)
        {
            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
			material.color = enemyTargetedHighlight;
        }
    }

	public void UnHighlightEnemyTile()
    {
		if (enemySelected != null) {
            //if enemy is within range and selected, highlight it as possible to selectplayerSelected != null && playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks () <= 0
            if (enemySelected.GetComponent<GenericEnemyScript> ().GetIsSelected ()) {

				enemySelected.GetComponent<GenericEnemyScript> ().SetIsSelected (false);
				enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<SpriteRenderer> ().
				material.color = enemyCanAttackHighlight;

			} else { //if no enemy selected, set tile back to unhighlighted
				bool moveableTile = false;
				//but if player hasn't moved and can move there, set it to moveable highlight
				if (playerSelected != null && !playerSelected.GetComponent<GenericCharacterScript> ().GetHasMoved ()) {
					/*
					Debug.Log (enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<GenericEarthScript>().GetTilePosition() [0] + 
						" " + enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<GenericEarthScript>().GetTilePosition() [1] + 
						" match to ");
						*/
					allValidTile = AStarScript.control.FloodFillWithinRange (LevelControlScript.control.GetAStarMap (), 
						LevelControlScript.control.GetAStarMapCost (),
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetMovement ());

					

                    for (int i = 0; i < allValidTile.Count; ++i)
                    {
                        //Debug.Log (allValidTile [i] [0] + " " + allValidTile [i] [1]);

                        if (enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0] == allValidTile[i][0] &&
                            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] == allValidTile[i][1])
                        {
                            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                            material.color = movementHighlight;
                            moveableTile = true;
                            break;
                        }
                    }
                    //if player can still move, recheck availible tile

                    List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap();

                    //Highlight all the valid tiles
                    for (int i = 0; i < allValidTile.Count; ++i)
                    {
                        movementmap[allValidTile[i][0]][allValidTile[i][1]].GetComponent<SpriteRenderer>().material.color = movementHighlight;
                    }
                }

				if (playerSelected != null && playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks () <= 0) {
					List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap ();
					//UnHighlight enemies the player can attack
					for (int i = 0; i < allValidAttackTile.Count; ++i) {
						if (movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].name.ToString () == "Earth(Clone)" &&
						    movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<GenericEarthScript> ().GetIsOccupyingObjectAnEnemy ()) {
							movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = restoreOriginalColor;
						}
					}
				}

				//if player is out of attacks remove highlight
				if (!moveableTile && playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() <= 0) {
					enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<SpriteRenderer> ().
					material.color = restoreOriginalColor;
				//if player has attacks and enemy alive set to enemy attackHighlight
				} else if (!moveableTile && playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() >= 0 &&
					enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetOccupingObject() != null) {
                    enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                    material.color = enemyCanAttackHighlight;
				//if tile not able to get to restore original colour
                } else if (!moveableTile) {
                    enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                    material.color = restoreOriginalColor;
                }
			}
		}
    }

    public void MovePlayer(GameObject tileMovingTo)
    {
        //get correct position (so tile placement but slightly up so goes to middle of tile)
        Vector3 tempTile = tileMovingTo.transform.position;
        tempTile.y += (tileMovingTo.gameObject.GetComponent<Renderer>().bounds.size.y / LevelControlScript.control.GetTileHeightRatio());
		tempTile.z -= 0.01f;
        playerSelected.transform.position = tempTile;

        //set old tile to have nothing on it
        GameObject prevTile = playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping();
        UnHighlightPlayerTile();
        prevTile.GetComponent<GenericEarthScript>().SetOccupingObject(null);

        //set tile occupying to correct tile
        playerSelected.GetComponent<GenericCharacterScript>().SetTileOccuping(tileMovingTo);

		//unhighlight floodfill tiles
		List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap();
		for (int i = 0; i < allValidTile.Count; ++i) {
			movementmap[allValidTile[i][0]][allValidTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
		}

		//character moved so set move to true so they cannot move again, need this line here, before breaks code, and after variables aren't corretly adjusted
		playerSelected.GetComponent<GenericCharacterScript>().SetHasMoved(true);

		HighlightPlayerTile();
		if (playerSelected != null && GetPlayerSelected().GetComponent<GenericCharacterScript>().GetNumOfAttacks() > 0)
        {
            allValidAttackTile = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(),
                LevelControlScript.control.GetAStarMapCost(),
                GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
                GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
                GetPlayerSelected().GetComponent<GenericCharacterScript>().GetRange());

            //Highlight enemies the player can attack
            for (int i = 0; i < allValidAttackTile.Count; ++i)
            {
                if (movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].name.ToString() == "Earth(Clone)" &&
                    movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy())
                {
					movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = enemyCanAttackHighlight;
                }
            }
        }
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

    //when new player selected, set the game object and set isPlayerSelected to true since by virtue of a player being selected it is true
    public void SetPlayerSelected(GameObject selected)
    {
        List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap();
        if (playerSelected != null)
        {
			//remove current player traits that are showing
			playerSelected.GetComponent<GenericCharacterScript> ().UnShowTraits ();

            UnHighlightPlayerTile();
			//unhighlight floodfill tiles
			for (int i = 0; i < allValidTile.Count; ++i) {
				movementmap[allValidTile[i][0]][allValidTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
			}
            //UnHighlight enemies the player can attack
            for (int i = 0; i < allValidAttackTile.Count; ++i)
            {
                if (movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].name.ToString() == "Earth(Clone)" &&
                    movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy())
                {
					movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
                }
            }
        }
        playerSelected = selected;
        if (enemySelected != null)
        {
            enemySelected.GetComponent<GenericEnemyScript>().SetIsSelected(false);
        }
        SetEnemySelected(null);

        if (playerSelected != null)
        {
			/*
			List<List<List<int>>> testing = new List<List<List<int>>> ();

			testing = AStarScript.control.FloodFillAttackAndMovement(LevelControlScript.control.GetAStarMap (), 
				LevelControlScript.control.GetAStarMapCost (),
				GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
				GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
				GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetRange(),
				GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetMovement ()
			);

			Debug.Log ("=====Movement=====");
			for (int i = 0; i < testing [0].Count; ++i) {
				Debug.Log(" " + testing[0][i] + " ");
			}
			Debug.Log ("=====END Movement=====");
			Debug.Log ("=====ExtendAttack=====");
			for (int i = 0; i < testing [1].Count; ++i) {
				Debug.Log(" " + testing[1][i] + " ");
			}
			Debug.Log("=====END Attack=====");
			*/
			List<List<GameObject>> map = LevelControlScript.control.GetAStarMap();
            HighlightPlayerTile();
			//but implement A* and not just teleport player with move player script
			List<List<int>> returnPath = new List<List<int>> ();
			if (!GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetHasMoved ()) {

				//Replace the '2' with the movement range of the unit
				allValidTile = AStarScript.control.FloodFillWithinRange (LevelControlScript.control.GetAStarMap (), 
					LevelControlScript.control.GetAStarMapCost (),
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetMovement());



                //Highlight all the valid tiles
				for (int i = 0; i < allValidTile.Count; ++i) {
					movementmap [allValidTile [i] [0]] [allValidTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = movementHighlight;
				}
			}

			if (GetPlayerSelected().GetComponent<GenericCharacterScript>().GetNumOfAttacks() > 0)
            {
                allValidAttackTile = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(),
                    LevelControlScript.control.GetAStarMapCost(),
                    GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0],
                    GetPlayerSelected().GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1],
                    GetPlayerSelected().GetComponent<GenericCharacterScript>().GetRange());

                //Highlight enemies the player can attack
                for (int i = 0; i < allValidAttackTile.Count; ++i)
                {
                    if (movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].name.ToString() == "Earth(Clone)" &&
                        movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy())
                    {
						movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = enemyCanAttackHighlight;
                    }
                }
            }
        }
    }

    public GameObject GetPlayerSelected()
    {
        return playerSelected;
    }

    public void SetEnemySelected(GameObject selected)
    {
		if (enemySelected != null || (playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() <= 0))
        {
			if (playerSelected != null) {
				Debug.Log ("Number of player attacks: " + playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks ());
			}
			UnHighlightEnemyTile();
		}
        enemySelected = selected;
		if (enemySelected != null) {
			HighlightEnemyTile ();
		}
    }

    public GameObject GetEnemySelected()
    {
        return enemySelected;
    }

	public List<List<int>> GetAllValidMovementTiles(){
		return allValidTile;
	}

    public List<List<int>> GetAllValidAttackTiles()
    {
        return allValidAttackTile;
    }
}
