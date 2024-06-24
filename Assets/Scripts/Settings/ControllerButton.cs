using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    public GameObject settingsMenu;
    void Update()
    {
        // Check if the A button on the right controller is pressed
        if (OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            // Toggle the settings menu
            settingsMenu.SetActive(!settingsMenu.activeSelf);
        }
    }
}
