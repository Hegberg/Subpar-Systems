using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectScript : MonoBehaviour
{
    // Use this for initialization
    public Button UIButt;
     void Start()  {
        UIButt = GetComponent<Button>();
        UIButt.onClick.AddListener(OnMouseOver);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {

        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = false;
        var UI = GameObject.FindWithTag("CharSelect");
        Canvas UI1;
        UI1 = UI.GetComponent<Canvas>();
        UI1.enabled = true;
        charselect.test.buttselected(UIButt);
    }

}