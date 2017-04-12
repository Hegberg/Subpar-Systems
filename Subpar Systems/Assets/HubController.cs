using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubController : MonoBehaviour {
    int missonlevel = 2;
    private List<string> deadpeoples = new List<string>();
    // Use this for initialization
    void Start()
    {
        missonlevel = GameControlScript.control.GetLevel();
        deadpeoples = GameControlScript.control.GetDeadCharacters();
        //Debug.Log(GameControlScript.control.GetDeadCharacters());
        {
            //Debug.Log(missonlevel);
            if (missonlevel == 1)
            {
                var scene = GameObject.FindWithTag("Level 1");
                if (deadpeoples.Contains("Geoff")|| deadpeoples.Contains("Taliyah"))
                {
                    GameObject.FindWithTag("Level1A").GetComponent<Button>().enabled=false;
                    GameObject.FindWithTag("Level1A").GetComponent<Image>().enabled = false;
                }
                if (deadpeoples.Contains("Jai Ono") || deadpeoples.Contains("Ashe"))
                {
                    GameObject.FindWithTag("Level1B").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level1B").GetComponent<Image>().enabled = false;
                }
                if (deadpeoples.Contains("Annie Winters") || deadpeoples.Contains("Terry Winters") || deadpeoples.Contains("Larry Winters"))
                {
                    GameObject.FindWithTag("Level1C").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level1C").GetComponent<Image>().enabled = false;
                }
                scene.GetComponent<Canvas>().enabled = true;
            }
            else if (missonlevel == 2)
            {
                var scene = GameObject.FindWithTag("Level 2");
                scene.GetComponent<Canvas>().enabled = true;
                if (deadpeoples.Contains("Lt-Col George Murphy") || deadpeoples.Contains("Roy LeGaul"))
                {
                    GameObject.FindWithTag("Level2A").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level2A").GetComponent<Image>().enabled = false;
                }
                if (deadpeoples.Contains("Yuri Sokolov") || deadpeoples.Contains("Roy LeGaul"))
                {
                    GameObject.FindWithTag("Level2B").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level2B").GetComponent<Image>().enabled = false;
                }
                if (deadpeoples.Contains("Devi Devai") || deadpeoples.Contains("Jai Ono") || deadpeoples.Contains("Larry Winters"))
                {
                    GameObject.FindWithTag("Level2C").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level2C").GetComponent<Image>().enabled = false;
                }
                if (deadpeoples.Contains("Devi Devai") || deadpeoples.Contains("Ashe") || deadpeoples.Contains("Sabrina"))
                {
                    GameObject.FindWithTag("Level2D").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("Level2D").GetComponent<Image>().enabled = false;
                }
                //activate scenes for 2
            }
            else
            {
                //level 3
            }
        }

        // Update is called once per frame
    }
}
