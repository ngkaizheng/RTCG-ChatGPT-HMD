using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSettings : MonoBehaviour
{
    public GameObject[] distanceGrabber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void TriggerButton()
    // {
    //     if (RayInteractor != null)
    //     {
    //         Debug.Log("RayInteractor Length: " + RayInteractor.Length);
    //         for (int i = 0; i < RayInteractor.Length; i++)
    //         {
    //             Debug.Log("Changing RayInteractor" + i + " to " + !RayInteractor[i].activeSelf);
    //             RayInteractor[i].SetActive(!RayInteractor[i].activeSelf);
    //         }
    //     }
    // }

    public void TriggerDistanceGrab()
    {
        if (distanceGrabber != null)
        {
            Debug.Log("DistanceGrabber Length: " + distanceGrabber.Length);
            for (int i = 0; i < distanceGrabber.Length; i++)
            {
                Debug.Log("Changing DistanceGrabber" + i + " to " + !distanceGrabber[i].activeSelf);
                distanceGrabber[i].SetActive(!distanceGrabber[i].activeSelf);
            }
        }

    }
}
