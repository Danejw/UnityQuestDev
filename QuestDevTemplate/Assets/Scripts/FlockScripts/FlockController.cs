using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    public float minVelocity = 1;
    public float maxVelocity = 8;
    public int flockSize = 20;

    // How far the boids should stick to the center
    public float centerWeight = 1;
    // Alignment behavior
    public float velocityWeight = 1;
    // How far each boid should be sperated withn the flock
    public float seperationWeight = 1;
    // How close wach boid should follow to the leader
    public float followWeight = 1;

    // Additional random noise
    public float randomizeWeight = 1;

    public Flock prefab;
    public Transform target;

    // Center position of the flock in the group
    internal Vector3 flockCenter;
    internal Vector3 flockVelocity; // Average Velocity

    public ArrayList flockList = new ArrayList();

    private void Start()
    {
        for(int i = 0; i < flockSize; i++)
        {
            Flock flock = Instantiate(prefab, transform.position, transform.rotation) as Flock;
            flock.transform.parent = transform;
            flock.controller = this;
            flockList.Add(flock);
        }
    }

    // Continuously update the average center and velocity of the flock
    private void Update()
    {
        // Calculate the Center and Velocity of the whole flock group
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach(Flock flock in flockList)
        {
            center += flock.transform.localPosition;
            velocity += flock.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
    }

}
