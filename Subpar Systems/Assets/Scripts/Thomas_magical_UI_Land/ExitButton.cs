using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

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
        Application.Quit();
    }
}
