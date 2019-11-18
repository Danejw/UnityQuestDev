using UnityEngine;

public class RightControllerInput : MonoBehaviour
{

    // Creating events to Subcribe to
    public delegate void OnTriggerDown();
    public delegate void OnTriggerHold();
    public delegate void OnTriggerUp();

    public delegate void OnGripDown();
    public delegate void OnGripHold();
    public delegate void OnGripUp();

    // Creating events to send thru a Subcription
    public static event OnTriggerDown TriggerDown;
    public static event OnTriggerHold TriggerHold;
    public static event OnTriggerUp TriggerUp;

    public static event OnGripDown GripDown;
    public static event OnGripHold GripHold;
    public static event OnGripUp GripUp;

    private bool triggerHold = false;
    private bool gripHold = false;

    void Update()
    {
        // If the right controller trigger is pressed, invoke the TriggerDown event
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space))
        {
            if(TriggerDown != null)
            {
                triggerHold = true;
                TriggerDown.Invoke();
            }
        }

        if (triggerHold)
        {
            TriggerHold.Invoke();
        }

        // If the right controller trigger is let go, invoke the TriggerUp event
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
        {
            if(TriggerUp != null)
            {
                triggerHold = false;
                TriggerUp.Invoke();
            }
        }

        // If the right controller grip is pressed, invoke the GripDown event
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown("g"))
        {
            if(GripDown != null)
            {
                gripHold = true;
                GripDown.Invoke();
            }
        }

        if (gripHold)
        {
            GripHold.Invoke();
        }

        // If the right controller's grip is released, invoke the GripUp event
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyUp("g"))
        {
            if(GripUp != null)
            {
                gripHold = false;
                GripUp.Invoke();
            }
        }



    }
}
