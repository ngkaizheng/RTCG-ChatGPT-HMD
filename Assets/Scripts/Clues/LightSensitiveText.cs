using UnityEngine;
using TMPro;

public class LightSensitiveText : MonoBehaviour
{
    public Light flashlight; // Assign the flashlight's Light component here
    public TextMeshProUGUI hiddenText; // Assign the TextMeshProUGUI component here
    public float maxDistance = 10f; // Max distance for the flashlight to affect the text
    public float maxAngle = 15f; // Max angle (in degrees) for the flashlight to affect the text

    private Collider textCollider;

    private void Start()
    {
        // Get the Box Collider attached to the TextMeshProUGUI GameObject
        textCollider = hiddenText.GetComponent<Collider>();
        if (textCollider == null)
        {
            Debug.LogError("No collider found on the TextMeshProUGUI object. Please add a Box Collider.");
        }
    }

    private void Update()
    {
        if (textCollider == null)
        {
            return; // Exit if no collider is found
        }

        // Perform a raycast from the flashlight
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Check if the raycast hit the text collider
            if (hit.collider != null && hit.collider == textCollider)
            {
                // Calculate the dot product between the flashlight's forward direction and the direction to the text
                Vector3 toText = (textCollider.transform.position - flashlight.transform.position).normalized;
                float dot = Vector3.Dot(flashlight.transform.forward, toText);

                // Map the dot product to an angle
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                // Calculate the alpha based on the angle, within the max angle range
                float alpha = Mathf.Clamp01(1 - (angle / maxAngle));

                // Update the visibility of the text based on the flashlight's position and angle
                hiddenText.color = new Color(hiddenText.color.r, hiddenText.color.g, hiddenText.color.b, alpha);
            }
            else
            {
                // Set the alpha to 0 if the raycast doesn't hit the text
                hiddenText.color = new Color(hiddenText.color.r, hiddenText.color.g, hiddenText.color.b, 0);
            }
        }
        else
        {
            // Set the alpha to 0 if the raycast doesn't hit anything
            hiddenText.color = new Color(hiddenText.color.r, hiddenText.color.g, hiddenText.color.b, 0);
        }
    }
}
