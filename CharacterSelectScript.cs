using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectScript : MonoBehaviour {
    // Use this for initialization
    GameObject[] Missions;
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var UI = GameObject.FindWithTag("CharSelect");
            Canvas UI1;
            UI1 = UI.GetComponent<Canvas>();
            UI1.enabled = true;
            GameObject[] Missions = GameObject.FindGameObjectsWithTag("Mission");
            foreach (GameObject i in Missions)
            {
                i.SetActive(false);
            }
            //GameControlScript.control.SelectCharacter(this.gameObject.name.ToString());
        }
    }
}
