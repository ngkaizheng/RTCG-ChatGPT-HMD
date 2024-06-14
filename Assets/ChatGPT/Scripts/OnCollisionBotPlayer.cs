using System.Collections;
using System.Collections.Generic;
using OpenAI;
using TMPro;
using UnityEngine;

public class OnCollisionBotPlayer : MonoBehaviour
{
    private ChatGPT chatGPT;
    public GameObject worldSpaceCanvas;

    [HideInInspector]
    public bool enterFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        // chatGPT = parent.transform.Find("ChatGPT").GetComponent<ChatGPT>();

        //Find ChatGPT in whole
        chatGPT = FindObjectOfType<ChatGPT>();

        // worldSpaceCanvas = transform.GetChild(1).GetComponent<Canvas>();

        if (chatGPT == null)
        {
            Debug.LogError("ChatGPT component not found on the ChatGPT GameObject.");
        }
        if (worldSpaceCanvas == null)
        {
            Debug.LogError("Canvas not found. Please make sure the Canvas is a child of the Bot object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "DistancePlayer")
            worldSpaceCanvas.gameObject.SetActive(true);

        Debug.Log("Trigger detected");

        if (other.gameObject.tag == "DistancePlayer" && enterFirst == false)
        {
            if (chatGPT != null)
            {
                chatGPT.SendReply();
            }
            else
            {
                Debug.LogError("ChatGPT script not found!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
            worldSpaceCanvas.gameObject.SetActive(false);

        Debug.Log("Trigger ended");
    }

}
