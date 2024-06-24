using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class DoorLockVoice : MonoBehaviour
{
    private Door door;
    private AudioSource audioSource;

    private void Start()
    {
        door = GetComponentInParent<Door>();
        audioSource = GetComponent<AudioSource>();
        if (door == null)
        {
            Debug.LogError("No door found. Please assign the door object.");
        }
    }

    public void OpenLockedDoor()
    {
        if (door.lockDoor && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
