using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HubConvo : MonoBehaviour
{
    Button go;
    Button go1;
    Button go2;
    string sentence;
    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;
    protected string text = " ";
    int missonlevel = 1;
    public string scene;
    public static HubConvo control;

    // Use this for initialization
    void Start()
    {
        if (control == null)
        {
            control = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //missonlevel = GameControlScript.control.GetLevel();
        //Debug.Log(missonlevel);
    }
    public void Setscene(string scenename)
    {
        scene = scenename;
    }
    public void StartScene()
    {
        if (scene == "Scene1")
        {
            var convo = GameObject.FindGameObjectsWithTag("Characters");
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("HubScene1level1.txt");
            reader = theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            go1 = convo[1].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Geoff").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Terry").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            UI1.GetComponent<Image>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            convo1.GetComponent<Text>().enabled = false;
        }
        if (scene == "Scene2")
        {
            Debug.Log("IAMHERE");
            var convo = GameObject.FindGameObjectsWithTag("Characters");
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("JaiAndAsheChill.txt");
            reader = theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Ashe").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Jai").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
        }
        if (scene == "Level2A")
        {
            Debug.Log("IAMHERE");
            var convo = GameObject.FindGameObjectsWithTag("Characters");
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("RoyVMurphy.txt");
            reader = theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Roy").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Murphy").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
        }
        go.onClick.AddListener(clicked);
        go2.onClick.AddListener(clicked);
        go1.onClick.AddListener(clicked);
    }
    private void clicked()
    {
        var convo = GameObject.FindGameObjectsWithTag("Characters");
        var UI1 = GameObject.FindWithTag("MissionHubConvo2");
        var convo1 = GameObject.FindWithTag("Convo2");
        if (text != null)
        {
            if (reader != null)
            {
                text = reader.ReadLine();
            }
            else
            {
                StartScene();
            }
        }
        if (text == null)
        {
            var scene1 = GameObject.FindWithTag("level1a");
            scene1.GetComponent<Canvas>().enabled = false;
            var scene = GameObject.FindWithTag("ConvoHub");
            scene.GetComponent<Canvas>().enabled = true;
            theSourceFile = null;
            reader = null;
            text = " ";
    //error handle
}
        else if (text.Substring(0, 1) == ("G"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("T"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
        }
        else if (text.Substring(0, 1) == ("A"))
        {
            
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("J"))
        {
   
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
        }
        else if (text.Substring(0, 1) == ("M"))
        {

            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("R"))
        {

            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
        }
    }
}
    // Update is called once per frame


