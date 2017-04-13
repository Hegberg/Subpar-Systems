using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubButtonScript : MonoBehaviour
{
    Button brief;
    // Use this for initialization
    void Start()
    {
        brief = GetComponent<Button>();
        brief.onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void clicked()
    {
        //4 since updates to 3 after 2nd level beat
        if (GameControlScript.control.GetLevel() >= 4)
        {
            SceneManager.LoadScene("Victory");
        } else
        {
            SceneManager.LoadScene("HubArea");
        }
    }
}
