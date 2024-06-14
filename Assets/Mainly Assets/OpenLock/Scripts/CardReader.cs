using System.Collections;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    private GameObject keyCard;
    private Transform keyCardTransform;
    private Vector3 hoverEntry;
    private bool swipeIsValid;

    public Material redMaterial;
    public Material greenMaterial;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public Door doorScript;
    [SerializeField] private float AllowedUprightErrorRange = 0.2f;

    private bool isHovering = false;
    private bool isRedEmissionCoroutineRunning = false;
    private bool isGreenEmissionCoroutineRunning = false;

    private float hoverStartTime;

    // Start is called before the first frame update
    void Start()
    {
        keyCard = GameObject.Find("Keycard(Clone)");
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (isHovering && keyCardTransform != null)
        {
            Vector3 keycardUp = keyCardTransform.forward;
            float dot = Vector3.Dot(keycardUp, Vector3.up);

            Debug.Log("Dot: " + dot);

            // Check if the keycard is upright, if not, the swipe is invalid
            if (!(dot < -1 + AllowedUprightErrorRange || dot > 1 - AllowedUprightErrorRange))
            {
                swipeIsValid = false;

                if (!isRedEmissionCoroutineRunning)
                {
                    redMaterial.EnableKeyword("_EMISSION");
                    greenMaterial.DisableKeyword("_EMISSION");

                    // Play fail sound if it's not already playing
                    if (!audioSource.isPlaying || (audioSource.isPlaying && audioSource.clip != audioClips[0]))
                    {
                        audioSource.volume = 0.15f;
                        audioSource.PlayOneShot(audioClips[0]); //Fail sound
                        audioSource.clip = audioClips[0];
                    }

                    StartCoroutine(DisableRedEmissionAfterDelay());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Keycard(Clone)")
        {
            Debug.Log("Hover Enter");
            isHovering = true;
            keyCardTransform = other.transform;
            hoverEntry = keyCardTransform.position; // Entry position of swipe
            swipeIsValid = true;
            hoverStartTime = Time.time; // Start time of swipe
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Keycard(Clone)")
        {
            isHovering = false;

            Vector3 entryToExit = keyCardTransform.position - hoverEntry; //Vector from entry to exit
            float swipeDuration = Time.time - hoverStartTime; //Time taken to swipe

            Debug.Log("keyCardTransform: " + keyCardTransform.position);
            Debug.Log("hoverEntry: " + hoverEntry);
            Debug.Log("entryToExit: " + entryToExit.y);

            if (swipeIsValid && entryToExit.y < -0.15f && swipeDuration <= 0.5f)
            {
                if (doorScript.lockDoor)
                {
                    doorScript.lockDoor = false;
                    doorScript.GetComponent<AudioSource>().PlayOneShot(doorScript.doorSound[3]);
                }
                Debug.Log("Swipe Down");

                if (!isGreenEmissionCoroutineRunning)
                {
                    redMaterial.DisableKeyword("_EMISSION");
                    greenMaterial.EnableKeyword("_EMISSION");

                    // Play fail sound if it's not already playing
                    if (!audioSource.isPlaying || (audioSource.isPlaying && audioSource.clip != audioClips[1]))
                    {
                        audioSource.volume = 0.6f;
                        audioSource.PlayOneShot(audioClips[1]); //Fail sound
                        audioSource.clip = audioClips[1];
                    }

                    StartCoroutine(DisableGreenEmissionAfterDelay());
                }
            }
            else
            {
                if (!isRedEmissionCoroutineRunning)
                {
                    redMaterial.EnableKeyword("_EMISSION");
                    greenMaterial.DisableKeyword("_EMISSION");

                    // Play fail sound if it's not already playing
                    if (!audioSource.isPlaying || (audioSource.isPlaying && audioSource.clip != audioClips[0]))
                    {
                        audioSource.volume = 0.15f;
                        audioSource.PlayOneShot(audioClips[0]); //Fail sound
                        audioSource.clip = audioClips[0];
                    }

                    StartCoroutine(DisableRedEmissionAfterDelay());
                }
            }

            keyCardTransform = null;
        }
    }

    private IEnumerator DisableRedEmissionAfterDelay()
    {
        isRedEmissionCoroutineRunning = true;
        yield return new WaitForSeconds(2);
        redMaterial.DisableKeyword("_EMISSION");
        isRedEmissionCoroutineRunning = false;

    }

    private IEnumerator DisableGreenEmissionAfterDelay()
    {
        isGreenEmissionCoroutineRunning = true;
        yield return new WaitForSeconds(2);
        greenMaterial.DisableKeyword("_EMISSION");
        isGreenEmissionCoroutineRunning = false;
    }
}
