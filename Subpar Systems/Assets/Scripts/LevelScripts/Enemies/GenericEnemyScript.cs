using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

	protected GameObject tileOccuping;
	protected float hp = 100;
    protected float maxHP = 100;
    protected float attack = 10;
	protected float movement = 3;
	protected float range = 3;
	protected float DetectRadius = 5;
	protected bool Detected = false;
	protected bool isSelected = false;

	public GUIStyle guiStyle;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        Vector2 targetPos;
		guiStyle.fontSize = (int)(250 / GameControlScript.control.GetCameraZoom ());
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 600 / GameControlScript.control.GetCameraZoom(), 
			200 / GameControlScript.control.GetCameraZoom()), hp + "/" + maxHP, guiStyle);
    }

	void OnMouseOver()
    {
		//if player turn, player selected and player hasn't attacked
        if (Input.GetMouseButtonDown(0) && TurnControlScript.control.GetPlayerTurn() && 
            TurnControlScript.control.GetPlayerSelected() != null &&
			TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetNumOfAttacks() > 0)
        {

            List<List<int>> validAttackTiles = TurnControlScript.control.GetAllValidAttackTiles();

            bool isValidTarget = false;
            /*
            Debug.Log(validAttackTiles.Count + " count");
            Debug.Log(GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0] + " "
                 + GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] + " pos"); 
                 */
            for (int i = 0; i < validAttackTiles.Count; ++i)
            {
                //Debug.Log(validAttackTiles[i][0] + " " + validAttackTiles[i][1]);
                if (validAttackTiles[i][0] == GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[0] &&
                    validAttackTiles[i][1] == GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1])
                {
                    //Debug.Log("match");
                    isValidTarget = true;
                    break;
                }
            }

            if (isValidTarget)
            {
                if (isSelected)
                {
                    //calculate damage taken, if it is 0 attack doesn't happen
                    if (TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetAttack() != 0)
                    {

						List<GenericTraitsScript> playerTraits = TurnControlScript.control.GetPlayerSelected ().
							GetComponent<GenericCharacterScript> ().GetTraits ();
						bool splash = false;
						for (int i = 0; i < playerTraits.Count; ++i) {
							if (playerTraits [i].GetName () == "Grenedier") {
								splash = true;
								break;
							}
						}

						if (splash) {
							List<List<int>> splashTiles = AStarScript.control.traitSplash (LevelControlScript.control.GetAStarMap(), 
								LevelControlScript.control.GetAStarMapCost(),
								tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0], 
								tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
							//Debug.Log (splashTiles.Count);

							bool attacked = false;

							for (int i = 0; i < splashTiles.Count; ++i) {
								for (int j = 0; j < GameControlScript.control.GetInGameEnemyList ().Count; ++j) {
									if (GameControlScript.control.GetInGameEnemyList () [j].GetComponent<GenericEnemyScript> ().
										GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition ()[0] == splashTiles [i][0] &&
										GameControlScript.control.GetInGameEnemyList () [j].GetComponent<GenericEnemyScript> ().
										GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition ()[1] == splashTiles [i][1]) {

										if (GameControlScript.control.GetInGameEnemyList () [j] != this.gameObject) {

											GameControlScript.control.GetInGameEnemyList () [j].GetComponent<GenericEnemyScript> ().
											SetHP (GameControlScript.control.GetInGameEnemyList () [j].GetComponent<GenericEnemyScript> ().GetHP() - TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack ());
											Debug.Log (TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack ());
											attacked = true;
											//Debug.Log ("Secondary hp" + hp);
										} else {
											//this doesn't perform death check until after everything else resolves
											hp -= TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().GetAttack ();
											//Debug.Log ("Primary hp" + hp);
											attacked = true;
										}
									}
								}
							}
							if (attacked) {
								//set player attacked to true
								TurnControlScript.control.GetPlayerSelected ().GetComponent<GenericCharacterScript> ().PlayerAttacked (1);
							}
						} else {
							hp -= TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetAttack();
							//set player attacked to true
							TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().PlayerAttacked(1);
						}

                        isSelected = false;
                        //check if enemy still alive
						if (hp <= 0) {
							EnemyParentScript.control.EnemyDied ();
							GameControlScript.control.RemoveEnemyFromInGameList (this.gameObject);
							tileOccuping.GetComponent<GenericEarthScript> ().SetOccupingObject (null);
							//need this repeated twice since if this is before the line above, the line above will malfunction, 
							//but if not in else than code malfunction
							TurnControlScript.control.SetEnemySelected(null);

							Destroy (gameObject);
						} else {
							TurnControlScript.control.SetEnemySelected(null);
						}
                    }
                    else
                    {
                        //do nothing attack doesn't happen
                        //Debug.Log("0 attack");
                    }
                }
                else
                {
                    //if another enemy selected, deselect it (done in turn control)
                    //set this enemy to selected regardless
                    isSelected = true;
                    TurnControlScript.control.SetEnemySelected(this.gameObject);
                }
            }
        }
    }

	public void HPLost(int hpLost) {
		SetHP (hp - hpLost);
	}

	public virtual void Move()
    {
        //if any players alive, move
        if (GameControlScript.control.GetInGameCharacterList().Count > 0)
        {
            List<List<int>> FloodFillTiles = new List<List<int>>();
			List<List<int>> CheckDetectRadius = new List<List<int>> ();
			//Return all valid movement tiles

            //broken, not giving tiles in a few cases, breaks nearest tile code below
            //Debug.Log("Current tile position is row " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0] + ", Index " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
            FloodFillTiles = AStarScript.control.FloodFillWithinRange(LevelControlScript.control.GetAStarMap(),
                LevelControlScript.control.GetAStarMapCost(),
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
                (int)movement);

			CheckDetectRadius = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(),
				LevelControlScript.control.GetAStarMapCost(),
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
				(int)DetectRadius);

            //Debug.Log("FloodFillTiles Count = " + FloodFillTiles.Count + " with movement range " + (int)movement);
            //Debug.Log(FloodFillTiles.Count + " count " + (int)movement + " movement");
            //Debug.Log("what");

            //Find the closest "player character"
			GameObject nearestPlayer = new GameObject();
			nearestPlayer =	FindClosestPlayer();

            List<int> closest = new List<int>();

            closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                    GetComponent<GenericEarthScript>().GetTilePosition();
			//Debug.Log ("I am at row " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [0] + " ,  " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [1]);
            //Debug.Log("Closest Player position is row " + closest[0] + "," + closest[1]);

			List<int> nearestTile = new List<int>();
			int closestTileValue = int.MaxValue;

			List<List<GameObject>> tempMap = LevelControlScript.control.GetAStarMap();

			foreach (var elementTile in CheckDetectRadius) {
				if (elementTile [0] == closest [0] &&
				   elementTile [1] == closest [1]) 
				{
					Detected = true;

					break;
				}
			}

			if (!Detected) 
			{
				Detected = false;
				return; 
			}
				
			List<List<int>> testing = new List<List<int>>();
			//Return all valid movement tiles

			//broken, not giving tiles in a few cases, breaks nearest tile code below
			//Debug.Log("Current tile position is row " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0] + ", Index " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
			testing = AStarScript.control.findShitestPath(LevelControlScript.control.GetAStarMap(),
				LevelControlScript.control.GetAStarMapCost(),
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
				tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
				closest[0],
				closest[1]);
			/*
			Debug.Log ("===Starting Test===");
			Debug.Log ("SR: " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [0] + " ,SI: " + tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [1]);
			for (int i = 0; i < testing.Count; ++i) {
				Debug.Log ("R: " + testing[i][0] + " ,I: " + testing[i][1]);
			}
			Debug.Log ("===Ending Test===");
			*/
			if (testing == null) {
				return;
			}

			GameObject tile = null;
			//Debug.Log("Current tile position is row " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0] + ", Index " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
			//Debug.Log("TestCount: " + testing.Count + " Movement: " + ((int)movement));
			if (testing.Count <= ((int)movement)) {
				int temp = 1;
				tile = tempMap[testing[testing.Count-temp][0]][testing[testing.Count-temp][1]];
				if (tile.GetComponent<GenericEarthScript> ().GetOccupingObject () != null) {
					if ((testing.Count-temp) > 0) {
						temp += 1;
						tile = tempMap [testing [testing.Count - temp] [0]] [testing [testing.Count - temp] [1]];
					} else {
						return;
					}
				}
			} else {
				int temp = 0;
				tile = tempMap[testing[(int)movement - temp][0]][testing[(int)movement- temp][1]];
				if (tile.GetComponent<GenericEarthScript> ().GetOccupingObject () != null) {
					if (((int)movement - temp) > 0) {
						temp += 1;
						tile = tempMap[testing[(int)movement - temp][0]][testing[(int)movement- temp][1]];
					} else {
						return;
					}
				}
			}
			//this is broken, nearest tile should give a tile, without this check, errors that shouldn't happen get thrown

			MoveToTile(tile);
		}
	}


	public virtual void Attack()
    {
        //if any players alive, attack
        if (GameControlScript.control.GetInGameCharacterList().Count > 0)
        {
            List<List<int>> FloodFillTiles = new List<List<int>>();
            List<List<GameObject>> map = LevelControlScript.control.GetAStarMap();
            //Return all valid movement tiles
            FloodFillTiles = AStarScript.control.FloodFillAttackRange(LevelControlScript.control.GetAStarMap(),
                LevelControlScript.control.GetAStarMapCost(),
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
                (int)range);

            //Find the closest "player character"
            GameObject nearestPlayer = FindClosestPlayer();

            List<int> targetPos = new List<int>();

            targetPos = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                    GetComponent<GenericEarthScript>().GetTilePosition();

            bool playerTileFound = false;

            //if character is within character tile, allow them to attack that character
            for (int i = 0; i < FloodFillTiles.Count; ++i)
            {
                if (targetPos[0] == FloodFillTiles[i][0] && targetPos[1] == FloodFillTiles[i][1])
                {
                    playerTileFound = true;
                    break;
                }
            }

            //Now attack the player standing on the neartestTile
            if (playerTileFound)
            {
                nearestPlayer.GetComponent<GenericCharacterScript>().HPLost((int)attack);
            }
        }
    }
    
    public GameObject FindClosestPlayer()
    {
        int closestCharacterTileValue = int.MaxValue;
        GameObject nearestPlayer = new GameObject();
        //when no players alive do nothing
        if (GameControlScript.control.GetInGameCharacterList().Count > 0)
        {
			
            nearestPlayer = GameControlScript.control.GetInGameCharacterList()[0];
            List<int> closest = new List<int>();
            closest = GameControlScript.control.GetInGameCharacterList()[0].GetComponent<GenericCharacterScript>().
                GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition();

			int sourceTileRow = tileOccuping.GetComponent<GenericEarthScript> ().GetTilePosition () [0];
			int sourceTileIndex = tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1];

			int closestDis = AStarScript.control.CalculateHeuristicCost (sourceTileRow, sourceTileIndex, closest [0], closest [1]);
			int x = 0;
            foreach (var character in GameControlScript.control.GetInGameCharacterList())
            {
                //stop from checking against itself if that condition happens, will always happen for [0]
                //if (character != nearestPlayer)
                //{
				x = x + 1;
				int newClosestDis = AStarScript.control.CalculateHeuristicCostEnemy (
						                    sourceTileRow,
						                    sourceTileIndex,
						                    character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0], 
						                    character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1] 
					                    );                    	
						//int difference = Mathf.Abs(character.GetComponent<GenericCharacterScript>().GetTileOccuping().
                        //GetComponent<GenericEarthScript>().GetTilePosition()[0] - closest[0]) + Mathf.Abs(character.GetComponent<GenericCharacterScript>().
                        //GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] - closest[1]);
                    //Check to see if the tile total different in coordinate is less than the current closest
				/*
				Debug.Log("SR: " + sourceTileRow + " SI: " + sourceTileIndex  + " SWAP:" + nearestPlayer.name.ToString() + " - " + character.name.ToString() + "\n"
					+ "NC: " + newClosestDis + " OC: "+ closestDis + " C:" + x + "\n" + "CSR: " + character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [0] + 
					" CSI:" + character.GetComponent<GenericCharacterScript> ().GetTileOccuping ().GetComponent<GenericEarthScript> ().GetTilePosition () [1]
				);
*/
				if (newClosestDis <= closestDis)
                {
					
                        //Swap the values
					closestDis = newClosestDis;
                    nearestPlayer = character;
                        //closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                        //    GetComponent<GenericEarthScript>().GetTilePosition();
                }
                //}
            }
        }
		//return null;
        return nearestPlayer;
    }

    public GameObject GetTileOccuping()
    {
        return tileOccuping;
    }

    public void SetTileOccuping(GameObject setTo)
    {
        tileOccuping = setTo;
        tileOccuping.GetComponent<GenericEarthScript>().SetOccupingObject(this.gameObject);
        tileOccuping.GetComponent<GenericEarthScript>().SetIsAnEnemyOccupyingThisTile(true);
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public virtual void MoveToTile(GameObject tileMovingTo)
    {
        tileOccuping.GetComponent<GenericEarthScript>().SetOccupingObject(null);
        tileOccuping.GetComponent<GenericEarthScript>().SetIsAnEnemyOccupyingThisTile(false);

        SetTileOccuping(tileMovingTo);

        //get correct position (so tile placement but slightly up so goes to middle of tile)
        Vector3 tempTile = tileMovingTo.transform.position;
        tempTile.z -= 0.01f;
        tempTile.y += (tileMovingTo.gameObject.GetComponent<Renderer>().bounds.size.y / (LevelControlScript.control.GetTileHeightRatio() * 2));

        this.gameObject.transform.position = tempTile;
    }

	public void SetHP(float newHP) {
		hp = newHP;
		//Debug.Log ("Secondary hp" + hp);
		//check if less then 0, and do appropiate actions
		if (hp <= 0) {
			EnemyParentScript.control.EnemyDied ();
			GameControlScript.control.RemoveEnemyFromInGameList (this.gameObject);
			tileOccuping.GetComponent<GenericEarthScript> ().SetOccupingObject (null);
			//need this repeated twice since if this is before the line above, the line above will malfunction, 
			//but if not in else than code malfunction
			TurnControlScript.control.SetEnemySelected(null);

			Destroy (gameObject);
		}
	}

	public float GetHP() {
		return hp;
	}
}
