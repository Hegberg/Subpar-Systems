using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    public Button startButton;
	// Use this for initialization
	void Start () {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(play);
    }
	void play(){
        SceneManager.LoadScene("MissionMenu");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
