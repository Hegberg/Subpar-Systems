using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

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
        SceneManager.LoadScene("Credits");
    }
}

