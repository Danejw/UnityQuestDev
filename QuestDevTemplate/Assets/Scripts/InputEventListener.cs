using UnityEngine;
using UnityEngine.Events;

public class InputEventListener : MonoBehaviour
{
    public UnityEvent OnTriggerDown;
    public UnityEvent OnTriggerHold;
    public UnityEvent OnTriggerUp;

    public UnityEvent OnGripDown;
    public UnityEvent OnGripHold;
    public UnityEvent OnGripUp;


    private void Start()
    {
        // Subscribing the InvokeTriggerDown method to the controller's TriggerDown input event
        RightControllerInput.TriggerDown += InvokeTriggerDown;
        RightControllerInput.TriggerHold += InvokeTriggerHold;
        // Subscribing the InvokeTriggerUp method to the controller's TriggerUp input event
        RightControllerInput.TriggerUp += InvokeTriggerUp;

        // Subscribing the InvokeGripDown method to the controller's GripDown input event
        RightControllerInput.GripDown += InvokeGripDown;
        RightControllerInput.GripHold += InvokeGripHold;
        // Subscribing the InvokeGripUp method to the controller's GripUp input event
        RightControllerInput.GripUp += InvokeGripUp;

    }

    void InvokeTriggerDown()
    {
        if(OnTriggerDown != null)
        {
            OnTriggerDown.Invoke();
        }    
    }

    void InvokeTriggerHold()
    {
        if(OnTriggerHold != null)
        {
            OnTriggerHold.Invoke();
        }
    }

    void InvokeTriggerUp()
    {
        if(OnTriggerUp != null)
        {
            OnTriggerUp.Invoke();
        }
    }


    void InvokeGripDown()
    {
        if (OnGripDown != null)
        {
            OnGripDown.Invoke();
        }
    }

    void InvokeGripHold()
    {
        if (OnGripHold != null)
        {
            OnGripHold.Invoke();
        }
    }

    void InvokeGripUp()
    {
        if (OnGripUp != null)
        {
            OnGripUp.Invoke();
        }
    }

}
