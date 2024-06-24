using UnityEngine;

public class FanBlades : MonoBehaviour
{
    public float maxRotationSpeed = 100f; // The maximum speed at which the blades will rotate
    public float acceleration = 10f; // The acceleration rate
    public float deceleration = 20f; // The deceleration rate

    private float currentRotationSpeed = 0f; // The current speed of the blades
    public bool isOn = false; // State of the fan

    void Update()
    {
        // If the fan is on, gradually increase the rotation speed
        if (isOn && currentRotationSpeed < maxRotationSpeed)
        {
            currentRotationSpeed += acceleration * Time.deltaTime;
            if (currentRotationSpeed > maxRotationSpeed)
            {
                currentRotationSpeed = maxRotationSpeed;
            }
        }
        // If the fan is off, gradually decrease the rotation speed
        else if (!isOn && currentRotationSpeed > 0)
        {
            currentRotationSpeed -= deceleration * Time.deltaTime;
            if (currentRotationSpeed < 0)
            {
                currentRotationSpeed = 0;
            }
        }

        // Rotate the blades
        RotateBlades();
    }

    void RotateBlades()
    {
        // Rotate the blades around the Y axis
        transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
    }

    public void ToggleFan()
    {
        // Toggle the state of the fan
        isOn = !isOn;
    }
}
