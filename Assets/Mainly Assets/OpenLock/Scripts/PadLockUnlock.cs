using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class PadLockUnlock : MonoBehaviour
{
    public GameObject padLock;
    public Door doorScript;

    //Write the the code onTriggerEnter, if the other is tag "PadLockKey", then the padLock set active to false
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PadLockKey")
        {
            padLock.SetActive(false);
            doorScript.lockDoor = false;
            doorScript.GetComponent<AudioSource>().PlayOneShot(doorScript.doorSound[3]);
        }
    }
}
