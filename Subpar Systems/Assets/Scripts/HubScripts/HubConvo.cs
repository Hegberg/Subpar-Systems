using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubConvo : MonoBehaviour {
    Button go;
    string sentence;
    private List<string> convo = new List<string>();
    // Use this for initialization
    void Start () {
        go = GetComponent<Button>();
        go.onClick.AddListener(clicked);
        convo.Add("test");
        convo.Add("test2");
        convo.Add("test3");
        convo.Add("test4");
        convo.Add("test5");
    }
    public void clicked() {
        StartCoroutine(OnMouseOver()); 
}
    // Update is called once per frame
    IEnumerator OnMouseOver()
    {
        var UI1 = GameObject.FindWithTag("MissionHubConvo2");
        var UI2 = GameObject.FindWithTag("MissionHubConvo1");
        var convo2 = GameObject.FindWithTag("Convo1");
        var convo1 = GameObject.FindWithTag("Convo2");
        while (convo.Count != 0){
            if (convo.Count != 0)
            {
                UI1.GetComponent<Image>().enabled = true;
                convo1.GetComponent<Text>().enabled = true;
                sentence = convo[0];
                convo.RemoveAt(0);
                convo1.GetComponent<Text>().text = sentence;
                yield return new WaitForSecondsRealtime(3);//could be long?
                UI1.GetComponent<Image>().enabled = false;
                convo1.GetComponent<Text>().enabled = false;
            }
            if (convo.Count != 0)
            {
                sentence = convo[0];
                convo.RemoveAt(0);
                convo2.GetComponent<Text>().text = sentence;
                UI2.GetComponent<Image>().enabled = true;
                convo2.GetComponent<Text>().enabled = true;
                yield return new WaitForSecondsRealtime(3);//could be long?
                UI2.GetComponent<Image>().enabled = false;
                convo2.GetComponent<Text>().enabled = false;
            }
        }
        //need to add trait adding 
    }

}
