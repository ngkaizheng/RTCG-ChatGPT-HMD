using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaBotFollowCamera : MonoBehaviour
{
    Transform mainCamera;
    private GameObject parent;
    private GameObject botAvatar;
    public GameObject worldSpaceCanvas;

    public GameObject cylinder;

    // Start is called before the first frame update
    void Start()
    {
        // mainCamera = Camera.main.transform;

        // parent = transform.parent.gameObject;

        // botAvatar = parent.transform.Find("BotAvatar").gameObject;
        // // worldSpaceCanvas = parent.transform.GetChild(1).GetComponent<Canvas>();

        // if (botAvatar == null)
        // {
        //     Debug.LogError("BotAvatar not found. Please make sure the BotAvatar is a child of the Bot object.");
        // }
        // if (worldSpaceCanvas == null)
        // {
        //     Debug.LogError("Canvas not found. Please make sure the Canvas is a child of the Bot object.");
        // }

        mainCamera = Camera.main?.transform;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a Camera tagged as 'MainCamera' in the scene.");
            return;
        }

        parent = transform.parent?.gameObject;

        if (parent == null)
        {
            Debug.LogError("Parent object not found. Please ensure this script is attached to a GameObject with a parent.");
            return;
        }

        botAvatar = parent.transform.Find("BotAvatar")?.gameObject;

        if (botAvatar == null)
        {
            Debug.LogError("BotAvatar not found. Please make sure the BotAvatar is a child of the Bot object.");
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
        if (worldSpaceCanvas != null && cylinder != null && botAvatar != null && mainCamera != null)
        {
            worldSpaceCanvas.transform.LookAt(mainCamera);

            // Correct the rotation to maintain the fixed x-axis rotation
            Quaternion currentRotation = worldSpaceCanvas.transform.rotation;
            worldSpaceCanvas.transform.rotation = Quaternion.Euler(-15, currentRotation.eulerAngles.y + 180, currentRotation.eulerAngles.z);

            worldSpaceCanvas.transform.position = botAvatar.transform.position + new Vector3(0, 2.25f, 0);

            // cylinder.transform.position = worldSpaceCanvas.transform.localPosition + new Vector3(0, 2.293f, -2.464f);
            // cylinder.transform.rotation = worldSpaceCanvas.transform.localRotation;

            // Keep the cylinder always 4 units in front of the worldSpaceCanvas
            Vector3 forwardPosition = worldSpaceCanvas.transform.localPosition + (-worldSpaceCanvas.transform.right * 4.0f);
            // Debug.Log("forwardPosition: " + forwardPosition);
            // Debug.Log("worldSpaceCanvas.transform.localPosition: " + worldSpaceCanvas.transform.localPosition);

            cylinder.transform.localPosition = forwardPosition;

            // Ensure the cylinder faces the same direction as the worldSpaceCanvas
            cylinder.transform.localRotation = worldSpaceCanvas.transform.localRotation;

        }
    }
}
