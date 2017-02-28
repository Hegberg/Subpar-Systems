using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionForSlot : MonoBehaviour
{
    public string Name;
    public string bios = "I am bio";
	public int characterPlace;
    Image image;
    Canvas main;
    Button btn;
    // Use this for initialization
    void Start()
    {


        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnMouseOver);

    }

    // Update is called once per frame
    void Update()
    {

    }

	//full color
	//GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
	//half fade
	//GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);

    private void OnMouseOver()
    {
        //expermental, need to fix, for preventing duplicates
        int selected = -1;
        for (int i = 0; i < GameControlScript.control.GetCharacters().Count; ++i)
        {
            if (GameControlScript.control.GetCharacters()[i].name.ToString().ToLower() == btn.name.ToString().ToLower())
            {
                selected = i;
            }
        }

        //if (GameControlScript.control.Get)

        var UI = GameObject.FindWithTag("UIChar");
        var pict = GameObject.FindWithTag("UIImage");
        Canvas UI1;
        Sprite picture;
        picture = GetComponent<Image>().sprite;
        pict.GetComponent<Image>().sprite = picture;

        GameObject.FindWithTag("name").GetComponent<Text>().text = btn.GetComponent<Button>().name.ToString();
        GameObject.FindWithTag("bio").GetComponent<Text>().text = bios;
        UI1 = UI.GetComponent<Canvas>();
        UI1.enabled = true;

        charselect.control.CharacterSelected(btn.name.ToString().ToLower());
    }

	public int GetCharacterPlace() {
		return characterPlace;
	}
}
