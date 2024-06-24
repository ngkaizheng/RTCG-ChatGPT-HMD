using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchControl : MonoBehaviour
{
    private Light[] lights;
    private Material lightMaterial;
    public bool lightsOn = true;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();  // Retrieves all Light components on this GameObject

        //Check if the child(0) name "Glow"
        if (transform.GetChild(0).name == "Glow.001")
        {
            //Get Child(0)'s Material
            lightMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
        }
        if (!lightsOn)
        {
            SwitchLights();
        }
    }

    public void SwitchLights()
    {
        Debug.Log("Lights: " + lights.Length);
        foreach (var light in lights)
        {
            light.enabled = !light.enabled;  // Toggle the enabled state of each light
            lightsOn = light.enabled;
        }
        if (lightMaterial != null)
        {
            if (!lightsOn)
            {
                lightMaterial.DisableKeyword("_EMISSION");
            }
            else
            {
                lightMaterial.EnableKeyword("_EMISSION");
            }
        }
    }
}
