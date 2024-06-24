using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperTrigger : MonoBehaviour
{
    [SerializeField] private Collider triggerArea;

    [SerializeField] private bool played = false;

    [SerializeField] private Camera vrCamera; // Reference to the VR camera (headset or controller)
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        Vector3 cameraPositionXZ = new Vector3(vrCamera.transform.position.x, 0f, vrCamera.transform.position.z);
        // Check if the text is in view of the renderCamera and inside the trigger area
        if (IsTextInView() && triggerArea.bounds.Contains(cameraPositionXZ))
        {
            // Play the audio if it's not already playing
            if (!played)
            {
                audioSource.Play();
                played = !played;
            }
        }
    }

    private bool IsTextInView()
    {
        // Example: Check if both player and ChatGPT (Jason) are within camera's view
        if (vrCamera != null)
        {
            Vector3 textScreenPoint = vrCamera.WorldToViewportPoint(transform.position);

            // Check if player is in view
            if (textScreenPoint.z > 0 && textScreenPoint.x > 0 && textScreenPoint.x < 1 &&
                textScreenPoint.y > 0 && textScreenPoint.y < 1)
            {
                Debug.Log("Text is in view.");
            }
            else
            {
                Debug.Log("Text is not in view.");
            }

            // Return true if both are in view
            return textScreenPoint.z > 0 &&
                textScreenPoint.x > 0 && textScreenPoint.x < 1 &&
                textScreenPoint.y > 0 && textScreenPoint.y < 1;
        }

        return false;
    }

}
