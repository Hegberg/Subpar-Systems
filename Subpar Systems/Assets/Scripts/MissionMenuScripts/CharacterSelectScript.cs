using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectScript : MonoBehaviour
{

    // Use this for initialization
    public static CharacterSelectScript control;
    public Button UIButt;
    string CharName;
    public bool happened = false;
    GameObject[] Characters1;
    void Start()  {
        UIButt = GetComponent<Button>();
        UIButt.onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mainmenu() //trying to clear chars if we go to mainmenu
    {
		/*
        happened = false;
        for (int i = 0; i < GameControlScript.control.GetTeam().Count; ++i)
        {
            name = GameControlScript.control.GetTeam()[0];
            Debug.Log(name);
            GameControlScript.control.SelectCharacter(name);
        }
        */
    }

    private void clicked()
    {
        if (happened == false)
        {
            OnMouseOver();
            happened = true;
        }
        else
        {
          GameControlScript.control.SelectCharacter(GetComponent<Button>().name.ToString());
          OnMouseOver();
        }
    }

    public void OnMouseOver()
    {
        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = false;
        var UI = GameObject.FindWithTag("CharSelect");
        Canvas UI1;
        UI1 = UI.GetComponent<Canvas>();
        Characters1 = GameObject.FindGameObjectsWithTag("Characters");
        UI1.enabled = true;
        foreach (GameObject character in Characters1)
        {
            
            character.GetComponent<Button>().interactable = true;
            character.GetComponent<CanvasGroup>().alpha = 1;
            CharName = character.GetComponent<Button>().name.ToString().ToLower();
			int characterPlace = char

            for (int i = 0; i < GameControlScript.control.GetTeam().Count; ++i)
            {
                if (GameControlScript.control.GetTeam()[i])
                {
                    character.GetComponent<Button>().interactable = false;
                    character.GetComponent<CanvasGroup>().alpha = 0.5f;
                }

            }
            for (int i = 0; i < GameControlScript.control.GetDeadCharacters().Count; ++i)
            {
                if (GameControlScript.control.GetDeadCharacters()[i].ToLower() == CharName)
                {
                    character.GetComponent<Button>().interactable = false;
                    character.GetComponent<CanvasGroup>().alpha = 0.5f;
                }
            }
            charselect.control.buttselected(UIButt);
        charselect.control.AddButtonClicked(UIButt);
        }
    }
}