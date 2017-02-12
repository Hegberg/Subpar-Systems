﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float cameraMoveSpeed = 0.25f;

    //http://tinypixel-studios.com/log-2-pixel-perfect-tutorial-for-unity-5
    void Awake()
    {
        //modificatoreSchermo = 1f;
        float ResolutionHeight = Screen.height;
        int currentPixelsToUnits = 16;
        int scale = 2;

        Camera.main.orthographicSize = (1.0f * ResolutionHeight) / 2 / currentPixelsToUnits/ scale;
    }

    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow) &&
            transform.position.x > LevelControlScript.control.GetCameraMinX())
        {
            transform.localPosition += new Vector3(-cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) &&
            transform.position.x < LevelControlScript.control.GetCameraMaxX())
        {
            transform.localPosition += new Vector3(cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) &&
            transform.position.y < LevelControlScript.control.GetCameraMaxY())
        {
            transform.localPosition += new Vector3(0, cameraMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) &&
            transform.position.y > LevelControlScript.control.GetCameraMinY())
        {
            transform.localPosition += new Vector3(0, -cameraMoveSpeed, 0);
        }
    }
}
