using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaFollowCamera : MonoBehaviour
{
    Transform mainCamera;
    public GameObject worldSpaceCanvas;
    public GameObject cylinder;

    // Start is called before the first frame update
    void Start()
    {

        mainCamera = Camera.main?.transform;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a Camera tagged as 'MainCamera' in the scene.");
            return;
        }

        if (worldSpaceCanvas == null)
        {
            Debug.LogError("Canvas not assigned. Please assign the Canvas in the Inspector.");
            return;
        }

        if (cylinder == null)
        {
            Debug.LogError("Cylinder not assigned. Please assign the Cylinder in the Inspector.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (worldSpaceCanvas != null && cylinder != null && mainCamera != null)
        // {
        //     worldSpaceCanvas.transform.LookAt(mainCamera);

        //     // Correct the rotation to maintain the fixed x-axis rotation
        //     Quaternion currentRotation = worldSpaceCanvas.transform.rotation;
        //     worldSpaceCanvas.transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y + 180, currentRotation.eulerAngles.z);

        //     worldSpaceCanvas.transform.position = mainCamera.transform.position + new Vector3(0f, 0f, -2f);

        //     // Keep the cylinder always 4 units in front of the worldSpaceCanvas
        //     Vector3 forwardPosition = worldSpaceCanvas.transform.localPosition + (-worldSpaceCanvas.transform.right * 4.0f);
        //     Debug.Log("forwardPosition: " + forwardPosition);
        //     Debug.Log("worldSpaceCanvas.transform.localPosition: " + worldSpaceCanvas.transform.localPosition);

        //     cylinder.transform.localPosition = forwardPosition;

        //     // Ensure the cylinder faces the same direction as the worldSpaceCanvas
        //     cylinder.transform.localRotation = worldSpaceCanvas.transform.localRotation;

        // }
        if (worldSpaceCanvas != null && cylinder != null && mainCamera != null)
        {
            // Make the worldSpaceCanvas look at the main camera
            worldSpaceCanvas.transform.LookAt(mainCamera);

            // Correct the rotation to maintain the fixed x-axis rotation
            Quaternion currentRotation = worldSpaceCanvas.transform.rotation;
            worldSpaceCanvas.transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y + 180, currentRotation.eulerAngles.z);

            // Set the worldSpaceCanvas position 2 units in front of the main camera
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * 2f;

            // Apply the constraint: limit the maximum y position of worldSpaceCanvas to 2.3
            if (targetPosition.y > 2.3f)
            {
                targetPosition.y = 2.3f;
                worldSpaceCanvas.transform.position = targetPosition;

                // Set the cylinder position 2 units in front of the adjusted worldSpaceCanvas position
                Vector3 cylinderPosition = targetPosition + (-worldSpaceCanvas.transform.forward * 2f);
                cylinder.transform.position = cylinderPosition;

                // Ensure the cylinder faces the same direction as the worldSpaceCanvas
                cylinder.transform.rotation = worldSpaceCanvas.transform.rotation;
            }
            else
            {
                worldSpaceCanvas.transform.position = targetPosition;

                // Set the cylinder position to be the same as mainCamera
                cylinder.transform.position = mainCamera.transform.position;

                // Ensure the cylinder faces the same direction as the worldSpaceCanvas
                cylinder.transform.rotation = worldSpaceCanvas.transform.rotation;
            }
        }
    }
}
