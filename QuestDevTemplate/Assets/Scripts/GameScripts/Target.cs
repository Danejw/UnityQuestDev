using UnityEngine;

public class Target : MonoBehaviour
{
    // Target Destroyed event
    public delegate void OnTargetDestroyed();

    public static event OnTargetDestroyed TargetDestroyed;

    public GameObject explosion;


    // if anything collides with the target, Invoke the Target Destroyed Event
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Target")
        {
            if (TargetDestroyed != null)
            {
                //Trigger event
                TargetDestroyed.Invoke();

                gameObject.GetComponent<Collider>().enabled = false;

                //play sound

                //explode
                if (explosion)
                {
                    Instantiate(explosion, transform.position, Quaternion.identity, this.transform);
                }

                //destroy object
                Destroy(this.gameObject, .7f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InnerBoundary")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OuterBoundary")
        {
            Destroy(this.gameObject);
        }
    }
}
