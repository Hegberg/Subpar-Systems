using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charselect : MonoBehaviour {

    public static charselect control;
    Button clicked;
    private string characterSelected;
    private List<Button> buttonClicked = new List<Button>();
    private List<string> charactersAdded = new List<string>();
    Button butselected;

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
        Button go;
       var add = GameObject.FindWithTag("addchar");
       go = add.GetComponent<Button>();
       go.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttselected(Button charac)
    {
        butselected = charac;
   //Debug.Log(clicked.name.ToString().ToLower());

    }

    //expermental need to fix, for changing a already chosen character
    public void CharacterSelected(string character)
    {
        /*
        if (characterSelected.Contains(character))
        {
            //GameControlScript.control.SelectCharacter(character);
        }
        */
        characterSelected = character;
        
    }

    public void OnMouseOver()
    {
        Sprite add;
        var UI1 = GameObject.FindWithTag("UIChar"); 
        var UI = GameObject.FindWithTag("CharSelect");
        var pict = GameObject.FindWithTag("UIImage");
        var names = GameObject.FindWithTag("name");
        name = names.GetComponent<Text>().name;
        add = pict.GetComponent<Image>().sprite;
        UI.GetComponent<Canvas>().enabled = false;
        UI1.GetComponent<Canvas>().enabled = false;

        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = true;
        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = true;
        butselected.GetComponent<Image>().sprite = add;
        if (butselected.GetComponentInChildren<Text>().text != name)
        {
            butselected.GetComponentInChildren<Text>().text = name;
        }
        if (butselected.name.ToString() == "Character5" || butselected.name.ToString() == "Character6") {
            GameControlScript.control.SelectSideMissionCharacter(butselected.GetComponentInChildren<Text>().text.ToString().ToLower());
        }
        else
        {
            GameControlScript.control.SelectCharacter(butselected.GetComponentInChildren<Text>().text.ToString().ToLower());
        }
    }

    public void AddButtonClicked(Button setTo)
    {
        //Debug.Log(setTo);
        buttonClicked.Add(setTo);
    }

    public List<Button> GetButtonsClicked()
    {
        return buttonClicked;
    }

    public void AddCharacterSelected(string setTo)
    {
        charactersAdded.Add(setTo);
    }
}
