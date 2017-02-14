using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Geoff : MonoBehaviour
{
    public string Name;
    public string bios = "I am bio";
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
    private void OnMouseOver()
    {
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

    }
}