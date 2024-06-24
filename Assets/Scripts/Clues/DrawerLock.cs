using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DrawerLock : OneGrabTranslateTransformer, ITransformer
{


    public delegate void ObjectGrabbed(GameObject source);
    public event ObjectGrabbed onObjectGrabbed;
    public delegate void ObjectMoved(GameObject source);
    public event ObjectMoved onObjectMoved;
    public delegate void ObjectReleased(GameObject source);
    public event ObjectReleased onObjectReleased;

    public bool locked = false;
    private AudioSource audioSource;

    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        //Get the parent GameObject
        parent = transform.parent.gameObject;

        //Get paret's audio source
        audioSource = parent.GetComponent<AudioSource>();
    }

    public new void BeginTransform()
    {
        CheckLock();

        base.BeginTransform();
        onObjectGrabbed?.Invoke(gameObject);
    }

    public new void UpdateTransform()
    {
        if (locked == true)
        {
            return;
        }

        base.UpdateTransform();
        onObjectMoved?.Invoke(gameObject);
    }
    public new void EndTransform()
    {
        base.EndTransform();
        onObjectReleased?.Invoke(gameObject);
    }

    public void CheckLock()
    {
        if (locked)
        {
            //Play the audio source
            audioSource.Play();
            return;
        }
    }
}
