using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSettings : MonoBehaviour
{
    public GameObject[] distanceGrabber;
    // Start is called before the first frame update
    void Start()
    {
        //Coroutine 2 sec
        StartCoroutine(TriggerDistanceGrabRoutine());
    }

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

    private IEnumerator TriggerDistanceGrabRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        TriggerDistanceGrab();
    }
}
