using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {


	private ShowPanels showPanels;						//Reference to the ShowPanels script used to hide and show UI panels
	private bool isPaused;                              //Boolean to check if the game is paused or not
    GameObject[] scene;
    //Awake is called before Start()
    void Awake()
	{
		//Get a component reference to ShowPanels attached to this object, store in showPanels variable
		showPanels = GetComponent<ShowPanels> ();
        GUI.BringWindowToFront(1);
    }

	// Update is called once per frame
	void Update () {

		//Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
		if (Input.GetButtonDown ("Cancel") && (isPaused == false)) 
		{
			//Call the DoPause function to pause the game
			DoPause();
		} 
		//If the button is pressed and the game is paused and not in main menu
		else if (Input.GetButtonDown ("Cancel") && isPaused) 
		{
			//Call the UnPause function to unpause the game
			UnPause ();
		}
	
	}


	public void DoPause()
	{
		//Set isPaused to true
		isPaused = true;
		//Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;
        //call the ShowPausePanel function of the ShowPanels script
        GUI.BringWindowToFront(1);
        showPanels.ShowPausePanel ();
        GameObject.FindWithTag("pause").GetComponent<Image>().enabled = true;
        transform.FindChild("camera").GetComponent<Camera>().enabled = true;

    }


	public void UnPause()
	{
		//Set isPaused to false
		isPaused = false;
		//Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;
		//call the HidePausePanel function of the ShowPanels script
		showPanels.HidePausePanel ();
        GameObject.FindWithTag("pause").GetComponent<Image>().enabled = false;
        transform.FindChild("camera").GetComponent<Camera>().enabled = false;
    }


}
