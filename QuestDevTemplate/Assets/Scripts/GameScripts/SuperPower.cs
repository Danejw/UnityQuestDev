using UnityEngine;


public class SuperPower : MonoBehaviour
{
    [SerializeField]
    public bool isActive = false;
    public bool isLeftHand = false;

    public Transform firePoint;
    public GameObject objectPrefab;
    public GameObject mussleFlash;
    public AudioSource instatiationSound;

    public bool followFirepoint = false;

    public Vector3 bulletForce = new Vector3(0, 0, 0);
    public Vector3 recoilForce = new Vector3(0, 0, 0);
    public Vector3 recoilTorqForce = new Vector3(0, 0, 0);

    private GameObject superPower;

    private bool gripDown = false;

    public void Update()
    {
        FollowFirePoint();
    }

    public void UseSuperPower()
    {
        if (isActive)
        {
            if (objectPrefab != null)
            {
                if (firePoint != null)
                {
                    if (mussleFlash != null)
                    {
                        GameObject flash = Instantiate(mussleFlash, firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
                        Destroy(flash, 1.5f);
                    }

                    superPower = Instantiate(objectPrefab, firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
                    superPower.GetComponent<Rigidbody>().AddRelativeForce(bulletForce);

                    this.GetComponent<Rigidbody>().AddRelativeForce(recoilForce, ForceMode.Impulse);
                    this.GetComponent<Rigidbody>().AddRelativeTorque(recoilTorqForce, ForceMode.Impulse);

                    Destroy(superPower, 3f);
                }
            }
        }
    }

    public void DestroySuperPower()
    {
        Destroy(superPower);
    }

    private void FollowFirePoint()
    {
        if (followFirepoint)
        {
            if (superPower != null)
            {
                superPower.transform.SetPositionAndRotation(firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
            }
        }
    }

    public void ToggleIsActive()
    {
        if (isActive == true)
        {
            isActive = false;
        }
        else if (isActive == false)
        {
            isActive = true;
        }
    }

    public void StartHaptics()
    {
        if (isActive)
        {
            OVRInput.SetControllerVibration(1, 1, HandToggle());
        }   
    }

    public void StopHaptics()
    {
        if (isActive)
        {
            OVRInput.SetControllerVibration(0, 0, HandToggle());
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

    public void SetGripDownTrue()
    {
        gripDown = true;
    }

    public void SetGripDownFalse()
    {
        gripDown = false;
    }

    // FireStarter specific gesture methods
    public void GripCheckUsePower()
    {
        if (gripDown == true)
        {
            UseSuperPower();
        }
    }

    public void GripCheckStartHaptics()
    {
        if(gripDown == true)
        {
            StartHaptics();
        }
    }

    public void PlaySound()
    {
        if (isActive)
        {
            if (instatiationSound != null)
            {
                instatiationSound.Play();
            }
        }
    }

}
