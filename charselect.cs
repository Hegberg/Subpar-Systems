using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charselect : MonoBehaviour {
    public static charselect test;
    public string clicked;
    // Use this for initialization
    void Start () {
       Button go;
       var test = GameObject.FindWithTag("addchar");
       go = test.GetComponent<Button>();
       go.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void buttselected(string charac)
    {
        clicked = charac;
        Debug.Log(clicked);
    }
    public void OnMouseOver()
    {
        Sprite test;
        var UI1 = GameObject.FindWithTag("UIChar"); 
        var UI = GameObject.FindWithTag("CharSelect");
        var pict = GameObject.FindWithTag("UIImage");
        test = pict.GetComponent<Image>().sprite;
        UI.GetComponent<Canvas>().enabled = false;
        UI1.GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = true;
        Debug.Log(clicked);
        GameObject.Find(clicked).GetComponent<Image>().sprite = test;
        GameControlScript.control.SelectCharacter(clicked);
    }
}
