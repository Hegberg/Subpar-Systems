using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonScript : MonoBehaviour {
    Button exit;
    // Use this for initialization
    void Start () {
        exit = GetComponent<Button>();
        exit.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        
            Application.Quit();
        
    }
}
