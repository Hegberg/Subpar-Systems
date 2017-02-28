using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturntoMainMenuScript : MonoBehaviour {
    Button start; 
	// Use this for initialization
	void Start () {
        start = GetComponent<Button>();
        start.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
		GameControlScript.control.ClearChosenCharacters ();
        SceneManager.LoadScene("MainMenu");
    }
}
