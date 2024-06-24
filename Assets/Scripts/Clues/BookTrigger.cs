using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bookCover;

    [SerializeField] private bool played = false;

    [SerializeField] private Camera vrCamera; // Reference to the VR camera (headset or controller)
    private AudioSource audioSource; // Reference to the AudioSource component

    // public GameObject spherePrefab; // Drag a small sphere prefab here in the Inspector
    // private GameObject currentSphere; // Track the current sphere instance

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // void Update()
    // {
    //     // Check if the VR camera (headset or controller) is available
    //     if (vrCamera != null)
    //     {
    //         // Raycast from the VR camera forward direction
    //         Ray ray = new Ray(vrCamera.position, vrCamera.forward);
    //         RaycastHit hit;

    //         Debug.Log("Ray Position: " + ray.origin);
    //         Debug.Log("Ray Direction: " + ray.direction);

    //         // Perform the raycast
    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             // Check if the ray hits this object
    //             if (hit.collider.gameObject == gameObject)
    //             {
    //                 // Play the AudioSource if it's not already playing
    //                 if (!played)
    //                 {
    //                     audioSource.Play();
    //                     played = !played;
    //                 }
    //             }
    //         }

    //         // Instantiate or move the sphere to the raycast hit point
    //         Vector3 spherePosition = ray.GetPoint(3f); // Adjust the distance of the sphere from the camera
    //         if (currentSphere == null)
    //         {
    //             currentSphere = Instantiate(spherePrefab, spherePosition, Quaternion.identity);
    //         }
    //         else
    //         {
    //             currentSphere.transform.position = spherePosition;
    //         }
    //     }
    //     else
    //     {
    //         // Destroy the sphere if VR camera is not available
    //         if (currentSphere != null)
    //         {
    //             Destroy(currentSphere);
    //             currentSphere = null;
    //         }
    //     }
    // }

    private void Update()
    {
        Debug.Log("Book Cover " + bookCover);
        Debug.Log("Book Cover Angle: " + bookCover.transform.eulerAngles.z);
        // Check if the text is in view of the renderCamera and bookcover angle < -110
        if (IsTextInView() && bookCover.transform.eulerAngles.z < 280 && bookCover.transform.eulerAngles.z >= 180)
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
