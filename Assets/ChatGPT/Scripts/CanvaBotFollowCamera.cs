using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaBotFollowCamera : MonoBehaviour
{
    Transform mainCamera;
    private GameObject parent;
    private GameObject botAvatar;
    private Canvas worldSpaceCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;

        parent = transform.parent.gameObject;

        botAvatar = parent.transform.Find("BotAvatar").gameObject;
        worldSpaceCanvas = parent.transform.GetChild(1).GetComponent<Canvas>();

        if (botAvatar == null)
        {
            Debug.LogError("BotAvatar not found. Please make sure the BotAvatar is a child of the Bot object.");
        }
        if (worldSpaceCanvas == null)
        {
            Debug.LogError("Canvas not found. Please make sure the Canvas is a child of the Bot object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        worldSpaceCanvas.transform.LookAt(mainCamera);

        // Correct the rotation to maintain the fixed x-axis rotation
        Quaternion currentRotation = worldSpaceCanvas.transform.rotation;
        worldSpaceCanvas.transform.rotation = Quaternion.Euler(-15, currentRotation.eulerAngles.y + 180, currentRotation.eulerAngles.z);

        worldSpaceCanvas.transform.position = botAvatar.transform.position + new Vector3(0, 2.25f, 0);
    }
}
