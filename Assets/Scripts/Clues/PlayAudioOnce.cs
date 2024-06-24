using UnityEngine;

public class PlayAudioOnce : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private bool hasPlayed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hasPlayed = false;
    }

    public void TriggerAudio()
    {
        // Check if the audio hasn't been played yet and there are clips in the array
        if (!hasPlayed && audioClips.Length > 0)
        {
            // Choose a random clip from the array or use the first one (if you want random, use Random.Range(0, audioClips.Length))
            AudioClip clipToPlay = audioClips[0];

            // Play the clip
            audioSource.clip = clipToPlay;
            audioSource.Play();

            // Set hasPlayed to true to prevent playing again
            hasPlayed = true;
        }
    }
}
