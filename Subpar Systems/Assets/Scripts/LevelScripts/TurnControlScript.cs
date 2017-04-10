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

	private List<List<List<int>>> allTiles = new List<List<List<int>>> ();
	private List<List<int>> allValidTile = new List<List<int>> ();
    private List<List<int>> allValidAttackTile = new List<List<int>>();
	private List<List<int>> allValidAttackReachTile = new List<List<int>> ();

	List<List<GameObject>> movementmap = new List<List<GameObject>> ();

	private Color playerHighlight = new Color(0.0f,1f,1f,1); //cyan
	private Color playerHighlightSwitch = new Color(0,0.5f,0.5f,1); //darker cyan
	//private Color playerHighlightSwitch = Color.white;

	private Color movementHighlight = Color.cyan;
	private Color enemyCanAttackHighlight = Color.yellow;
	private Color enemyWithinAttackRange = Color.red;

	private Color enemyTargetedHighlight = new Color(1,0.0f,0.0f,1); //bright red
	//private Color enemyTargetedHighlightSwitch = new Color(0.2f,0,0,1); //dark red
	private Color enemyTargetedHighlightSwitch = Color.white;

	private Color restoreOriginalColor = Color.white;

	private float switchTimer = 0.5f;

	private int turnCounter = 0;
	private bool usingTurnCounter = false;

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


		float H;
		float S;
		float V;

		Color.RGBToHSV (playerHighlight, out H, out S, out V);
		S = 0;
		playerHighlight = Color.HSVToRGB(H, S, V);
		/*
		H = S = V = 0;

		Color.RGBToHSV (enemyTargetedHighlight, out H, out S, out V);
		S = 0;
		enemyTargetedHighlight = Color.HSVToRGB(H, S, V);
		*/

		movementmap = LevelControlScript.control.GetAStarMap();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void EndTurn()
    {
        if (playerTurn)
        {
            playerTurn = false;
			UpdateTurn ();
            //Debug.Log("Player Turn Ended");
            //need this broadcast first, so checks in Unhighlight that are rellying on what state the character in are correct
            LevelControlScript.control.BroadcastRemoveActionsToCharacters();
            UnHighlightPlayerTile();
            UnHighlightEnemyTile();
            playerSelected = null;
			EnemyParentScript.control.StartAITurn();
        }
    }

	public void AllEnemiesHaveAttacked() {
		if (usingTurnCounter) {
			CompleteLevelConditions.control.CheckIfSurvivedEnoughTurns(GameControlScript.control.GetSurviveToThisTurn());
		}
		StartTurn ();
	}

    public void StartTurn()
    {
        playerTurn = true;
        //Debug.Log("Player Turn Started");
        LevelControlScript.control.BroadcastRefreshActionsToCharacters();
    }

	IEnumerator FlashingPlayerTile() {
		/*
		Shader shaderGUItext;
		shaderGUItext = Shader.Find ("GUI/Text Shader");
		*/
		while (playerSelected) {
			playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<Renderer>().
			material.color = Color.Lerp(playerHighlightSwitch, playerHighlight, Mathf.PingPong(Time.time, switchTimer));

			yield return new WaitForSeconds(0.001f);
		}
		//yield return new WaitForSeconds(0.01f);
	}

	IEnumerator FlashingEnemyTile() {
		while (enemySelected) {
			enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<Renderer>().
			material.color = Color.Lerp(enemyTargetedHighlight, enemyTargetedHighlightSwitch, Mathf.PingPong(Time.time, switchTimer));

			yield return new WaitForSeconds(0.001f);
		}
		//yield return new WaitForSeconds(0.01f);
	}

    public void HighlightPlayerTile()
    {
        if (playerSelected != null)
        {
			/*
            playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
			material.color = playerHighlight;
			*/
			/*
			playerSelected.GetComponent<GenericCharacterScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
			material.color = Color.Lerp(Color.white, Color.cyan, Mathf.PingPong(Time.time, 1));
			*/
			StartCoroutine (FlashingPlayerTile ());
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
			/*
            enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
			material.color = enemyTargetedHighlight;
			*/
			StartCoroutine (FlashingEnemyTile ());
        }
    }

	public void UnHighlightEnemyTile()
    {
		if (enemySelected != null) {
			/*
			Debug.Log("Enemy Selected " + enemySelected.GetComponent<GenericEnemyScript> ().GetIsSelected ());
			Debug.Log("Player Selected " + enemySelected.GetComponent<GenericEnemyScript> ().GetIsSelected ());
			Debug.Log("Has Attacks " + (playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks () >= 0));
			*/
			//if enemy is within range and selected, highlight it as possible to attack when switching to a deifferent enemy to attack
			if (enemySelected.GetComponent<GenericEnemyScript> ().GetIsSelected () ) {

				enemySelected.GetComponent<GenericEnemyScript> ().SetIsSelected (false);
				enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<SpriteRenderer> ().
				material.color = enemyWithinAttackRange;
				//Debug.Log ("1");

				//Highlight enemies the player can attack
				for (int i = 0; i < allValidAttackTile.Count; ++i) {
					if (movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].name.ToString () == "Earth(Clone)" &&
						movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<GenericEarthScript> ().GetIsOccupyingObjectAnEnemy ()) {
						movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = enemyWithinAttackRange;
					}
				}

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

					//Debug.Log ("2");
                }

				//player has no attacks left, so unhighlight enemies
				if (playerSelected != null && playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks () <= 0) {

					RemoveExtraReachAttackTiles ();

					List<List<GameObject>> movementmap = LevelControlScript.control.GetAStarMap ();
					//UnHighlight enemies the player can attack
					for (int i = 0; i < allValidAttackTile.Count; ++i) {
						if (movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].name.ToString () == "Earth(Clone)" &&
						    movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<GenericEarthScript> ().GetIsOccupyingObjectAnEnemy ()) {
							movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = restoreOriginalColor;
						}
					}
					//Debug.Log ("3");
				}

				bool contains = false;

				//used for second check below
				if (playerSelected != null) {
					
					allValidAttackTile = AStarScript.control.FloodFillAttackRange (LevelControlScript.control.GetAStarMap (),
						LevelControlScript.control.GetAStarMapCost (),
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
						GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetRange ());

					foreach (var tile in allValidAttackTile) {
						if (tile[0] ==  enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition ()[0] 
							&& tile[1] ==  enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition ()[1]) {
							contains = true;
						}
					}
				}

				//if player is out of attacks remove highlight
				if (!moveableTile && playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() <= 0) {
					enemySelected.GetComponent<GenericEnemyScript> ().GetTileOccuping ().GetComponent<SpriteRenderer> ().
					material.color = restoreOriginalColor;
					//Debug.Log ("a");
				//if player has attacks and enemy alive and within range set to enemy attackHighlight
				} else if (!moveableTile && playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() > 0 &&
					enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<GenericEarthScript>().GetOccupingObject() != null &&
					contains) {
                    enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
					material.color = enemyWithinAttackRange;
					//Debug.Log ("b");

					for (int i = 0; i < allValidAttackTile.Count; ++i)
					{
						if (movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].name.ToString() == "Earth(Clone)" &&
							movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<GenericEarthScript>().GetIsOccupyingObjectAnEnemy())
						{
							movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = enemyWithinAttackRange;
						}
					}
				//if tile not able to get to restore original colour
                } else if (!moveableTile) {
                    enemySelected.GetComponent<GenericEnemyScript>().GetTileOccuping().GetComponent<SpriteRenderer>().
                    material.color = restoreOriginalColor;
					//Debug.Log ("c");
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

		//unhighlight extra reach attack tiles
		RemoveExtraReachAttackTiles();

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
					movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = enemyWithinAttackRange;
                }
            }
        }
    }

    public bool GetPlayerTurn()
    {
        return playerTurn;
    }

	public void RemoveExtraReachAttackTiles(){
		//Highlight all the reachable attack tiles
		for (int i = 0; i < allValidAttackReachTile.Count; ++i) {
			movementmap [allValidAttackReachTile [i] [0]] [allValidAttackReachTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = restoreOriginalColor;
		}
	}

    //when new player selected, set the game object and set isPlayerSelected to true since by virtue of a player being selected it is true
    public void SetPlayerSelected(GameObject selected)
    {
        if (playerSelected != null)
        {
			//remove current player traits that are showing
			playerSelected.GetComponent<GenericCharacterScript> ().UnShowTraits ();

            UnHighlightPlayerTile();
			//unhighlight floodfill tiles
			for (int i = 0; i < allValidTile.Count; ++i) {
				movementmap[allValidTile[i][0]][allValidTile[i][1]].GetComponent<SpriteRenderer>().material.color = restoreOriginalColor;
			}

			//unhighlight extra reach tiles
			RemoveExtraReachAttackTiles();

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
				Debug.Log("M:" + testing[0][i][0] + " " + testing[1][i][1]);
			}
			Debug.Log ("=====END Movement=====");
			Debug.Log ("=====ExtendAttack=====");
			for (int i = 0; i < testing [1].Count; ++i) {
				Debug.Log("A" + testing[1][i][0] + " " + testing[1][i][1]);
			}
			Debug.Log("=====END Attack=====");
			*/
			List<List<GameObject>> map = LevelControlScript.control.GetAStarMap();
            HighlightPlayerTile();
			//but implement A* and not just teleport player with move player script
			List<List<int>> returnPath = new List<List<int>> ();

			if (!GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetHasMoved () &&
				GetPlayerSelected().GetComponent<GenericCharacterScript>().GetNumOfAttacks() > 0) {

				allTiles =  AStarScript.control.FloodFillAttackAndMovement (LevelControlScript.control.GetAStarMap (), 
					LevelControlScript.control.GetAStarMapCost (),
					GetPlayerSelected().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
					GetPlayerSelected().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
					GetPlayerSelected().GetComponent<GenericCharacterScript>().GetRange(),
					GetPlayerSelected().GetComponent<GenericCharacterScript> ().GetMovement());

				allValidTile = allTiles [0];
				allValidAttackReachTile = allTiles [1];

				/*
				Debug.Log ("=====Movement=====");
				for (int i = 0; i < allTiles [0].Count; ++i) {
					Debug.Log("M:" + allTiles[0][i][0] + " " + allTiles[1][i][1]);
				}
				Debug.Log ("=====END Movement=====");
				Debug.Log ("=====ExtendAttack=====");
				for (int i = 0; i < allTiles [1].Count; ++i) {
					Debug.Log("A" + allTiles[1][i][0] + " " + allTiles[1][i][1]);
				}
				Debug.Log("=====END Attack=====");
				*/
				//Highlight all the valid tiles
				for (int i = 0; i < allValidTile.Count; ++i) {
					movementmap [allValidTile [i] [0]] [allValidTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = movementHighlight;
				}

				//Highlight all the reachable attack tiles
				for (int i = 0; i < allValidAttackReachTile.Count; ++i) {
					movementmap [allValidAttackReachTile [i] [0]] [allValidAttackReachTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = enemyCanAttackHighlight;
				}

			}
			else if (!GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetHasMoved ()) {

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
						movementmap[allValidAttackTile[i][0]][allValidAttackTile[i][1]].GetComponent<SpriteRenderer>().material.color = enemyWithinAttackRange;
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
		bool removedExtraTiles = false;

		if (enemySelected != null || (playerSelected != null && playerSelected.GetComponent<GenericCharacterScript>().GetNumOfAttacks() <= 0))
        {
			if (playerSelected != null && !playerSelected.GetComponent<GenericCharacterScript> ().GetHasMoved ()) {
				//Do not remove extra reach tiles
			} else {
				RemoveExtraReachAttackTiles ();
				removedExtraTiles = true;
			}

			if (playerSelected != null) {
				//Debug.Log ("Number of player attacks: " + playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks ());
			}
			UnHighlightEnemyTile();
		}

        enemySelected = selected;

		if (!removedExtraTiles) {
			if (playerSelected != null && !playerSelected.GetComponent<GenericCharacterScript> ().GetHasMoved ()) {
				//Do not remove extra reach tiles
			} else {
				RemoveExtraReachAttackTiles ();
			}

			if (playerSelected != null && playerSelected.GetComponent<GenericCharacterScript> ().GetNumOfAttacks() > 0) {
				allValidAttackTile = AStarScript.control.FloodFillAttackRange (LevelControlScript.control.GetAStarMap (), 
					LevelControlScript.control.GetAStarMapCost (),
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0],
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1],
					GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetRange ());

				//Highlight enemies the player can attack
				for (int i = 0; i < allValidAttackTile.Count; ++i) {
					if (movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].name.ToString () == "Earth(Clone)" &&
					   movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<GenericEarthScript> ().GetIsOccupyingObjectAnEnemy ()) {
						movementmap [allValidAttackTile [i] [0]] [allValidAttackTile [i] [1]].GetComponent<SpriteRenderer> ().material.color = enemyWithinAttackRange;
						//Debug.Log ("7");
					}
				}
			}
		}

		if (enemySelected != null) {
			HighlightEnemyTile ();
		}
    }

	public void UpdateTurn() {
		++turnCounter;
	}

	public void ResetTurnCounter(){
		turnCounter = 0;
	}

	public void SetUsingTurnCounter(bool newUsingTurnCounter){
		usingTurnCounter = newUsingTurnCounter;
	}

	public int GetCurrentTurn() {
		return turnCounter;
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
