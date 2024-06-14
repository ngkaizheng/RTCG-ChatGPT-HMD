using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberPad : MonoBehaviour
{
    // Start is called before the first frame update

    public string Sequence;
    public KeycardSpawner CardSpawner;
    public TextMeshProUGUI InputDisplayText;
    private string m_CurrentEnteredCode = "";

    public GameObject screenDispenser;
    private AudioSource audioSourceScreenDispenser;
    public AudioClip[] audioClipsScreenDispenser;
    void Start()
    {
        audioSourceScreenDispenser = screenDispenser.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPressed(int valuePressed)
    {
        if (InputDisplayText.text == "VALID<br>( ^_^)/" || InputDisplayText.text == "INVALID<br>( ͡°Ɛ ͡°)")
        {
            m_CurrentEnteredCode = "";
        }

        m_CurrentEnteredCode += valuePressed;

        InputDisplayText.text = m_CurrentEnteredCode;
        InputDisplayText.color = Color.black;

        Debug.Log("Button Pressed: " + valuePressed);

        audioSourceScreenDispenser.clip = audioClipsScreenDispenser[0];
        audioSourceScreenDispenser.Play();

        if (m_CurrentEnteredCode.Length == Sequence.Length)
        {
            if (m_CurrentEnteredCode == Sequence)
            {
                // code correct
                InputDisplayText.text = "VALID<br>( ^_^)/";
                InputDisplayText.color = Color.green;
                if (GameObject.Find("Keycard(Clone)") == null)
                    CardSpawner.SpawnKeycard();
                audioSourceScreenDispenser.clip = audioClipsScreenDispenser[1];
                audioSourceScreenDispenser.Play();
            }
            else
            {
                // code incorrect
                m_CurrentEnteredCode = "";
                InputDisplayText.text = "INVALID<br>( ͡°Ɛ ͡°)";
                InputDisplayText.color = Color.red;
                audioSourceScreenDispenser.clip = audioClipsScreenDispenser[2];
                audioSourceScreenDispenser.Play();
            }
        }
    }
}
