using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMissionScript : MonoBehaviour {
    Button start;
    // Use this for initialization
    void Start () {
        start = GetComponent<Button>();
        start.onClick.AddListener(started);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void started()
    {
        if (GameControlScript.control.EnoughPlayersSelected() == false) {
            Debug.Log("Not enough players selected to start mission");
        }
        if (GameControlScript.control.EnoughPlayersSelected())
           {
				if (GameControlScript.control.GetLevel () == 0) {
					SceneManager.LoadScene ("TestLevel");
                    //switch have loadscene be level1 when ==1 etc etc.
				}
                if (GameControlScript.control.GetLevel() == 9)
                {
                    SceneManager.LoadScene("BlankLevel");
                    //switch have loadscene be level1 when ==1 etc etc.
                }
                if (GameControlScript.control.GetLevel() == 1)
                {
                    SceneManager.LoadScene("Level1");
                    //switch have loadscene be level1 when ==1 etc etc.
                }
                if (GameControlScript.control.GetLevel() == 2)
                {
                    SceneManager.LoadScene("Level2");
                    //switch have loadscene be level1 when ==1 etc etc.
                }
                if (GameControlScript.control.GetLevel() == 3)
                {
                    SceneManager.LoadScene("Level3");
                    //switch have loadscene be level1 when ==1 etc etc.
                }
            }   else  {
                //replace with proper warning
             Debug.Log("Not enough players selected to start mission");
            }
       }
}
