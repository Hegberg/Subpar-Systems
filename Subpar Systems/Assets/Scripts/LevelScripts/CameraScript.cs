using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float cameraMoveSpeed = 0.25f;

	private Vector3 level1Spawn = new Vector3(4,18,0);
	private Vector3 level2Spawn = new Vector3(8,14,0);
	private Vector3 level3Spawn = new Vector3(4,18,0);

    private bool cameraDragging = true;
    private bool cameraDraggingUpDown = true;
    private float dragSpeed = 2;
    private Vector3 dragOrigin;

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
        CameraDrag();
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
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }

        if (cameraDragging)
        {

            if (Input.GetMouseButtonDown(1))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-(pos.x * dragSpeed), 0, 0);

            if (move.x > 0f)
            {
                if (this.transform.position.x < LevelControlScript.control.GetCameraMaxX() )
                {
                    transform.Translate(move, Space.World);
                }
            }
            else
            {
                if (this.transform.position.x > LevelControlScript.control.GetCameraMinX() )
                {
                    transform.Translate(move, Space.World);
                }
            }
        }

        float top = Screen.height * 0.2f;
        float bottom = Screen.height - (Screen.height * 0.2f);

        if (mousePosition.x < top)
        {
            cameraDraggingUpDown = true;
        }
        else if (mousePosition.x > bottom)
        {
            cameraDraggingUpDown = true;
        }

        if (cameraDraggingUpDown)
        {

            if (Input.GetMouseButtonDown(1))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(0, -(pos.y * dragSpeed), 0);

            if (move.y > 0f)
            {
                if (this.transform.position.y < LevelControlScript.control.GetCameraMaxY() )
                {
                    transform.Translate(move, Space.World);
                }
            }
            
            else
            {
                if (this.transform.position.y > LevelControlScript.control.GetCameraMinY() )
                {
                    transform.Translate(move, Space.World);
                }
            }
            
        }
    }
}
