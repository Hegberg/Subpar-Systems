using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float cameraMoveSpeed = 0.25f;

	private Vector3 level1Spawn = new Vector3(4,18,0);
	private Vector3 level2Spawn = new Vector3(8,14,0);
	private Vector3 level3Spawn = new Vector3(4,18,0);

    //http://tinypixel-studios.com/log-2-pixel-perfect-tutorial-for-unity-5
    void Awake()
    {
        //modificatoreSchermo = 1f;
        float ResolutionHeight = Screen.height;
        int currentPixelsToUnits = 16;
        int scale = 2;

        Camera.main.orthographicSize = (1.0f * ResolutionHeight) / 2 / currentPixelsToUnits/ scale;

		//Camera.main.transform.localPosition = LevelControlScript.control.GetCameraSpawnPosition();
		//transform.localPosition.y = LevelControlScript.control.GetCameraSpawnPosition().y;

		if (GameControlScript.control.GetLevel () == 1) {
			Camera.main.transform.localPosition += level1Spawn;
		} else if (GameControlScript.control.GetLevel () == 2) {
			Camera.main.transform.localPosition += level2Spawn;
		} else if (GameControlScript.control.GetLevel () == 3) {
			Camera.main.transform.localPosition += level3Spawn;
		}

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
            Camera.main.transform.localPosition += new Vector3(-cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) &&
            transform.position.x < LevelControlScript.control.GetCameraMaxX())
        {
            Camera.main.transform.localPosition += new Vector3(cameraMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) &&
            transform.position.y < LevelControlScript.control.GetCameraMaxY())
        {
            Camera.main.transform.localPosition += new Vector3(0, cameraMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) &&
            transform.position.y > LevelControlScript.control.GetCameraMinY())
        {
            Camera.main.transform.localPosition += new Vector3(0, -cameraMoveSpeed, 0);
        }
    }
}
