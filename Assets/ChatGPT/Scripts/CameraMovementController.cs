using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //Move camera based on current cameara view
            mainCamera.transform.localPosition += mainCamera.transform.forward * 0.1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.localPosition -= mainCamera.transform.forward * 0.1f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.localPosition -= mainCamera.transform.right * 0.1f;
        }
        if(Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.localPosition += mainCamera.transform.right * 0.1f;
        }

        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            mainCamera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            // mainCamera.transform.Rotate(-0, 0, 0);
        }

        // //For up and down movement
        // if(Input.GetAxis("Mouse Y") != 0)
        // {
        //     mainCamera.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        // }

        //Lock cursor
        // if(Input.GetKeyDown(KeyCode.G))
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        // }
        // else
        // {
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        // }

    }
}
