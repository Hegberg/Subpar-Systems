using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

    public static GameControlScript control;

    private static List<Object> characters = new List<Object>();
    private static List<bool> chosen = new List<bool>();

	private static List<bool> sideMissionChosen = new List<bool>();
    public static List<bool> team = new List<bool>();

    public Transform character1;
    public Transform character2;
    public Transform character3;
    public Transform character4;
    public Transform character5;
    public Transform character6;
    public Transform character7;
    public Transform character8;
    public Transform character9;
    public Transform character10;
    public Transform character11;
    public Transform character12;

    private static List<GameObject> tiles = new List<GameObject>();

    public Transform earth;
    public Transform water;
    public Transform mountian;
	public Transform voidTile;

    public static List<GameObject> enemies = new List<GameObject>();

    public Transform slime;
	public Transform kiteMonster;
	public Transform fastMonster;
	public Transform tankMonster;


    private int maxCharacters = 4;
    private int selectedCharacters = 0;
    private int selectedSideMissionCharacters = 0;

    //level progression, auto increment on victory
    private int currentLevel = 1;

    private int testLevel = 0;
    private int firstLevel = 1;

	private int surviveToThisTurn = 25;

	private string level1Objective = "Defeat all the enemies";
	private string level2Objective = "Survive ";
	private string level2ObjectiveEnd = " turns";
	private string level3Objective = "Defend the tank";

    //public Transform characterParent;

    private static List<GameObject> characterInGameList = new List<GameObject>();
    private static List<GameObject> enemyInGameList = new List<GameObject>();
    private static List<string> deadCharacterList = new List<string>();


    private List<List<GenericTraitsScript>> allCharacterTraits = new List<List<GenericTraitsScript>>();
    //Characters 1-4 riflemen.
    //Characters 5-7 Grenadiers
    //Characters 8-10 Assaults
    //Characters 11,12 Machinegunners

    //character F27, Taliyah
    private List<GenericTraitsScript> character1InitialTraits = new List<GenericTraitsScript>
    {new RiflemanTrait(), new BacklineCommanderTrait(), new F27GoodWithF25Trait(), new F27BadWithM40Trait()};

    //character M31, Terry
    private List<GenericTraitsScript> character2InitialTraits = new List<GenericTraitsScript>
    {new RiflemanTrait(), new M31GoodWithM29Trait(), new M31MarriedToF32Trait(), new M31FriendM29DeadTrait(), new M31WifeF32DeadTrait()};

    //character F29, sabrina
    private List<GenericTraitsScript> character3InitialTraits = new List<GenericTraitsScript>
    {new RiflemanTrait(), new F29FriendsWithF28(), new BacklineCommanderTrait(), new SleepDeprived()};

    //Character Roy
    private List<GenericTraitsScript> character4InitialTraits = new List<GenericTraitsScript>
    {new RiflemanTrait() };

    //character M40, Geoff
    private List<GenericTraitsScript> character5InitialTraits = new List<GenericTraitsScript>
    { new GrenedierTrait()};

    //character F28, Ashe
    private List<GenericTraitsScript> character6InitialTraits = new List<GenericTraitsScript>
    {new GrenedierTrait(), new F28FriendsWithF29(), new FrontLineCommander()};

    //Character Lt-Col George Murphy
    private List<GenericTraitsScript> character7InitialTraits = new List<GenericTraitsScript>
    { new GrenedierTrait() };


    //character F32, Annie
    private List<GenericTraitsScript> character8InitialTraits = new List<GenericTraitsScript>
    {new AssaultTrait(), new F32GoodWithM41Trait()};

    //character Jai Ono, the beserker.
	private List<GenericTraitsScript> character9InitialTraits = new List<GenericTraitsScript>
    {new AssaultTrait() };

    //character Yuri Sokolov
	private List<GenericTraitsScript> character10InitialTraits = new List<GenericTraitsScript>
    { new AssaultTrait() };

    //Character Larry Winters
    private List<GenericTraitsScript> character11InitialTraits = new List<GenericTraitsScript>
    { new MachineGunTrait() };

    //Character Devi Devai.
    private List<GenericTraitsScript> character12InitialTraits = new List<GenericTraitsScript>
    { new MachineGunTrait() };

    // Use this for initialization
    void Start () {
		if (control == null)
        {
            control = this;
            //add character prefabs
            characters.Add(character1);
            characters.Add(character2);
            characters.Add(character3);
            characters.Add(character4);
            characters.Add(character5);
            characters.Add(character6);
            characters.Add(character7);
            characters.Add(character8);
            characters.Add(character9);
            characters.Add(character10);
            characters.Add(character11);
            characters.Add(character12);

            //add tile prefabs, need to ad walkable tiles first so earth, then everything else right now
            tiles.Add(earth.gameObject);
            tiles.Add(water.gameObject);
            tiles.Add(mountian.gameObject);
			tiles.Add (voidTile.gameObject);

            //add enemy prefabs
            enemies.Add (slime.gameObject);
			enemies.Add (kiteMonster.gameObject);
			enemies.Add (fastMonster.gameObject);
			enemies.Add (tankMonster.gameObject);

			level2Objective = level2Objective + surviveToThisTurn.ToString () + level2ObjectiveEnd;

            //initialize chosen list
            for (int i = 0; i < characters.Count; ++i)
            {
                chosen.Add(false);
				sideMissionChosen.Add (false);
				team.Add (false);
            }

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
		//ClearDeadCharacters ();
        //SaveDeadCharacters();
        //Load();
		ClearTraits();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            
            KillAllEnemies();
        }
    }
	/*
	public void Load() {
		//Debug.Log (deadCharacterList.Count);
		LoadDeadCharacters ();
		LoadCharacterTraits ();
		LoadCurrentLevel ();
	}

	public void Save() {
		//Debug.Log (deadCharacterList.Count);
		SaveDeadCharacters ();
		SaveCharacterTraits ();
		SaveCurrentLevel ();
	}
	*/

	//If need to clear traits at any point
	public void ClearTraits () {
		allCharacterTraits.Clear ();
		allCharacterTraits.Add (character1InitialTraits);
		allCharacterTraits.Add (character2InitialTraits);
		allCharacterTraits.Add (character3InitialTraits);
		allCharacterTraits.Add (character4InitialTraits);
		allCharacterTraits.Add (character5InitialTraits);
		allCharacterTraits.Add (character6InitialTraits);
		allCharacterTraits.Add (character7InitialTraits);
		allCharacterTraits.Add (character8InitialTraits);
		allCharacterTraits.Add (character9InitialTraits);
		allCharacterTraits.Add (character10InitialTraits);
		allCharacterTraits.Add (character11InitialTraits);
		allCharacterTraits.Add (character12InitialTraits);
		InitializeTraits ();
	}
	/*
    //load dead characters
    public void LoadDeadCharacters()
    {
		if (File.Exists (Application.persistentDataPath + "/charactersDead.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/charactersDead.dat", FileMode.Open);
			deadCharacterList = (List<string>)bf.Deserialize (file);
			file.Close ();
		} else {
			//no dead characters so empty list
			deadCharacterList.Clear();
		}
    }

    //save dead characters
    public void SaveDeadCharacters()
    {
        if (File.Exists(Application.persistentDataPath + "/charactersDead.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/charactersDead.dat", FileMode.Open);
            bf.Serialize(file, deadCharacterList);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/charactersDead.dat");
            bf.Serialize(file, deadCharacterList);
            file.Close();
        }
    }

    public void ClearDeadCharacters()
    {
        deadCharacterList.Clear();
		SaveDeadCharacters ();
    }

	//need the character traits to be updatedby the respective character classes upon load and save
	public void LoadCharacterTraits()
	{
		if (File.Exists (Application.persistentDataPath + "/allCharactersTraits.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/allCharactersTraits.dat", FileMode.Open);
			allCharacterTraits = (List<List<GenericTraitsScript>>)bf.Deserialize (file);
			file.Close ();
		} else {
			//need to initialize traits
			allCharacterTraits.Add (character1InitialTraits);
			allCharacterTraits.Add (character2InitialTraits);
			allCharacterTraits.Add (character3InitialTraits);
			allCharacterTraits.Add (character4InitialTraits);
			allCharacterTraits.Add (character5InitialTraits);
			allCharacterTraits.Add (character6InitialTraits);
			allCharacterTraits.Add (character7InitialTraits);
			allCharacterTraits.Add (character8InitialTraits);
			allCharacterTraits.Add (character9InitialTraits);
			allCharacterTraits.Add (character10InitialTraits);
			allCharacterTraits.Add (character11InitialTraits);
			allCharacterTraits.Add (character12InitialTraits);
		}
		InitializeTraits ();
	}

	public void SaveCharacterTraits() {
		if (File.Exists(Application.persistentDataPath + "/allCharactersTraits.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/allCharactersTraits.dat", FileMode.Open);
			bf.Serialize(file, allCharacterTraits);
			file.Close();
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/allCharactersTraits.dat");
			bf.Serialize(file, allCharacterTraits);
			file.Close();
		}
	}
	*/
	public void InitializeTraits() {
		for (int i = 0; i < allCharacterTraits.Count; ++i) {
			for (int j = 0; j < allCharacterTraits [i].Count; ++j) {
				allCharacterTraits [i] [j].InitializeValues ();
			}
		}
	}
	/*
	public void LoadCurrentLevel() {
		if (File.Exists (Application.persistentDataPath + "/currentLevel.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/currentLevel.dat", FileMode.Open);
			currentLevel = (int)bf.Deserialize (file);
			file.Close ();
		} else {
			//if current level hasn't been save it is a new game so level 1
			currentLevel = firstLevel;
		}
	}

	public void SaveCurrentLevel() {
		if (File.Exists(Application.persistentDataPath + "/currentLevel.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/currentLevel.dat", FileMode.Open);
			bf.Serialize(file, currentLevel);
			file.Close();
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/currentLevel.dat");
			bf.Serialize(file, currentLevel);
			file.Close();
		}
	}
	*/

    //character selected in list to join team
    public void SelectCharacter(string nameSelected)
    {
        int selected = -1;
        for (int i = 0; i < characters.Count; ++i)
        {
            if(characters[i].name.ToString().ToLower() == nameSelected)
            {
                selected = i;
            }
        }

        //Debug.Log("selected - " + selected);
        

        //if name not found selected will be -1, and need to check for that before using selected to grab item from a list
        if (selected == -1)
        {
            //do nothing, but prevents other if checks from raising errors
        }

        //if not selected it and only selected less then max then select it
        else if (!chosen[selected] && selectedCharacters < maxCharacters)
        {
			team[selected] = !team[selected];
            chosen[selected] = !chosen[selected];
            selectedCharacters += 1;
        }
        //not selected but have max selected
        else if (!chosen[selected] && selectedCharacters == maxCharacters)
        {
            //replace with proper in game warning
            Debug.Log("Have max already");
        }
        //unselecting character
        else if (chosen[selected])
        {
            Debug.Log("check");
			team[selected] = !team[selected];
            chosen[selected] = !chosen[selected];
            selectedCharacters -= 1;
        }

        //Debug.Log("selected characters = " + selectedCharacters);
    }

	//need to fix, and fix previous choose to check for this choose, and this to check for chosen list, 
	//so side mission and prev characters are not in the same game
	//character selected in list to join side mission team
	public void SelectSideMissionCharacter(string nameSelected)
	{
		int selected = -1;
		for (int i = 0; i < characters.Count; ++i)
		{
			if(characters[i].name.ToString().ToLower() == nameSelected)
			{
				selected = i;
			}
		}

		//Debug.Log("selected - " + selected);


		//if name not found selected will be -1, and need to check for that before using selected to grab item from a list
		if (selected == -1)
		{
			//do nothing, but prevents other if checks from raising errors
		}

		//if not selected it and only selected less then max then select it
		else if (!sideMissionChosen[selected] && selectedSideMissionCharacters < maxCharacters)
		{
			team[selected] = !team[selected];
			sideMissionChosen[selected] = !sideMissionChosen[selected];
            selectedSideMissionCharacters += 1;
		}
		//not selected but have max selected
		else if (!sideMissionChosen[selected] && selectedSideMissionCharacters == maxCharacters)
		{
			//replace with proper in game warning
			//Debug.Log("Have max side already");
		}
		//unselecting character
		else if (sideMissionChosen[selected])
		{
			team[selected] = !team[selected];
			sideMissionChosen[selected] = !sideMissionChosen[selected];
            selectedSideMissionCharacters -= 1;
		}

		//Debug.Log("selected characters = " + selectedCharacters);
	}

    public bool EnoughPlayersSelected()
    {
        return selectedCharacters == maxCharacters;
    }

	//index of character1 is 0, character 2 is 1, etc
	public void SetTraitsOfACharacter(int indexOfCharacter, List<GenericTraitsScript> traitList){
		allCharacterTraits [indexOfCharacter] = traitList;
	}

	//index of character1 is 0, character 2 is 1, etc
	public List<GenericTraitsScript> GetTraitsOfACharacter(int indexOfCharacter) {
		return allCharacterTraits [indexOfCharacter];
	}

    public List<bool> GetTeam()
    {
        return team;
    }

    public List<GameObject> GetTiles()
    {
        return tiles;
    }

    public List<bool> GetChosen()
    {
        return chosen;
    }

    public List<Object> GetCharacters()
    {
        return characters;
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

	public int GetLevel(){
        return currentLevel;
	}

	//set the level to be loaded to be the next level
	public void NextLevel(){
		for (int i = characterInGameList.Count - 1; i >= 0; --i) {
			characterInGameList [i].GetComponent<GenericCharacterScript> ().RemoveFromGame ();
		}
		currentLevel += 1;
        //SaveDeadCharacters();
        characterInGameList.Clear();
        enemyInGameList.Clear();
        ClearChosenCharacters();
        SceneManager.LoadScene("Debriefing");
        //Debug.Log (currentLevel);
    }

    public void FailedLevel()
    {
		for (int i = characterInGameList.Count - 1; i >= 0; --i) {
			characterInGameList [i].GetComponent<GenericCharacterScript> ().RemoveFromGame ();
		}
        //SaveDeadCharacters();
        characterInGameList.Clear();
        enemyInGameList.Clear();
        ClearChosenCharacters();
		SceneManager.LoadScene ("Game Over");
    }

    public void ClearChosenCharacters()
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            chosen[i] = false;
            sideMissionChosen[i] = false;
            team[i] = false;
        }
        selectedCharacters = 0;
		selectedSideMissionCharacters = 0;
    }

    public List<GameObject> GetInGameCharacterList()
    {
        return characterInGameList;
    }

    public List<GameObject> GetInGameEnemyList()
    {
        return enemyInGameList;
    }

    public void AddCharacterToInGameList(GameObject character)
    {
        characterInGameList.Add(character);
    }

    public void AddEnemyToInGameList(GameObject enemy)
    {
        enemyInGameList.Add(enemy);
    }

    public void RemoveCharacterFromInGameList(GameObject character)
    {
        characterInGameList.Remove(character);
    }

    public void RemoveEnemyFromInGameList(GameObject enemy)
    {
        enemyInGameList.Remove(enemy);
    }

	//Stores characters name (ex. M31), not the name of the prefab or gameObject
    public void CharacterDied(GameObject character)
    {
		deadCharacterList.Add(character.GetComponent<GenericCharacterScript>().GetName());
        RemoveCharacterFromInGameList(character);

		for (int i = 0; i < characterInGameList.Count; ++i) {
			characterInGameList [i].GetComponent<GenericCharacterScript> ().GetTraits ();
			for (int j = 0; j < characterInGameList [i].GetComponent<GenericCharacterScript> ().GetTraits ().Count; ++j) {
				characterInGameList [i].GetComponent<GenericCharacterScript> ().GetTraits () [j].AttemptToSetCrazy ();
			}
		}
        //SaveDeadCharacters();
		//FailedLevel ();
    }

	public List<string> GetDeadCharacters(){
		return deadCharacterList;
	}
    
    public List<bool> GetSideMissionChosen()
    {
        return sideMissionChosen;
    }

    public string GetMissionObjective() {
		if (currentLevel == 1) {
			return level1Objective;
		} else if (currentLevel == 2) {
			return level2Objective;
		} else if (currentLevel == 3) {
			return level3Objective;
		}

		return "";
	}

	public int GetSurviveToThisTurn() {
		return surviveToThisTurn;
	}

    public void KillAllEnemies()
    {
        foreach (var enemy in enemyInGameList)
        {
            enemy.GetComponent<GenericEnemyScript>().SetHP(0);
        }
    }
}
