using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchControl : MonoBehaviour
{
    private Light[] lights;
    public bool lightsOn = true;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();  // Retrieves all Light components on this GameObject

        if (!lightsOn)
        {
            SwitchLights();
        }
    }

    public void SwitchLights()
    {
        foreach (var light in lights)
        {
            light.enabled = !light.enabled;  // Toggle the enabled state of each light
        }
    }
}
