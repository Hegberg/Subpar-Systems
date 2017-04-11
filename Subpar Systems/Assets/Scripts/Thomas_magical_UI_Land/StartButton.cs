using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    public AudioSource buttonSound;

    // Use this for initialization
    void Start () {
        buttonSound = GetComponent<AudioSource>();

        foreach (Transform child in transform)
        {

            child.gameObject.SetActive(false);
        }

	}

    void OnMouseEnter()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }


    void OnMouseExit()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        //This works, but scene switches too fast. Deal with later.
        buttonSound.Play();
        
        SceneManager.LoadScene("HubArea");
    }
}
