using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMission1Runner : MonoBehaviour {
    // Use this for initialization
    private int sideMission1Result;
    public static SideMission1Runner control;

    void Start()
    {
        if (control == null)
        {
            control = this;
            //sideCharactersBool = GameControlScript.control.GetSideMissionChosen();
            //determine character to spawn
        }

    sideMission1Result = SideMissionScript.control.runSideMission1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetSideMission1Result()
    {
        return sideMission1Result;
    }
}
