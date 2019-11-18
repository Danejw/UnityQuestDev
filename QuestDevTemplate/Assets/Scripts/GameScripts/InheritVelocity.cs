using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritVelocity : ChargeSuperPower
{
    List<Vector3> prevPositions;
    private Vector3 currentPosition;

    private float timeInterval = 0;
    private float elapsedTime = 0;

    private float velocity;
    private float distance;

    [SerializeField]
    private float forceMultiplier = 10;


    public void StoreStartPosition()
    {
        if (isActive)
        {
            prevPositions = new List<Vector3>();

            currentPosition = firePoint.position;
            prevPositions.Add(currentPosition);
        }
    }

    public void StorePrevPositions()
    {
        if (isActive)
        {
            timeInterval += 1 * Time.deltaTime * 2;
            elapsedTime += 1 * Time.deltaTime;

            // store current position every timeInterval
            if (timeInterval > 1)
            {
                // store current position if has moved from previous poistion
                if (currentPosition != firePoint.position)
                {
                    prevPositions.Add(firePoint.position);
                    currentPosition = firePoint.position;
                    //print("Last Position: " + currentPosition);
                    timeInterval = 0;
                }
            }
            // store only 10 previous positions
            if (prevPositions.Count > 10)
            {
                prevPositions.RemoveAt(10);
            }
        }
    }

    public void ResetElapsedTime()
    {
        elapsedTime = 0;
        //print("Elapsed Time: " + elapsedTime);
    }

    private float CalcVelocity()
    {
        prevPositions.Reverse();
        // calculate velocity if there are more that two stored positions
        if (prevPositions.Count > 2)
        {
            CalcDistance(prevPositions[0], prevPositions[prevPositions.Count / 2]);
            velocity = distance / elapsedTime;
        }
        else
        {
            velocity = 0;
        }
        //print("Elapsed Time: " + elapsedTime);
        //print("Velocity: " + velocity);
        return velocity;
    }

    private void CalcDistance(Vector3 pos1, Vector3 pos2)
    {
        distance = Mathf.Sqrt( Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2) + Mathf.Pow(pos1.z - pos2.z, 2));
        //print("Distance: " + distance);
    }

    public void CalcForce()
    {
        if (isActive)
        {
            bulletForce.z = CalcVelocity() * forceMultiplier;
        }
    }

    // Utility
    public void PrintPrevPositions()
    {
        if (prevPositions != null)
        {
            prevPositions.Reverse();

            foreach (Vector3 pos in prevPositions)
            {
                print(pos);
            }
        }
    }
}
