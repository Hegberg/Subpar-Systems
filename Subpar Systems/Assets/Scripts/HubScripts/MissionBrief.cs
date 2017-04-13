using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionBrief : MonoBehaviour {
    Button brief;
    // Use this for initialization
    void Start () {
        brief = GetComponent<Button>();
        brief.onClick.AddListener(clicked);
    }
	
	// Update is called once per frame
	void clicked () {
        HubConvo.control.Garbage();
        SceneManager.LoadScene("MissionMenu2.0");
    }
}
