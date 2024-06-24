using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;

using UnityEngine;

public class Door : OneGrabRotateTransformer, ITransformer
{
    public delegate void ObjectGrabbed(GameObject source);
    public event ObjectGrabbed onObjectGrabbed;
    public delegate void ObjectMoved(GameObject source);
    public event ObjectMoved onObjectMoved;
    public delegate void ObjectReleased(GameObject source);
    public event ObjectReleased onObjectReleased;

    [SerializeField] private float initialAngle = 0;
    [SerializeField] private float closeDoorAngle = 0;

    public GameObject doorHandle_1;
    public GameObject doorHandle_2;
    public bool handleAnimation;
    public bool lockDoor;

    public AudioClip[] doorSound;
    public new void Initialize(IGrabbable grabbable)
    {
        base.Initialize(grabbable);
    }
    public new void BeginTransform()
    {
        if (lockDoor == true)
        {
            doorHandle_1.GetComponent<Animation>().Play("Door_Handle_Hold_Anim");
            doorHandle_2.GetComponent<Animation>().Play("Door_Handle2_Hold_Anim");
            GetComponent<AudioSource>().PlayOneShot(doorSound[2]);
            return;
        }

        base.BeginTransform();
        onObjectGrabbed?.Invoke(gameObject);

        if (handleAnimation == true)
        {
            //doorHandle_1 have animation component, so we can play the animation, play the 1st animation
            doorHandle_1.GetComponent<Animation>().Play("Door_Handle_Hold_Anim");
            doorHandle_2.GetComponent<Animation>().Play("Door_Handle2_Hold_Anim");
        }

        if (transform.localEulerAngles.y == initialAngle)
        {
            Debug.Log("Door is opening");
            GetComponent<AudioSource>().PlayOneShot(doorSound[0]);
        }

    }

    public new void UpdateTransform()
    {
        if (lockDoor == true)
        {
            return;
        }

        base.UpdateTransform();
        onObjectMoved?.Invoke(gameObject);
    }

    public new void EndTransform()
    {
        if (lockDoor == true)
        {
            doorHandle_1.GetComponent<Animation>().Play("Door_Handle_Release_Anim");
            doorHandle_2.GetComponent<Animation>().Play("Door_Handle2_Release_Anim");
            return;
        }

        //Parent class does nothing with that method so no need to call it
        onObjectReleased?.Invoke(gameObject);

        if (handleAnimation == true)
        {
            doorHandle_1.GetComponent<Animation>().Play("Door_Handle_Release_Anim");
            doorHandle_2.GetComponent<Animation>().Play("Door_Handle2_Release_Anim");
        }

        //check Y axis rotation of the door, if is initial degree, play the sound and close the door
        if (Mathf.Abs(transform.localEulerAngles.y - initialAngle) < closeDoorAngle)
        {
            GetComponent<AudioSource>().PlayOneShot(doorSound[1]);
            transform.localEulerAngles = new Vector3(0, initialAngle, 0);
        }
    }
}
