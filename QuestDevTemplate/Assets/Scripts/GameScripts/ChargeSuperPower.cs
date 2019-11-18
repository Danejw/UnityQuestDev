using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSuperPower : MonoBehaviour
{
    [SerializeField]
    public bool isActive = false;
    public bool isLeftHand = false;

    public Transform chargePoint;
    public Transform firePoint;
    public GameObject objectPrefab;

    public GameObject chargeEffect;
    public AudioSource chargeSound;

    public GameObject mussleFlash;
    public AudioSource instatiationSound;

    public bool followFirepoint = true;

    public Vector3 bulletForce = new Vector3(0, 0, 2000);

    private GameObject superPower;
    private GameObject charge;

    private float startScale = .5f;
    private float maxScale = 3;
    private float upScaler;

    private float holdTime = 0;

    //private LinkedList<Vector3> previousPositions = new LinkedList<Vector3>();

    private bool inHand = false;
    private float vibeAmt = 0;



    public void Update()
    {
        FollowFirePoint();
    }

    public void CreatePower()
    {
        if (isActive){
            if (firePoint != null){
                if (objectPrefab != null){
                    if (!inHand)
                    {
                        superPower = Instantiate(objectPrefab, firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
                        superPower.transform.localScale = new Vector3(startScale, startScale, startScale);
                        superPower.GetComponent<Rigidbody>().useGravity = false;
                        upScaler = startScale;

                        CreateCharge();

                        followFirepoint = true;
                        inHand = true;
                    }
                }
            }
        }
    }

    public void GrowPower()
    {
        if (inHand){
            if (superPower != null){
                if(upScaler < maxScale){   
                    superPower.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
                    upScaler += 1 * Time.deltaTime;
                }
            }
        }
    }

    public void UsePower()
    {
        if (inHand){
            if (mussleFlash != null)
            {
                GameObject flash = Instantiate(mussleFlash, firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
                Destroy(flash, 1.5f);
            }
            superPower.GetComponent<Rigidbody>().AddRelativeForce(bulletForce);
            objectPrefab.GetComponent<Rigidbody>().useGravity = true;
            followFirepoint = false;
            inHand = false;

            SetChargeInActive();
            charge = null;
            vibeAmt = 0;
        }
    }

    public void CreateCharge()
    {
        if (chargeEffect != null){
            if(chargePoint != null)
            {
                charge = Instantiate(chargeEffect, chargePoint.position, Quaternion.Euler(chargePoint.eulerAngles));
                charge.transform.localScale = new Vector3(startScale, startScale, startScale);
            } 
        }
    }

    public void SetChargeActive()
    {
        if(charge != null)
        {
            charge.SetActive(true);
        }
    }

    public void SetChargeInActive()
    {
        if(charge != null)
        {
            charge.SetActive(false);
        }
    }

    public void SetSuperPowerActive()
    {
        if (superPower != null)
        {
            superPower.SetActive(true);
        }
    }

    public void SetSuperPowerInActive()
    {
        if(superPower != null)
        {
            superPower.SetActive(false);
        }
    }

    public void DestroySuperPower(float time)
    {
        Destroy(superPower, time);
    }

    private void FollowFirePoint()
    {
        if (followFirepoint){
            if (superPower != null)
            {
                superPower.transform.SetPositionAndRotation(firePoint.position, Quaternion.Euler(firePoint.eulerAngles));
            }
            if (charge != null)
            {
                charge.transform.SetPositionAndRotation(chargePoint.position, Quaternion.Euler(chargePoint.eulerAngles));
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

    public void HoldTimer()
    {
        holdTime += 1 * Time.deltaTime;
    }

    public void ResetHoldTimer()
    {
        holdTime = 0;
    }
    
    public void IncreaseHaptics()
    {
        if (inHand)
        {
            vibeAmt = Normalize(upScaler, startScale, maxScale);
            OVRInput.SetControllerVibration(1, vibeAmt, HandToggle());
        }
    }

    public void StopHaptics()
    {
        OVRInput.SetControllerVibration(0, 0, HandToggle());
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

    public void PlayChargeTone()
    {
        if (inHand)
        {
            if(chargeSound != null)
            {
                chargeSound.Play();
                chargeSound.pitch = upScaler;
            }
        }
    }

    public void PlaySound()
    {
        if(inHand)
        {
            if (instatiationSound != null)
            {
                instatiationSound.Play();
            }
        }
    }

    private float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }

}

