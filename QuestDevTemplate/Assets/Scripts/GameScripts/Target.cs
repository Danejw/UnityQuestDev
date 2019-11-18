using UnityEngine;

public class Target : MonoBehaviour
{
    // Target Destroyed event
    public delegate void OnTargetDestroyed();

    public static event OnTargetDestroyed TargetDestroyed;


    // if anything collides with the target, Invoke the Target Destroyed Event
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Target")
        {
            if (TargetDestroyed != null)
            {
                //Trigger event
                TargetDestroyed.Invoke();

                //play sound

                //explode?

                //destroy object
                Destroy(gameObject, .5f);
            }
        }
    }
}
