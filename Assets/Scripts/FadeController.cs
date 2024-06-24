using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FadeController : MonoBehaviour
{
    private FadeInOut fadeInOut;

    public GameObject fadeScreen;
    public TextMeshPro escapeText;
    public TextMeshPro backToMenuText;

    public bool gameScene;
    public GameObject activeEverything;

    void Start()
    {
        fadeInOut = GetComponent<FadeInOut>();

        if (!gameScene)
        {
            StartCoroutine(StartGameRoutine());
        }
        else
        {
            StartCoroutine(StartGameRoutine(2.0f));
        }
    }

    private IEnumerator StartGameRoutine()
    {
        fadeScreen.SetActive(true);
        fadeInOut.fadeOut = true;
        yield return new WaitForSeconds(1.0f);
        fadeScreen.SetActive(false);
    }
    private IEnumerator StartGameRoutine(float timeTofade)
    {
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        activeEverything.SetActive(true);
        yield return new WaitForSeconds(timeTofade);
        fadeInOut.fadeOut = true;
        yield return new WaitForSeconds(1.0f);
        fadeScreen.SetActive(false);
    }



    public void EndGame()
    {
        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine()
    {
        fadeInOut.fadeIn = true;
        yield return new WaitForSeconds(1.0f);

        escapeText.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            escapeText.color = new Color(escapeText.color.r, escapeText.color.g, escapeText.color.b, i);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);

        backToMenuText.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            backToMenuText.color = new Color(backToMenuText.color.r, backToMenuText.color.g, backToMenuText.color.b, i);
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
        // Do something
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {
        fadeScreen.SetActive(true);
        fadeInOut.fadeIn = true;
        yield return new WaitForSeconds(1.0f);

        escapeText.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            escapeText.color = new Color(escapeText.color.r, escapeText.color.g, escapeText.color.b, i);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);

        backToMenuText.gameObject.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            backToMenuText.color = new Color(backToMenuText.color.r, backToMenuText.color.g, backToMenuText.color.b, i);
            yield return null;
        }
        // yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("VRScene");
    }

    public void BackToMainMenu()
    {
        StartCoroutine(BackToMainMenuRoutine());
    }

    private IEnumerator BackToMainMenuRoutine()
    {
        fadeInOut.fadeIn = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainMenu");
    }

}
