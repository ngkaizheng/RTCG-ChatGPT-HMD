using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBook : MonoBehaviour
{
    public GameObject Cover;
    public HingeJoint hinge;
    // Start is called before the first frame update
    void Start()
    {
        hinge = Cover.GetComponent<HingeJoint>();

        hinge.useMotor = false;
    }

    // Update is called once per frame
    public void OpenSesame()
    {
        // hinge.useMotor = true;
        // hinge.useLimits = true;
    }
    public void CloseSesame()
    {
        hinge.useMotor = false;
        // hinge.useLimits = false;
    }
}
