using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material staticMaterial;
    public Material grabbableMaterial;
    public Material interactbleMaterial;
    public Material ammoMaterial;

    private GameObject[] staticObjects;
    private GameObject[] grabbableObjects;
    private GameObject[] interactableObjects;
    private GameObject[] ammoObjects;

    private void Start()
    {
        ChangeStaticMaterials();
        ChangeGrabbableMaterials();
        ChangeInteractableMaterials();
        ChangeAmmoMaterials();
    }

    private void ChangeStaticMaterials()
    {
        staticObjects = GameObject.FindGameObjectsWithTag("Static");

        if (staticMaterial != null)
        {
            foreach (GameObject obj in staticObjects)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().material = staticMaterial;
                }
            }
        }
    }

    private void ChangeGrabbableMaterials()
    {
        grabbableObjects = GameObject.FindGameObjectsWithTag("Grabbable");

        if (grabbableObjects != null)
        {
            foreach (GameObject obj in grabbableObjects)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().material = grabbableMaterial;
                }
            }
        }
    }

    private void ChangeInteractableMaterials()
    {
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");

        if (interactableObjects != null)
        {
            foreach (GameObject obj in interactableObjects)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().material = interactbleMaterial;
                }
            }
        }
    }

    private void ChangeAmmoMaterials()
    {
        ammoObjects = GameObject.FindGameObjectsWithTag("Ammo");

        if (ammoObjects != null)
        {
            foreach (GameObject obj in ammoObjects)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().material = ammoMaterial;
                }
            }
        }
    }

}
