using UnityEngine;
using Oculus.Interaction.HandGrab;

public class TVController : MonoBehaviour, IHandGrabUseDelegate
{
    private PlayQuickSound playQuickSound;
    private ChangeMaterial changeMaterial;
    public PlayVideoTV playVideoTV;
    // Start is called before the first frame update
    void Start()
    {
        playQuickSound = GetComponent<PlayQuickSound>();
        changeMaterial = GetComponentInChildren<ChangeMaterial>();
        // playVideoTV = GetComponent<PlayVideoTV>();
    }


    #region input

    public void BeginUse()
    {
        playQuickSound.Play();
        changeMaterial.SetOtherMaterial();
        playVideoTV.TogglePlayPause();
    }

    public void EndUse()
    {
        changeMaterial.SetOriginalMaterial();
    }

    public float ComputeUseStrength(float strength)
    {
        // No strength computation needed, always return 1
        return 1f;
    }

    #endregion
}
