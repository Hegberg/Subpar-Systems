using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitOnClick : MonoBehaviour {
    public Button stop;
    void Start() {
        Button btn = stop.GetComponent<Button>();
        btn.onClick.AddListener(end);
    }
    void end(){
        Application.Quit ();
    }
}
