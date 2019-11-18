using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HapticSystem : MonoBehaviour
{

    public Transform hand;
    public Transform Obj;

    public bool isLeftHand;

    private float distance;
    private bool inObjectRange = false;
    public float farThreshold = 1;
    public float nearThreshold = .2f;

    private float vibeAmt;
    public float dampening = .4f; // lowers the max vibeAmt

    public AudioClip entryAudioClip;
    public AudioClip exitAudioClip;

    private void Start()
    {
        if (hand != null)
        {
            hand = gameObject.GetComponent<Transform>();
        } else { OVRDebugConsole.Log("No Hand Transform"); }
    }

    void Update()
    {
        distance = DistanceBetween(hand.position, Obj.position);

        ObjectInRange();
        CalculateHaptic();

        PlayEnterClip();
        PlayExitClip();

    }

    private void PlayEnterClip()
    {
        if (entryAudioClip != null)
        {
            if (distance < farThreshold && isLeftHand)
            {
                OVRHaptics.LeftChannel.Mix(new OVRHapticsClip(entryAudioClip));
            }
            else if (distance < farThreshold && !isLeftHand)
            {
                OVRHaptics.RightChannel.Mix(new OVRHapticsClip(entryAudioClip));
            }
        }
        else
        {
            OVRDebugConsole.Log("No Haptic Entry Audio Clip");
        }
    }

    private void PlayExitClip()
    {
        if (exitAudioClip != null)
        {
            if (distance < nearThreshold && isLeftHand)
            {
                OVRHaptics.LeftChannel.Mix(new OVRHapticsClip(exitAudioClip));
            }
            else if (distance < nearThreshold && !isLeftHand)
            {
                OVRHaptics.RightChannel.Mix(new OVRHapticsClip(exitAudioClip));
            }
        }
        else
        {
            OVRDebugConsole.Log("No Haptic Exit Audio Clip");
        }
    }

    private OVRInput.Controller HandToggle()
    {
        if (isLeftHand)
        {
            return OVRInput.Controller.LTouch;
        }
        else if (!isLeftHand)
        {
            return OVRInput.Controller.RTouch;
        }
        else
        {
            return OVRInput.Controller.None;
        }
    }

    private void CalculateHaptic()
    {
        if (inObjectRange)
        {
            vibeAmt = Normalize(distance, farThreshold, nearThreshold); // flipped min, max around to get vibe stronger as the object gets closer    
            OVRInput.SetControllerVibration(vibeAmt - dampening, vibeAmt - dampening, HandToggle());
        }
        else if (!inObjectRange)
        {
            vibeAmt = 0;
            OVRInput.SetControllerVibration(0, 0, HandToggle());
        }
        
    }

    private void ObjectInRange()
    {
        if (distance < farThreshold && distance > nearThreshold)
        {
            inObjectRange = true;
            //print("IS in Range");
        }
        else if (distance > farThreshold || distance < nearThreshold)
        {
            inObjectRange = false;
            //print("NOT in Range");
        }
    }

    private float DistanceBetween(Vector3 hand, Vector3 obj)
    {
        return Mathf.Sqrt(  Mathf.Pow(hand.x - obj.x, 2) + 
                            Mathf.Pow(hand.y - obj.y, 2) + 
                            Mathf.Pow(hand.z - obj.z, 2) 
                          );
    }

    private float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}
