using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hubnames : MonoBehaviour {
    private string scenename;
    Button Scene;
	// Use this for initialization
	void Start () {
        Scene = GetComponent<Button>();
        Scene.onClick.AddListener(clicked);
	}
	
	// Update is called once per frame
	void clicked () {
        HubConvo.control.Setscene(GetComponent<Button>().name.ToString());
        var scene = GameObject.FindWithTag("ConvoHub");
        scene.GetComponent<Canvas>().enabled = false;
        var scene1 = GameObject.FindWithTag("level1a");
        scene1.GetComponent<Canvas>().enabled = true;
        HubConvo.control.StartScene();
    }
}
