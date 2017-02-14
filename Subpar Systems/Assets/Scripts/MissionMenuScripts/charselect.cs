using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charselect : MonoBehaviour {
    public static charselect test;
    Button clicked;
    // Use this for initialization
    void Start () {
        if (test == null)
        {
            test = this;
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
        GameControlScript.control.SelectCharacter(clicked.name.ToString().ToLower());
    }
}
