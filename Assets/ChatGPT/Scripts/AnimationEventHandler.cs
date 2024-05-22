using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : StateMachineBehaviour
{
    private float initialValue;
    private float targetValue;
    private float elapsedTime;
    private float transitionDuration = 1.0f; // Duration of the transition

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set the initial and target values
        initialValue = animator.GetFloat("IdleBlend");
        // Choose the target value based on probability
        float random = Random.value; // Random value between 0 and 1
        if (random < 0.8f)
        {
            targetValue = 0.0f; // 80% probability
        }
        else
        {
            targetValue = 1.0f; // 20% probability
        }
        elapsedTime = 0f; // Reset elapsed time

        Debug.Log("OnStateEnter: Target random value: " + targetValue);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(initialValue, targetValue, elapsedTime / transitionDuration);
            animator.SetFloat("IdleBlend", newValue);
        }
        else
        {
            // Ensure the target value is set at the end of the transition
            animator.SetFloat("IdleBlend", targetValue);
        }
    }

}
