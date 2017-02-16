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
        clicked = charac;
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
        add = pict.GetComponent<Image>().sprite;
        UI.GetComponent<Canvas>().enabled = false;
        UI1.GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = true;
        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = true;
        clicked.GetComponent<Image>().sprite = add;
        //Debug.Log(clicked.name.ToString().ToLower());
        GameControlScript.control.SelectCharacter(characterSelected);
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
