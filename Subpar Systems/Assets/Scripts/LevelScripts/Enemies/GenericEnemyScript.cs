using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

    private GameObject tileOccuping;

    private float hp = 100;
    private float attack = 10;
    private float movement = 3;
    private float range = 3;

    private bool isSelected = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), hp + "/" + 100);
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
                        hp -= TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().GetAttack();
                        //set player attacked to true
						TurnControlScript.control.GetPlayerSelected().GetComponent<GenericCharacterScript>().PlayerAttacked(1);
                        isSelected = false;
                        //check if enemy still alive
						if (hp <= 0) {
							EnemyParentScript.control.EnemyDied ();
							GameControlScript.control.RemoveEnemyFromInGameList (this.gameObject);
							tileOccuping.GetComponent<GenericEarthScript> ().SetOccupingObject (null);
							//need this repeated twice since if this is before the line above, the line above will malfunction, 
							//but if not in else than code malfunctions
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

    public void Move()
    {
        //if any players alive, move
        if (GameControlScript.control.GetInGameCharacterList().Count > 0)
        {
            List<List<int>> FloodFillTiles = new List<List<int>>();
            //Return all valid movement tiles

            //broken, not giving tiles in a few cases, breaks nearest tile code below
            Debug.Log("Current tile position is row " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0] + ", Index " + tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1]);
            FloodFillTiles = AStarScript.control.FloodFillWithinRange(LevelControlScript.control.GetAStarMap(),
                LevelControlScript.control.GetAStarMapCost(),
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[0],
                tileOccuping.GetComponent<GenericEarthScript>().GetTilePosition()[1],
                (int)movement);

            Debug.Log("FloodFillTiles Count = " + FloodFillTiles.Count + " with movement range " + (int)movement);
            //Debug.Log(FloodFillTiles.Count + " count " + (int)movement + " movement");
            //Debug.Log("what");

            //Find the closest "player character"
            GameObject nearestPlayer = FindClosestPlayer();

            List<int> closest = new List<int>();

            closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                    GetComponent<GenericEarthScript>().GetTilePosition();

            Debug.Log("Closest Player position is row " + closest[0] + "," + closest[1]);

            List<int> nearestTile = new List<int>();
            int closestTileValue = int.MaxValue;

            List<List<GameObject>> tempMap = LevelControlScript.control.GetAStarMap();

            //find closest tile to that character
            foreach (var elementTile in FloodFillTiles)
            {
                //if(nearestTile == null && tempMap[elementTile[0]][elementTile[1]])
                //{
                //    nearestTile = elementTile;
                //}
                int difference = Mathf.Abs(elementTile[0] - closest[0]) + Mathf.Abs(elementTile[1] - closest[1]);
                //Check to see if the tile total different in coordinate is less than the current closest
                if (difference <= closestTileValue && tempMap[elementTile[0]][elementTile[1]].GetComponent<GenericEarthScript>().GetOccupingObject() == null)
                {
                    //Swap the values
                    Debug.Log("The cloeset Tile currently is " + elementTile[0] + "," + elementTile[1] + " with difference of " + difference);
                    closestTileValue = difference;
                    nearestTile = elementTile;
                }
            }//end find the closest tile to enemy

            //Move the enemy to the tile coordinates
            Debug.Log("END OF TESTING FIRST HALF");
            //Debug.Log(nearestTile.Count);
            //Debug.Log(nearestTile[0]);
            //Debug.Log(nearestTile[1]);

            //this is broken, nearest tile should give a tile, without this check, errors that shouldn't happen get thrown
            if (nearestTile.Count > 0)
            {
                GameObject tile = tempMap[nearestTile[0]][nearestTile[1]];
                MoveToTile(tile);
            }
        }
    }

    public void Attack()
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

            foreach (var character in GameControlScript.control.GetInGameCharacterList())
            {
                //stop from checking against itself if that condition happens, will always happen for [0]
                if (character != nearestPlayer)
                {
                    int difference = Mathf.Abs(character.GetComponent<GenericCharacterScript>().GetTileOccuping().
                        GetComponent<GenericEarthScript>().GetTilePosition()[0] - closest[0]) + Mathf.Abs(character.GetComponent<GenericCharacterScript>().
                        GetTileOccuping().GetComponent<GenericEarthScript>().GetTilePosition()[1] - closest[1]);
                    //Check to see if the tile total different in coordinate is less than the current closest
                    if (difference <= closestCharacterTileValue)
                    {
                        //Debug.Log("swapped = " + nearestPlayer.name.ToString() + " with " + character.name.ToString());
                        //Swap the values
                        closestCharacterTileValue = difference;
                        nearestPlayer = character;
                        closest = nearestPlayer.GetComponent<GenericCharacterScript>().GetTileOccuping().
                            GetComponent<GenericEarthScript>().GetTilePosition();
                    }
                }
            }
        }

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
}
