using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float cameraMoveSpeed = 0.25f;
	private float cameraZoomSpeed = 2;

	private Vector3 level1Spawn = new Vector3(4,18,0);
	private Vector3 level2Spawn = new Vector3(8,14,0);
	private Vector3 level3Spawn = new Vector3(4,18,0);

    private bool cameraDragging = true;
    private bool cameraDraggingUpDown = true;
    private float dragSpeed = 2;
    private Vector3 dragOrigin;

	private float dist;
	private Vector3 v3OrgMouse;

    //http://tinypixel-studios.com/log-2-pixel-perfect-tutorial-for-unity-5
    void Awake()
    {
		dist = transform.position.y;  // Distance camera is above map

        //modificatoreSchermo = 1f;
        float ResolutionHeight = Screen.height;
        int currentPixelsToUnits = 16;
        int scale = 3;

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

		GameControlScript.control.SetCameraZoom (Camera.main.orthographicSize);

    }

    void Update()
    {
        CameraMove();
        CameraDrag();
		CameraZoom();
    }

	private void CameraZoom() {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 3) {
			Camera.main.orthographicSize--;
			GameControlScript.control.SetCameraZoom (Camera.main.orthographicSize);
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 10) {
			Camera.main.orthographicSize++;
			GameControlScript.control.SetCameraZoom (Camera.main.orthographicSize);
		}
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

    private void CameraDrag()
    {
		//when mouse pressed
		if (Input.GetMouseButtonDown (1)) {
			v3OrgMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
			v3OrgMouse = Camera.main.ScreenToWorldPoint (v3OrgMouse);
		} 
		//when dragging
		else if (Input.GetMouseButton (1)) {
			//getting distance moved
			var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
			v3Pos = Camera.main.ScreenToWorldPoint (v3Pos);
			Vector3 checkVector = transform.position -(v3Pos - v3OrgMouse);
			//set temp and make it have no y effect
			Vector3 tempVector = v3Pos;
			tempVector.y = transform.position.y;
			//variable to do move calculation
			Vector3 affectedXVector = transform.position;
			Vector3 affectedYVector = transform.position;
			Vector3 affectedVector = new Vector3 ();

			//going right
			if (transform.position.x < checkVector.x && 
				transform.position.x < LevelControlScript.control.GetCameraMaxX ()) {
				affectedXVector -= (tempVector - v3OrgMouse);
			}

			//going left
			else if (transform.position.x > checkVector.x && 
				transform.position.x > LevelControlScript.control.GetCameraMinX ()) {
				affectedXVector -= (tempVector - v3OrgMouse);
			}

			//reset temp and make it have no x effect
			tempVector = v3Pos;
			tempVector.x = transform.position.x;

			//going up
			if (transform.position.y < checkVector.y && 
				transform.position.y < LevelControlScript.control.GetCameraMaxY ()) {
				affectedYVector -= (tempVector - v3OrgMouse);
			}

			//going down
			else if (transform.position.y > checkVector.y && 
				transform.position.y > LevelControlScript.control.GetCameraMinY ()) {
				affectedYVector -= (tempVector - v3OrgMouse);
			}

			affectedVector.x = affectedXVector.x;
			affectedVector.y = affectedYVector.y;
			affectedVector.z = transform.position.z;

			transform.position = affectedVector;
		}
    }
}
