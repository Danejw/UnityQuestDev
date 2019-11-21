using System.Collections;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    // Move target around circle with tangential speed
    public Vector3 bound;
    public float speed = 100;
    public float sphereRadius = 15;
    public float proximityToTarget = 10;

    private Vector3 initialPosition;

    private Vector3 nextMovementPoint;
    

    private void Start()
    {
        initialPosition = transform.position;
        CalculateNextMovementPoint();
    }

    private void CalculateNextMovementPoint()
    {
        /*
        float posX = Random.Range(initialPosition.x = bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y = bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z = bound.z, initialPosition.z + bound.z);

        nextMovementPoint = initialPosition + new Vector3(posX, posY, posZ);
        */

        float posX = Random.Range(-30, 30);
        float posY = Random.Range(6, 45);
        float posZ = Random.Range(-30, 30);

        nextMovementPoint = new Vector3(posX, posY, posZ);

        //nextMovementPoint = Random.insideUnitSphere * sphereRadius;
        


    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 1.0f * Time.deltaTime);

        if(Vector3.Distance(nextMovementPoint, transform.position) <= proximityToTarget)
        {
            CalculateNextMovementPoint();
        }
    }

}
