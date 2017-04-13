using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HubConvo : MonoBehaviour
{
    Button go;
    Button go1;
    Button go2;
    Button go3;
    string sentence;
    protected FileInfo theSourceFile = null;
    protected FileInfo theSourceFile0 = null;
    protected FileInfo theSourceFile1 = null;
    protected StreamReader reader = null;
    protected StreamReader reader1 = null;
    protected StreamReader reader2 = null;
    protected StreamReader reader3 = null;
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

        var convo = GameObject.FindGameObjectsWithTag("Characters");
        convo[2].GetComponent<Image>().enabled = false;
        
        if (scene == "Scene1")
        {

            GameObject.FindWithTag("Level1A").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level1A").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("HubScene1level1.txt");
            reader= theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            go1 = convo[1].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Geoff").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Taliyah").GetComponent<Image>().sprite;
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponent<Image>().sprite = sprite;
            UI1.GetComponent<Image>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            convo1.GetComponent<Text>().enabled = false;
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Scene2")
        {
            GameObject.FindWithTag("Level1B").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level1B").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile0 = new FileInfo("JaiAndAsheChill.txt");
            reader = theSourceFile0.OpenText();
            go = convo[2].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Ashe").GetComponent<Image>().sprite;
            convo[2].GetComponent<Image>().sprite = sprite;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
            sprite = GameObject.FindWithTag("Jai").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
        }
        if (scene == "Scene3")
        {
            Debug.Log("IAMHERE2");

            GameObject.FindWithTag("Level1C").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level1C").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile1 = new FileInfo("TerryLarryAnnie.txt");
            reader = theSourceFile1.OpenText();
            go = convo[0].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Annie").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Terry").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Larry").GetComponent<Image>().sprite;
            convo[2].GetComponent<Image>().sprite = sprite;
            convo[2].GetComponent<Image>().enabled = true;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            go3 = convo[2].GetComponent<Button>();
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Level3A")
        {
            GameObject.FindWithTag("Level3A").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level3A").GetComponent<Image>().enabled = false;
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
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Level3B")
        {
            Debug.Log("IAMHERE");
            GameObject.FindWithTag("Level3B").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level3B").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("RoyVYuri.txt");
            reader = theSourceFile.OpenText();
            go = convo[1].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Roy").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Yuri").GetComponent<Image>().sprite;
            convo[2].GetComponent<Image>().sprite = sprite;
            go1 = convo[2].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Level2C")
        {
            GameObject.FindWithTag("Level2C").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level2C").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("DeviJaiLarry.txt");
            reader = theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Devi").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Jai").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Larry").GetComponent<Image>().sprite;
            convo[2].GetComponent<Image>().sprite = sprite;
            convo[2].GetComponent<Image>().enabled = true;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            go3 = convo[2].GetComponent<Button>();
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Level3D")
        {
            GameObject.FindWithTag("Level3D").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level3D").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("AsheSabrinaDevi.txt");
            reader = theSourceFile.OpenText();
            go = convo[0].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Devi").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Sabrina").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Ashe").GetComponent<Image>().sprite;
            convo[2].GetComponent<Image>().sprite = sprite;
            convo[2].GetComponent<Image>().enabled = true;
            go1 = convo[1].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            go3 = convo[2].GetComponent<Button>();
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        if (scene == "Level3E")
        {
            Debug.Log("IAMHERE");
            GameObject.FindWithTag("Level3E").GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Level3E").GetComponent<Image>().enabled = false;
            var UI1 = GameObject.FindWithTag("MissionHubConvo2");
            var convo1 = GameObject.FindWithTag("Convo2");
            theSourceFile = new FileInfo("AnnieShaken.txt");
            reader = theSourceFile.OpenText();
            go = convo[1].GetComponent<Button>();
            Sprite sprite;
            sprite = GameObject.FindWithTag("Annie").GetComponent<Image>().sprite;
            convo[0].GetComponent<Image>().sprite = sprite;
            sprite = GameObject.FindWithTag("Terry").GetComponent<Image>().sprite;
            convo[1].GetComponent<Image>().sprite = sprite;
            go1 = convo[2].GetComponent<Button>();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            go2 = UI1.GetComponent<Button>();
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
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
            Debug.Log("TEST");
            if (reader != null)
            {
                text = reader.ReadLine();
                Debug.Log(text);
            }
        }
        if (text == null)
        {
            Debug.Log("TESTING");
            var scene1 = GameObject.FindWithTag("level1a");
            scene1.GetComponent<Canvas>().enabled = false;
            var scene = GameObject.FindWithTag("ConvoHub");
            scene.GetComponent<Canvas>().enabled = true;
            text = " ";
            theSourceFile = null;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
            go.onClick.RemoveAllListeners();
            go1.onClick.RemoveAllListeners();
            go2.onClick.RemoveAllListeners();
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
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("T"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
        }
        else if (text.Substring(0, 1) == ("J"))
        {
   
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
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
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("R"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("L"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = true;
        }
        else if (text.Substring(0, 1) == ("Y"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = true;
        }
        else if (text.Substring(0, 1) == ("D"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 1) == ("S"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = false;
            convo[1].GetComponentInChildren<Image>().enabled = true;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 2) == ("A:"))
        {

            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[2].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[0].GetComponentInChildren<Image>().enabled = false;
        }
        else if (text.Substring(0, 2) == ("An"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(3);
            convo1.GetComponent<Text>().text = sentence;
            //maybe change to spotlight?
            convo[0].GetComponentInChildren<Image>().enabled = true;
            convo[1].GetComponentInChildren<Image>().enabled = false;
            convo[2].GetComponentInChildren<Image>().enabled = false;
        }
    }
}

    // Update is called once per frame


