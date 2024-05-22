using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBot : MonoBehaviour
{
    private GameObject parent;
    private GameObject botAvatar;
    private Animator botAnimator;
    void Start()
    {

        parent = transform.parent.gameObject;

        botAvatar = parent.transform.Find("BotAvatar").gameObject;
        botAnimator = botAvatar.GetComponent<Animator>();

        if (botAvatar == null)
        {
            Debug.LogError("BotAvatar not found. Please make sure the BotAvatar is a child of the Bot object.");
        }
        if (botAnimator == null)
        {
            Debug.LogError("Animator component not found on the Bot GameObject.");
        }
        botAnimator.SetTrigger("Idle");
    }

    public void BotTalk()
    {
        if (botAnimator != null)
        {
            botAnimator.SetTrigger("TalkTrigger");

            // Set a random value (0 or 1) for the "TalkIndex" parameter
            int randomValue = Random.Range(0, 2); // Random.Range with max exclusive for integers

            botAnimator.SetFloat("Talk", randomValue);

            Debug.Log("Random value: " + randomValue);
        }
    }
}
