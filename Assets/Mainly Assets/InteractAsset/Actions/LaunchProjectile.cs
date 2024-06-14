using Oculus.Interaction.HandGrab;
using UnityEngine;

/// <summary>
/// Apply forward force to instantiated prefab
/// </summary>
public class LaunchProjectile : MonoBehaviour, IHandGrabUseDelegate
{
    [Tooltip("The projectile that's created")]
    public GameObject projectilePrefab = null;

    [Tooltip("The point that the project is created")]
    public Transform startPoint = null;

    [Tooltip("The speed at which the projectile is launched")]
    public float launchSpeed = 1.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        GameObject newObject = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation);

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            ApplyForce(rigidBody);

        Destroy(newObject, 5f);
    }

    private void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = startPoint.forward * launchSpeed;
        rigidBody.AddForce(force);
    }

    #region input

    public void BeginUse()
    {
        Fire();
        audioSource.Play();
    }

    public void EndUse()
    {
        // Do nothing for now
    }

    public float ComputeUseStrength(float strength)
    {
        // No strength computation needed, always return 1
        return 1f;
    }

    #endregion
}
