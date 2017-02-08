using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControlScript : MonoBehaviour {

    public static GameControlScript control;

    private static List<Object> characters = new List<Object>();
    private static List<bool> chosen = new List<bool>();

    public Transform blue;
    public Transform brown;
    public Transform green;
    public Transform grey;
    public Transform pink;
    public Transform purple;
    public Transform red;
    public Transform yellow;

    private static List<GameObject> tiles = new List<GameObject>();

    public Transform earth;
    public Transform water;
    public Transform mountian;

    public static List<GameObject> enemies = new List<GameObject>();

    public Transform slime;

    private int maxCharacters = 4;
    private int selectedCharacters = 0;

	//level progression
	private int currentLevel = 0;

    //public Transform characterParent;

    private static List<GameObject> characterInGameList = new List<GameObject>();
    private static List<GameObject> enemyInGameList = new List<GameObject>();
    private static List<GameObject> deadCharacterList = new List<GameObject>();

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

        //add character prefabs
        characters.Add(blue);
        characters.Add(brown);
        characters.Add(green);
        characters.Add(grey);
        characters.Add(pink);
        characters.Add(purple);
        characters.Add(red);
        characters.Add(yellow);

        //add tile prefabs, need to ad walkable tiles first so earth, then everything else right now
        tiles.Add(earth.gameObject);
        tiles.Add(water.gameObject);
        tiles.Add(mountian.gameObject);

        //add enemy prefabs
        enemies.Add(slime.gameObject);

        for (int i = 0; i < characters.Capacity; ++i)
        {
            chosen.Add(false);
        }

		DontDestroyOnLoad (this.gameObject);

        LoadDeadCharacters();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //load dead characters
    public void LoadDeadCharacters()
    {
        if (File.Exists(Application.persistentDataPath + "/charactersDead.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/charactersDead.dat", FileMode.Open);
            deadCharacterList = (List<GameObject>)bf.Deserialize(file);
            file.Close();
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

    //character selected in list to join team
    public void SelectCharacter(string nameSelected)
    {
        int selected = -1;
        for (int i = 0; i < characters.Capacity; ++i)
        {
            if(characters[i].name.ToString() == nameSelected)
            {
                selected = i;
            }
        }

        //if name not found selected will be -1, and need to check for that before using selected to grab item from a list
        if (selected == -1)
        {
            //do nothing, but prevents other if checks from raising errors
        }

        //if not selected it and only selected less then max then select it
        else if (!chosen[selected] && selectedCharacters < maxCharacters)
        {
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
            chosen[selected] = !chosen[selected];
            selectedCharacters -= 1;
        }
    }

    public bool EnoughPlayersSelected()
    {
        return selectedCharacters == maxCharacters;
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
		currentLevel += 1;
        SaveDeadCharacters();
        //Debug.Log (currentLevel);
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

    public void CharacterDied(GameObject character)
    {
        deadCharacterList.Add(character);
        RemoveCharacterFromInGameList(character);
        SaveDeadCharacters();
    }
}
