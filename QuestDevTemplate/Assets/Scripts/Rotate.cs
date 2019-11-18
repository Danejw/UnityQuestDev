using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotationSpeed = 1;
    public Transform pivotPoint;

    // Update is called once per frame
    void Update()
    {
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
            transform.LookAt(pivotPoint);
        }
    }
   
}
