using UnityEngine;

public class DisplaySuperPowerInfo : MonoBehaviour
{

    public GameObject superPowerInfo;

    public void DisplayInfo()
    {
        if (superPowerInfo.active == false)
        {
            superPowerInfo.SetActive(true);
        }
        else if (superPowerInfo.active == true)
        {
            superPowerInfo.SetActive(false);
        }
    }
}
