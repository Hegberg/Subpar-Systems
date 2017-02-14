﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour {
    Button start;
    // Use this for initialization
    void Start () {
        start = GetComponent<Button>();
        start.onClick.AddListener(OnMouseOver);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
            SceneManager.LoadScene("MissionMenu");
    }
}
