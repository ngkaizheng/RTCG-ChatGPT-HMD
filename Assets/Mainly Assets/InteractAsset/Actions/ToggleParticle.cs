using Oculus.Interaction.HandGrab;

using UnityEngine;

public class ToggleParticle : MonoBehaviour, IHandGrabUseDelegate
{
    [SerializeField] private ParticleSystem[] particleSystems;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get all particle systems in children
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        // Deactive gamObject
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Play()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
            audioSource.Play();
        }
    }

    public void Stop()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(false);
            ps.Stop();
        }
    }

    #region input

    public void BeginUse()
    {
        Play();
    }

    public void EndUse()
    {
        Stop();
    }

    public float ComputeUseStrength(float strength)
    {
        // No strength computation needed, always return 1
        return 1f;
    }

    #endregion
}
