using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuperPowerUIManager : MonoBehaviour
{

    public GameObject spPanel; 
    public GameObject panelPrefab;
    public GameObject superPowerManager;


    private List<Transform> superPowers =  new List<Transform>();

    private float panelHeight = 50;
    private float panelYPos = 100;

    void Start()
    {
        GetSuperpowers();
        CreateSPPanel();
    }

    private void CreateSPPanel()
    {
        foreach (Transform sp in superPowers)
        {
            // create panel prefab as a child of sppanel
            GameObject newPanel = GameObject.Instantiate(panelPrefab);
            RectTransform rt = newPanel.GetComponent<RectTransform>();
            rt.SetParent(spPanel.transform);
            rt.transform.localPosition = new Vector3(0, panelYPos, 0);
            panelYPos -= panelHeight;
            // set the text as the superpower name
            newPanel.GetComponentInChildren<TextMeshProUGUI>().text = sp.name;
        }
    }

    private void GetSuperpowers()
    {
        foreach (Transform child in superPowerManager.transform)
        {
            superPowers.Add(child);
        }
    }

    public void ToggleOnOff()
    {
        superPowers[0].GetComponent<GameObject>().SetActive(true);
    }
}
