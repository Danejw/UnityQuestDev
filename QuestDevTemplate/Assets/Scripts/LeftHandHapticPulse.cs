using UnityEngine;

public class LeftHandHapticPulse : MonoBehaviour
{
    OVRHapticsClip pulse;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        pulse = new OVRHapticsClip(audioClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            OVRHaptics.LeftChannel.Mix(pulse);
        }
    }
}
