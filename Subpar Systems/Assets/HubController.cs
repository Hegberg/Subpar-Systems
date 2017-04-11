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
        {
            //Debug.Log(missonlevel);
            if (missonlevel == 1)
            {
                var scene = GameObject.FindWithTag("Level 1");
                if (deadpeoples.Contains("Geoff")|| deadpeoples.Contains("Taliyah"))
                {
                    GameObject.FindWithTag("Level1A").GetComponent<Button>().enabled=false;
                    //GameObject.FindWithTag("Level1A").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                }
                if (deadpeoples.Contains("Jai Ono") || deadpeoples.Contains("Ashe"))
                {
                    GameObject.FindWithTag("Level1B").GetComponent<Button>().enabled = false;
                    //GameObject.FindWithTag("Level1B").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
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
                    //GameObject.FindWithTag("Level2A").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
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
