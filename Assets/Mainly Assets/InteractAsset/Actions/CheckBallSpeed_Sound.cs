using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallSpeed_Sound : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Get the Rigidbody component attached to the GameObject
        rigidBody = GetComponent<Rigidbody>();

        // Ensure both components exist
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }

        if (rigidBody == null)
        {
            Debug.LogError("No Rigidbody component found on this GameObject.");
        }
    }

    // This function is called when a collision happens
    public void CollisionHappen()
    {
        if (rigidBody != null && audioSource != null)
        {
            // Calculate the current velocity magnitude of the GameObject
            float velocity = rigidBody.velocity.magnitude;

            // Adjust the volume of the AudioSource based on the velocity
            // Assuming max velocity corresponds to volume 1 and min to volume 0
            audioSource.volume = Mathf.Clamp01(velocity / 10f); // Adjust the divisor as needed

            Debug.Log("Volume: " + audioSource.volume);

            // Play the audio source
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}