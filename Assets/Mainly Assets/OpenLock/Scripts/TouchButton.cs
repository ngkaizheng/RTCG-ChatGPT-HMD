// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;

// public class TouchButton : XRBaseInteractable
// {
//     // Start is called before the first frame update
//     private Renderer m_renderer = null;
//     private Material originalMaterial = null;
//     public Material hoverMaterial = null;
//     private NumberPad numberPad;
//     private bool isPressed = false;
//     void Start()
//     {
//         m_renderer = GetComponent<Renderer>();
//         originalMaterial = m_renderer.material;

//         numberPad = GameObject.Find("Numberpad").GetComponent<NumberPad>();
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     protected override void OnHoverEntered(HoverEnterEventArgs args)
//     {
//         if(isPressed)
//         {
//             return;
//         }
//         base.OnHoverEntered(args);
//         m_renderer.material = hoverMaterial;
//         Debug.Log("Hover Enter");

//         //Check the last character of the name of the button
//         string buttonName = this.gameObject.name;
//         int buttonValue = int.Parse(buttonName.Substring(buttonName.Length - 1));

//         Debug.Log("Button Value: " + buttonValue);
//         numberPad.ButtonPressed(buttonValue);

//         isPressed = true;

//     }

//     protected override void OnHoverExited(HoverExitEventArgs args)
//     {
//         base.OnHoverExited(args);
//         m_renderer.material = originalMaterial;
//         Debug.Log("Hover Exit");
//         isPressed = false;
//     }

// }
