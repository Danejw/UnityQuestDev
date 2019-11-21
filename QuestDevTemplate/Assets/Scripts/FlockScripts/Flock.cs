using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    internal FlockController controller;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    // Calculate the velocity of the boid using the Steer() method and apply the result to the boid's rigibody velocity. Then, check if the current speed of the rigidbody component is inside out controller's max and min velocity ranges. If not, cap the velocity to the preset range.
    void Update()
    {
        if (controller)
        {
            Vector3 relativePos = Steer() * Time.deltaTime;

            if(relativePos != Vector3.zero)
            {
                rigidbody.velocity = relativePos;

                // enforce min and max speeds for the boids (flock)
                float speed = rigidbody.velocity.magnitude;

                if (speed > controller.maxVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.maxVelocity;
                }
                else if (speed > controller.minVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.minVelocity;
                }

                transform.LookAt(controller.target);
                
            }
        }
    }

    private Vector3 Steer()
    {
        // Cohesion
        Vector3 center = controller.flockCenter - transform.localPosition;

        // Alignment
        Vector3 velocity = controller.flockVelocity - rigidbody.velocity;

        // Follow the Leader
        Vector3 follow = controller.target.localPosition - transform.localPosition;

        //Seperation
        Vector3 seperation = Vector3.zero;

        foreach(Flock flock in controller.flockList)
        {
            if(flock != this)
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;
                seperation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        // Randomize
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        return (controller.centerWeight * center +
                controller.velocityWeight * velocity +
                controller.seperationWeight * seperation +
                controller.followWeight * follow +
                controller.randomizeWeight * randomize);
    }
}
