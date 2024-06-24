using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePlacement : MonoBehaviour
{
    [SerializeField] private bool isOnMat = false;
    private AudioSource otherAudioSource;
    private AudioSource selfAudioSource;
    public GameObject itemToOpen;
    private GameObject drawerLockedObject;
    private DrawerLock drawerLock;

    private ToggleParticle toggleParticle;

    private void Start()
    {
        if (itemToOpen != null)
        {
            otherAudioSource = itemToOpen.GetComponent<AudioSource>();
            if (otherAudioSource == null)
            {
                Debug.LogError("No AudioSource component found on itemToOpen.");
            }
            drawerLockedObject = itemToOpen.transform.GetChild(2).gameObject;
            drawerLock = drawerLockedObject.GetComponent<DrawerLock>();
        }
        else
        {
            Debug.LogError("Item to open is not assigned.");
        }
        selfAudioSource = GetComponent<AudioSource>();

        toggleParticle = GetComponent<ToggleParticle>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CandleMat"))
        {
            isOnMat = true;
            if (toggleParticle.particleOn)
            {
                selfAudioSource.Play();
                CheckAllCandlesOnMat();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CandleMat"))
        {
            isOnMat = false;
        }
    }

    public bool IsOnMat()
    {
        return isOnMat;
    }

    private void CheckAllCandlesOnMat()
    {
        // Find the Tag "Candle" in the same hierarchy
        GameObject[] candles = GameObject.FindGameObjectsWithTag("Candle");

        // List all candles
        Debug.Log("Candles found: " + candles.Length);

        // Check if all candles are on the mat
        foreach (GameObject candle in candles)
        {
            CandlePlacement candlePlacement = candle.GetComponent<CandlePlacement>();
            if (candlePlacement == null)
            {
                Debug.LogError("CandlePlacement component is missing on candle: " + candle.name);
                return;
            }

            if (!candlePlacement.IsOnMat())
            {
                return;
            }
        }

        // If all candles are on the mat, play the audio
        if (otherAudioSource != null && !otherAudioSource.isPlaying)
        {
            Vector3 drawerLockedObjectPosition = drawerLockedObject.transform.position;
            drawerLockedObjectPosition.z += 0.2f;
            drawerLock.locked = false;
            otherAudioSource.Play();
        }

    }
}
