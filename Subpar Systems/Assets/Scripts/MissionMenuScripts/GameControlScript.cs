using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static List<Object> tiles = new List<Object>();

    public Transform earth;
    public Transform water;
    public Transform mountian;

    private int maxCharacters = 4;
    private int selectedCharacters = 0;

    private float tileWidth = 2.0f;
    private float tileHeight = 2.0f;

    //public Transform characterParent;

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

        //add tile prefabs
        tiles.Add(earth);
        tiles.Add(water);
        tiles.Add(mountian);

        for (int i = 0; i < characters.Capacity; ++i)
        {
            chosen.Add(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

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

    public List<Object> GetTiles()
    {
        return tiles;
    }

    public float GetTileWidth()
    {
        return tileWidth;
    }

    public float GetTileHeight()
    {
        return tileHeight;
    }

    public List<bool> GetChosen()
    {
        return chosen;
    }

    public List<Object> GetCharacters()
    {
        return characters;
    }
}
