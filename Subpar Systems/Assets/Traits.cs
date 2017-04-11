using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Traits : MonoBehaviour {
    Button Trait;
    string current;
    private new List<GenericTraitsScript> skills = new List<GenericTraitsScript>();
    // Use this for initialization
    void Start () {
        Trait = GetComponent<Button>();
        Trait.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
        var pict = GameObject.FindWithTag("UIImage");
        var names = GameObject.FindWithTag("name");
        var bio = GameObject.FindWithTag("bio");
        var pict2 = GameObject.FindWithTag("Circle");
        var bio2 = GameObject.FindWithTag("Traits");
        if (pict.GetComponent<Image>().enabled == true || names.GetComponent<Text>().name != current ) {
            pict.GetComponent<Image>().enabled = false;
            names.GetComponent<Text>().enabled = false;
            pict2.GetComponent<Image>().enabled = false;
            bio.GetComponent<Text>().enabled = false;
            name = names.GetComponent<Text>().name;
            current = names.GetComponent<Text>().name;
            char num = name[name.Length - 1];
            Debug.Log(num);
            int x = Convert.ToInt32(new string(num, 1));
            x -= 1;
            if (name == "Character10")  {
                x = 9;
            }
            if (name == "Character11")
            {
                x = 10;
            }
            if (name == "Character12")
            {
                x = 11;
            }
            Debug.Log(x);
            skills = GameControlScript.control.GetTraitsOfACharacter(x);
            Debug.Log(skills.Count);
            string text = "";
            string trait = "";
            for (int i = 0; i < skills.Count; ++i)
            {
                trait = skills[i].ToString();
                Debug.Log(trait);
                if (trait == "RiflemanTrait")
                {
                    text += "Rifleman: This officer leads a squad of riflemen" + "\r\n" + "\r\n";
                }
                else if (trait == "AssaultTrait")
                {
                    text += "Assault: This officer leads a squad of close-range assault troopers" + "\r\n" + "\r\n";
                }
                else if (trait == "GrenedierTrait")
                {
                    text += "Grenedier: This officer leads a squad of explosive grenadiers." + "\r\n" + "\r\n";
                }
                else if (trait == "MachineGunTrait")
                {
                    text += "Machine Gunner: This officer leads several Machinegun teams." + "\r\n" + "\r\n";
                }
                else if (trait == "BacklineCommanderTrait")
                {
                    text += "Backline Commander: Better to be called a coward at the debriefing than a hero in the obituary." + "\r\n" + "\r\n";
                }
                else if (trait == "FrontLineCommander")
                {
                    text += "Frontline Commander: While leading from the front is inspiring, it can severely lower one’s life expectancy." + "\r\n" + "\r\n";
                }
                else if (trait == "SleepDeprived")
                {
                    text += "Sleep Deprived: You try shooting straight on your 5th night without any sleep." + "\r\n" + "\r\n";
                }
                else
                {
                    text += skills[i] + "\r\n" + "\r\n";
                }
            }
            bio2.GetComponent<Text>().text = text;
            bio2.GetComponent<Text>().enabled = true;
        }
        else
        {
            pict.GetComponent<Image>().enabled = true;
            names.GetComponent<Text>().enabled = true;
            pict2.GetComponent<Image>().enabled = true;
            bio.GetComponent<Text>().enabled = true;
            bio2.GetComponent<Text>().enabled = false;
        }

    }
}
