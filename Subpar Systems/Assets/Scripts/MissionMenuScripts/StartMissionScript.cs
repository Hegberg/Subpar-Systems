using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMissionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameControlScript.control.EnoughPlayersSelected())
            {
                SceneManager.LoadScene("TestLevel");
            }
            else
            {
                //replace with proper warning
                Debug.Log("Not enough players selected to start mission");
            }
        }
    }
}
