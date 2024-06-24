using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public Renderer fadeScreen;

    public float timeToFade = 1.0f;

    public bool fadeIn = false;
    public bool fadeOut = false;

    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else if (fadeOut)
        {
            FadeOut();
        }
    }

    // Fade in
    public void FadeIn()
    {
        if (fadeScreen.material.color.a < 1)
        {
            Color color = fadeScreen.material.color;
            color.a += timeToFade * Time.deltaTime;
            fadeScreen.material.color = color;
            if (fadeScreen.material.color.a >= 1)
            {
                fadeScreen.material.color = new Color(0, 0, 0, 1);
                fadeIn = false;
            }
        }
    }

    // Fade out
    public void FadeOut()
    {
        if (fadeScreen.material.color.a >= 0)
        {
            Color color = fadeScreen.material.color;
            color.a -= timeToFade * Time.deltaTime;
            fadeScreen.material.color = color;
            if (fadeScreen.material.color.a <= 0)
            {
                fadeScreen.material.color = new Color(0, 0, 0, 0);
                fadeOut = false;
            }
        }
    }


}
